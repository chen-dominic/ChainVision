using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChainVision.Data.Models
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

        public virtual DbSet<IngredientsDataCocoa> IngredientsDataCocoas { get; set; } = null!;
        public virtual DbSet<IngredientsDataSpringWheat> IngredientsDataSpringWheats { get; set; } = null!;
        public virtual DbSet<IngredientsDataSugar> IngredientsDataSugars { get; set; } = null!;
        public virtual DbSet<IngredientsDataWheat> IngredientsDataWheats { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<MaterialDisruption> MaterialDisruptions { get; set; } = null!;
        public virtual DbSet<MaterialInventory> MaterialInventories { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductDisruption> ProductDisruptions { get; set; } = null!;
        public virtual DbSet<ProductMaterial> ProductMaterials { get; set; } = null!;
        public virtual DbSet<SeverityRating> SeverityRatings { get; set; } = null!;
        public virtual DbSet<UpdatedTime> UpdatedTimes { get; set; } = null!;
        public virtual DbSet<VwHighSeverity> VwHighSeverities { get; set; } = null!;
        public virtual DbSet<VwNewsProductDisruptionDetail> VwNewsProductDisruptionDetails { get; set; } = null!;
        public virtual DbSet<VwProductMaterialDetail> VwProductMaterialDetails { get; set; } = null!;
        public virtual DbSet<VwRecentHighSeverityNews> VwRecentHighSeverityNews { get; set; } = null!;

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
            modelBuilder.Entity<IngredientsDataCocoa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Ingredients_Data_Cocoa");

                entity.Property(e => e.Cocoa).HasMaxLength(50);

                entity.Property(e => e.Last).HasMaxLength(50);

                entity.Property(e => e.Previous).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.Time).HasColumnType("date");
            });

            modelBuilder.Entity<IngredientsDataSpringWheat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Ingredients_Data_Spring_Wheat");

                entity.Property(e => e.Last).HasMaxLength(50);

                entity.Property(e => e.Previous).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.SpringWheat)
                    .HasMaxLength(50)
                    .HasColumnName("Spring_Wheat");

                entity.Property(e => e.Time).HasColumnType("date");
            });

            modelBuilder.Entity<IngredientsDataSugar>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Ingredients_DataSugar");

                entity.Property(e => e.Change).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.Last).HasMaxLength(50);

                entity.Property(e => e.Low).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.Previous).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.Sugar).HasMaxLength(50);

                entity.Property(e => e.Time).HasColumnType("date");
            });

            modelBuilder.Entity<IngredientsDataWheat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Ingredients_Data_Wheat");

                entity.Property(e => e.Last).HasMaxLength(50);

                entity.Property(e => e.Previous).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.Time).HasColumnType("date");

                entity.Property(e => e.Wheat).HasMaxLength(50);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.Property(e => e.MaterialName).HasMaxLength(255);
            });

            modelBuilder.Entity<MaterialDisruption>(entity =>
            {
                entity.ToTable("MaterialDisruption");
            });

            modelBuilder.Entity<MaterialInventory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MaterialInventory");

                entity.Property(e => e.Ingredient).HasMaxLength(50);

                entity.Property(e => e.InventoryMonth).HasColumnType("date");
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

            modelBuilder.Entity<VwRecentHighSeverityNews>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_RecentHighSeverityNews");

                entity.Property(e => e.ArticleId).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.MaterialName).HasMaxLength(255);

                entity.Property(e => e.ProductName).HasMaxLength(255);

                entity.Property(e => e.PublishedDateUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("PublishedDateUTC");

                entity.Property(e => e.Title).HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
