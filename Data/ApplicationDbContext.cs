using Microsoft.EntityFrameworkCore;
using OnlineSinavPortal.Models;

namespace OnlineSinavPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Sinav> Sinavlar { get; set; }
        public DbSet<Soru> Sorular { get; set; }
        public DbSet<Secenek> Secenekler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasIndex(e => e.KullaniciAdi).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Sinav>(entity =>
            {
                entity.HasOne(s => s.Admin)
                    .WithMany()
                    .HasForeignKey(s => s.AdminId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Soru>(entity =>
            {
                entity.HasOne(s => s.Sinav)
                    .WithMany(s => s.Sorular)
                    .HasForeignKey(s => s.SinavId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Secenek>(entity =>
            {
                entity.HasOne(s => s.Soru)
                    .WithMany(s => s.Secenekler)
                    .HasForeignKey(s => s.SoruId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}



