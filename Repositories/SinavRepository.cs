using Microsoft.EntityFrameworkCore;
using OnlineSinavPortal.Data;
using OnlineSinavPortal.Models;

namespace OnlineSinavPortal.Repositories
{
    public class SinavRepository : Repository<Sinav>, ISinavRepository
    {
        public SinavRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sinav>> GetAktifSinavlarAsync()
        {
            return await _dbSet
                .Where(s => s.Aktif)
                .Where(s => s.BaslangicTarihi == null || s.BaslangicTarihi <= DateTime.Now)
                .Where(s => s.BitisTarihi == null || s.BitisTarihi >= DateTime.Now)
                .Include(s => s.User)
                .OrderByDescending(s => s.OlusturmaTarihi)
                .ToListAsync();
        }

        public async Task<Sinav?> GetSinavWithSorularAsync(int id)
        {
            return await _dbSet
                .Include(s => s.Sorular)
                    .ThenInclude(s => s.Secenekler)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sinav>> GetSinavlarByUserIdAsync(string userId)
        {
            return await _dbSet
                .Where(s => s.UserId == userId)
                .Include(s => s.User)
                .OrderByDescending(s => s.OlusturmaTarihi)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sinav>> GetAllWithSorularAsync()
        {
            return await _dbSet
                .Include(s => s.Sorular)
                .Include(s => s.User)
                .ToListAsync();
        }
    }
}



