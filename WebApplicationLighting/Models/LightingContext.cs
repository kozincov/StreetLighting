using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplicationLighting
{
    public partial class LightingContext : DbContext
    {
        public virtual DbSet<Lamps> Lamps { get; set; }
        public virtual DbSet<Lanterns> Lanterns { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<StreetLightings> StreetLightings { get; set; }
        public virtual DbSet<Streets> Streets { get; set; }
        public virtual DbSet<User> User { get; set; }

        public LightingContext(DbContextOptions<LightingContext> options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=дом-ПК\SQLExpress;Database=Lighting;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lamps>(entity =>
            {
                entity.HasKey(e => e.LampId);

                entity.Property(e => e.LampId).HasColumnName("LampID");
            });

            modelBuilder.Entity<Lanterns>(entity =>
            {
                entity.HasKey(e => e.LanternId);

                entity.Property(e => e.LanternId).HasColumnName("LanternID");
            });

            modelBuilder.Entity<Sections>(entity =>
            {
                entity.HasKey(e => e.SectionId);

                entity.HasIndex(e => e.StreetId);

                entity.Property(e => e.SectionId).HasColumnName("SectionID");

                entity.Property(e => e.BeginAndEnd).HasColumnName("Begin_and_End");

                entity.Property(e => e.StreetId).HasColumnName("StreetID");

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.StreetId)
                    .HasConstraintName("FK_Sections_Streets");
            });

            modelBuilder.Entity<StreetLightings>(entity =>
            {
                entity.HasKey(e => e.StreetLightingId);

                entity.Property(e => e.StreetLightingId).HasColumnName("StreetLightingID");

                entity.Property(e => e.Failure).HasColumnType("datetime");

                entity.Property(e => e.LampId).HasColumnName("LampID");

                entity.Property(e => e.LanternId).HasColumnName("LanternID");

                entity.Property(e => e.SectionId).HasColumnName("SectionID");

                entity.HasOne(d => d.Lamp)
                    .WithMany(p => p.StreetLightings)
                    .HasForeignKey(d => d.LampId)
                    .HasConstraintName("FK_StreetLightings_Lamps");

                entity.HasOne(d => d.Lantern)
                    .WithMany(p => p.StreetLightings)
                    .HasForeignKey(d => d.LanternId)
                    .HasConstraintName("FK_StreetLightings_Lanterns");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.StreetLightings)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_StreetLightings_Sections");
            });

            modelBuilder.Entity<Streets>(entity =>
            {
                entity.HasKey(e => e.StreetId);

                entity.Property(e => e.StreetId).HasColumnName("StreetID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);
            });
        }
    }
}
