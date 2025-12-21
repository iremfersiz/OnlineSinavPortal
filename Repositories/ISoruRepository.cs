using OnlineSinavPortal.Models;

namespace OnlineSinavPortal.Repositories
{
    public interface ISoruRepository : IRepository<Soru>
    {
        Task<IEnumerable<Soru>> GetSorularBySinavIdAsync(int sinavId);
        Task<Soru?> GetSoruWithSeceneklerAsync(int id);
    }
}






