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

        public virtual DbSet<IngredientsDataSugar> IngredientsDataSugars { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<MaterialDisruption> MaterialDisruptions { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductDisruption> ProductDisruptions { get; set; } = null!;
        public virtual DbSet<ProductMaterial> ProductMaterials { get; set; } = null!;
        public virtual DbSet<SeverityRating> SeverityRatings { get; set; } = null!;
        public virtual DbSet<UpdatedTime> UpdatedTimes { get; set; } = null!;
        public virtual DbSet<VwHighSeverity> VwHighSeverities { get; set; } = null!;
        public virtual DbSet<VwNewsProductDisruptionDetail> VwNewsProductDisruptionDetails { get; set; } = null!;
        public virtual DbSet<VwProductMaterialDetail> VwProductMaterialDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=1235dc-sqldev;Database=ChainVision;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientsDataSugar>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Ingredients_Data_Sugar");

                entity.Property(e => e.Column1)
                    .HasMaxLength(50)
                    .HasColumnName("column1");

                entity.Property(e => e.Column2)
                    .HasMaxLength(50)
                    .HasColumnName("column2");

                entity.Property(e => e.Column3)
                    .HasMaxLength(50)
                    .HasColumnName("column3");

                entity.Property(e => e.Column4)
                    .HasMaxLength(50)
                    .HasColumnName("column4");

                entity.Property(e => e.Column5)
                    .HasMaxLength(50)
                    .HasColumnName("column5");

                entity.Property(e => e.Column6)
                    .HasMaxLength(50)
                    .HasColumnName("column6");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.Property(e => e.MaterialName).HasMaxLength(255);
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

            modelBuilder.Entity<SeverityRating>(entity =>
            {
                entity.ToTable("SeverityRating");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Hexcode).HasMaxLength(7);
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

            modelBuilder.Entity<VwNewsProductDisruptionDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_NewsProductDisruptionDetails");

                entity.Property(e => e.ArticleId).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.MaterialName).HasMaxLength(255);

                entity.Property(e => e.ProductName).HasMaxLength(255);

                entity.Property(e => e.PublishedDateUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("PublishedDateUTC");

                entity.Property(e => e.Title).HasMaxLength(500);
            });

            modelBuilder.Entity<VwProductMaterialDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_ProductMaterialDetails");

                entity.Property(e => e.MaterialName).HasMaxLength(255);

                entity.Property(e => e.ProductName).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
