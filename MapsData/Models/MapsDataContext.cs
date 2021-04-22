using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MapsData.Models
{
    public partial class MapsDataContext : DbContext
    {
        public MapsDataContext()
        {
        }

        public MapsDataContext(DbContextOptions<MapsDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LocationData> LocationData { get; set; }
        public virtual DbSet<LocationMap> LocationMap { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DZIDZOR-LAPTOP\\MSSQLSERVER01;Database=MapsData;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationData>(entity =>
            {
                entity.ToTable("locationData");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AtmosphericPressure);

                entity.Property(e => e.Gust);

                entity.Property(e => e.LocationId)
                    .IsRequired()
                    .HasColumnName("locationID")
                    .HasMaxLength(50);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");

                entity.Property(e => e.WindDirection);

                entity.Property(e => e.WindSpeed);
            });

            modelBuilder.Entity<LocationMap>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.ToTable("locationMap");

                entity.Property(e => e.LocationId)
                    .HasColumnName("locationID")
                    .HasMaxLength(50);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasColumnName("locationName")
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
