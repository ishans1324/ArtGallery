using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ArtGalleryAPI.Models
{
    public partial class ACE42023Context : DbContext
    {
        public ACE42023Context()
        {
        }

        public ACE42023Context(DbContextOptions<ACE42023Context> options)
            : base(options)
        {
        }

        public virtual DbSet<ArtUser> ArtUsers { get; set; }
        public virtual DbSet<Artistishan> Artistishans { get; set; }
        public virtual DbSet<Painting> Paintings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DEVSQL.corp.local;Database=ACE 4- 2023;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ArtUser>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__art_user__DD70126488BB7E01");

                entity.ToTable("art_user");

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Uname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("uname");
            });

            modelBuilder.Entity<Artistishan>(entity =>
            {
                entity.HasKey(e => e.Aid)
                    .HasName("PK__artistis__DE508E2E425390CD");

                entity.ToTable("artistishan");

                entity.Property(e => e.Aid)
                    .ValueGeneratedNever()
                    .HasColumnName("aid");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Birthplace)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("birthplace");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.StyleOfWork)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("style_of_work");
            });

            modelBuilder.Entity<Painting>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PK__Painting__DD37D91A3E6137C2");

                entity.ToTable("Painting");

                entity.Property(e => e.Pid)
                    .ValueGeneratedNever()
                    .HasColumnName("pid");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.StyleOfArt)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("style_of_art");

                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.Year)
                    .HasColumnType("date")
                    .HasColumnName("year");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Paintings)
                    .HasForeignKey(d => d.ArtistId)
                    .HasConstraintName("FK__Painting__artist__1CBC4616");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
