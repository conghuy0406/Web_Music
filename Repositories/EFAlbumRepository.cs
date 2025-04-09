using Microsoft.EntityFrameworkCore;
using Web_Music.Models;

namespace Web_Music.Repositories
{
    public class EFAlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _context;

        public EFAlbumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Album>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync(); 
            return await _context.Albums.Include(p => p.MaCaSiNavigation).ToListAsync();

        }

        public async Task<Album> GetByIdAsync(int id)
        {
            return await _context.Albums
                .Include(a => a.BaiHats)              // Nạp danh sách bài hát của album
                .Include(a => a.MaCaSiNavigation)      // Nạp thông tin ca sĩ (navigation property)
                .FirstOrDefaultAsync(a => a.MaAlbum == id);
        }


        public async Task AddAsync(Album album)
        {
            var caSi = await _context.CaSis.FindAsync(album.MaCaSi);
            if (caSi != null)
            {
                album.MaCaSiNavigation = caSi;
            }

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Album product)
        {
            _context.Albums.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                // Xử lý trường hợp không tìm thấy album
                throw new ArgumentException("Album không tồn tại.");
            }
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();



        }
    }
    
}
