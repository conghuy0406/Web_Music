using Microsoft.EntityFrameworkCore;
using Web_Music.Models;

namespace Web_Music.Repositories
{
    public class EFBaiHatRepository:IBaiHatRepository
    {
        private readonly ApplicationDbContext _context;

        public EFBaiHatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BaiHat>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync(); 
            return await _context.BaiHats.Include(p => p.MaAlbumNavigation).Include(p => p.MaTheLoaiNavigation).Include(p => p.MaCaSiNavigation).ToListAsync();

        }

        public async Task<BaiHat> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id); 
            // lấy thông tin kèm theo category 
            return await _context.BaiHats.Include(p => p.MaAlbumNavigation).FirstOrDefaultAsync(p => p.MaBaiHat == id);
        }

        public async Task AddAsync(BaiHat product)
        {
            _context.BaiHats.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BaiHat product)
        {
            _context.BaiHats.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.BaiHats.FindAsync(id);
            _context.BaiHats.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
