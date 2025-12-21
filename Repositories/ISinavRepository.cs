using OnlineSinavPortal.Models;

namespace OnlineSinavPortal.Repositories
{
    public interface ISinavRepository : IRepository<Sinav>
    {
        Task<IEnumerable<Sinav>> GetAktifSinavlarAsync();
        Task<Sinav?> GetSinavWithSorularAsync(int id);
        Task<IEnumerable<Sinav>> GetSinavlarByUserIdAsync(string userId);
        Task<IEnumerable<Sinav>> GetAllWithSorularAsync();
    }
}



