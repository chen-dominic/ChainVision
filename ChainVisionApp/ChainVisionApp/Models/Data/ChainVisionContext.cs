using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChainVisionApp.Models.Data
{
    public partial class ChainVisionContext : DbContext
    {
        public ChainVisionContext()
        {
        }

        public ChainVisionContext(DbContextOptions<ChainVisionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<MaterialDisruption> MaterialDisruptions { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductDisruption> ProductDisruptions { get; set; } = null!;
        public virtual DbSet<ProductMaterial> ProductMaterials { get; set; } = null!;
        public virtual DbSet<SeverityRating> SeverityRatings { get; set; } = null!;
        public virtual DbSet<UpdatedTime> UpdatedTimes { get; set; } = null!;
        public virtual DbSet<VwHighSeverity> VwHighSeverities { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.MaterialName).HasMaxLength(100);
            });

            modelBuilder.Entity<MaterialDisruption>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MaterialDisruption");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.ArticleId).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.PublishedDateUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("PublishedDateUTC");

                entity.Property(e => e.Title).HasMaxLength(500);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductName).HasMaxLength(255);
            });

            modelBuilder.Entity<ProductDisruption>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProductDisruption");
            });

            modelBuilder.Entity<ProductMaterial>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<SeverityRating>(entity =>
            {
                entity.ToTable("SeverityRating");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<UpdatedTime>(entity =>
            {
                entity.ToTable("UpdatedTime");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LastUpdatedTimeUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("LastUpdatedTimeUTC");
            });

            modelBuilder.Entity<VwHighSeverity>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwHighSeverity");

                entity.Property(e => e.ArticleId).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.LastUpdatedTimeUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("LastUpdatedTimeUTC");

                entity.Property(e => e.PublishedDateUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("PublishedDateUTC");

                entity.Property(e => e.SeverityDescription).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
