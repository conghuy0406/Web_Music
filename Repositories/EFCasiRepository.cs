using Microsoft.EntityFrameworkCore;
using Web_Music.Models;

namespace Web_Music.Repositories
{
    public class EFCasiRepository : ICasiRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCasiRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CaSi>> GetAllAsync()
        {
            return await _context.CaSis.ToListAsync();
        }

        public async Task<CaSi> GetByIdAsync(int id)
        {
            return await _context.CaSis.FindAsync(id);
        }

        public async Task AddAsync(CaSi caSi)
        {
            _context.CaSis.Add(caSi);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CaSi caSi)
        {
            _context.CaSis.Update(caSi);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var caSi = await _context.CaSis.FindAsync(id);
            if (caSi != null)
            {
                _context.CaSis.Remove(caSi);
                await _context.SaveChangesAsync();
            }
        }
    }
}
