using OnlineSinavPortal.Models;

namespace OnlineSinavPortal.Repositories
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<Admin?> GetByKullaniciAdiAsync(string kullaniciAdi);
        Task<Admin?> GetByEmailAsync(string email);
        Task<Admin?> GetByOgrenciNumarasiAsync(string ogrenciNumarasi);
        Task<bool> ValidateLoginAsync(string kullaniciAdi, string sifre, string ogrenciNumarasi);
    }
}







