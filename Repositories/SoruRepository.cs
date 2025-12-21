using Microsoft.EntityFrameworkCore;
using OnlineSinavPortal.Data;
using OnlineSinavPortal.Models;

namespace OnlineSinavPortal.Repositories
{
    public class SoruRepository : Repository<Soru>, ISoruRepository
    {
        public SoruRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Soru>> GetSorularBySinavIdAsync(int sinavId)
        {
            return await _dbSet
                .Where(s => s.SinavId == sinavId)
                .Include(s => s.Secenekler)
                .OrderBy(s => s.Sira)
                .ToListAsync();
        }

        public async Task<Soru?> GetSoruWithSeceneklerAsync(int id)
        {
            return await _dbSet
                .Include(s => s.Secenekler)
                .Include(s => s.Sinav)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}






