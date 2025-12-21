using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineSinavPortal.Models;

namespace OnlineSinavPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sinav> Sinavlar { get; set; }
        public DbSet<Soru> Sorular { get; set; }
        public DbSet<Secenek> Secenekler { get; set; }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<SinavSonucu> SinavSonuclari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sinav>(entity =>
            {
                entity.HasOne(s => s.User)
                    .WithMany(u => u.OlusturduguSinavlar)
                    .HasForeignKey(s => s.UserId)
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

            modelBuilder.Entity<SinavSonucu>(entity =>
            {
                entity.HasOne(s => s.User)
                    .WithMany(u => u.SinavSonuclari)
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Ogrenci)
                    .WithMany(o => o.SinavSonuclari)
                    .HasForeignKey(s => s.OgrenciId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Sinav)
                    .WithMany()
                    .HasForeignKey(s => s.SinavId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Ogrenci>(entity =>
            {
                entity.HasIndex(e => e.OgrenciNumarasi).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}







