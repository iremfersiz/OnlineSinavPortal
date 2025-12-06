using System.Security.Cryptography;
using System.Text;
using OnlineSinavPortal.Models;

namespace OnlineSinavPortal.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Veritabanı oluşturulmuş mu kontrol et
            if (!context.Database.CanConnect())
            {
                return;
            }

            // Admin kullanıcısı var mı kontrol et
            if (context.Admins.Any())
            {
                // Mevcut admin kullanıcısını güncelle (öğrenci numarası ve şifre hash'i)
                var admin = context.Admins.FirstOrDefault(a => a.KullaniciAdi == "admin");
                if (admin != null)
                {
                    // Öğrenci numarası yoksa ekle
                    if (string.IsNullOrEmpty(admin.OgrenciNumarasi))
                    {
                        admin.OgrenciNumarasi = "12345678";
                    }
                    // Şifre hash'ini güncelle
                    admin.Sifre = HashPassword("admin123");
                    context.SaveChanges();
                }
                return;
            }

            // Varsayılan admin kullanıcısı oluştur
            var newAdmin = new Admin
            {
                KullaniciAdi = "admin",
                Sifre = HashPassword("admin123"),
                Email = "admin@onlinesinav.com",
                AdSoyad = "Sistem Yöneticisi",
                OgrenciNumarasi = "12345678",
                Aktif = true,
                OlusturmaTarihi = DateTime.Now
            };

            context.Admins.Add(newAdmin);
            context.SaveChanges();
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}



