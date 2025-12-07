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
                .Include(s => s.Admin)
                .OrderByDescending(s => s.OlusturmaTarihi)
                .ToListAsync();
        }

        public async Task<Sinav?> GetSinavWithSorularAsync(int id)
        {
            return await _dbSet
                .Include(s => s.Sorular)
                    .ThenInclude(s => s.Secenekler)
                .Include(s => s.Admin)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sinav>> GetSinavlarByAdminIdAsync(int adminId)
        {
            return await _dbSet
                .Where(s => s.AdminId == adminId)
                .Include(s => s.Admin)
                .OrderByDescending(s => s.OlusturmaTarihi)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sinav>> GetAllWithSorularAsync()
        {
            return await _dbSet
                .Include(s => s.Sorular)
                .Include(s => s.Admin)
                .ToListAsync();
        }
    }
}



