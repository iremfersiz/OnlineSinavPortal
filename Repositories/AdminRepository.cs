using Microsoft.EntityFrameworkCore;
using OnlineSinavPortal.Data;
using OnlineSinavPortal.Models;
using System.Security.Cryptography;
using System.Text;

namespace OnlineSinavPortal.Repositories
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Admin?> GetByKullaniciAdiAsync(string kullaniciAdi)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.KullaniciAdi == kullaniciAdi);
        }

        public async Task<Admin?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Admin?> GetByOgrenciNumarasiAsync(string ogrenciNumarasi)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.OgrenciNumarasi == ogrenciNumarasi);
        }

        public async Task<bool> ValidateLoginAsync(string kullaniciAdi, string sifre, string ogrenciNumarasi)
        {
            var admin = await GetByKullaniciAdiAsync(kullaniciAdi);
            if (admin == null || !admin.Aktif)
                return false;

            // Öğrenci numarası kontrolü
            if (string.IsNullOrEmpty(admin.OgrenciNumarasi) || admin.OgrenciNumarasi != ogrenciNumarasi)
                return false;

            // Şifre kontrolü
            var hashedPassword = HashPassword(sifre);
            return admin.Sifre == hashedPassword;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}







