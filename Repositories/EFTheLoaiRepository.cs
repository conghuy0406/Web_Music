using Microsoft.EntityFrameworkCore;
using Web_Music.Models;

namespace Web_Music.Repositories
{
    public class EFTheLoaiRepository:ITheLoaiRepository
    {
        private readonly ApplicationDbContext _context;

        public EFTheLoaiRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả danh mục
        public async Task<IEnumerable<TheLoai>> GetAllAsync()
        {
            return await _context.TheLoais.ToListAsync();
        }

        // Lấy danh mục theo ID
        public async Task<TheLoai> GetByIdAsync(int id)
        {
            return await _context.TheLoais.FindAsync(id);
        }

        // Thêm danh mục mới
        public async Task AddAsync(TheLoai category)
        {
            await _context.TheLoais.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        // Cập nhật danh mục
        public async Task UpdateAsync(TheLoai category)
        {
            _context.TheLoais.Update(category);
            await _context.SaveChangesAsync();
        }

        // Xóa danh mục theo ID
        public async Task DeleteAsync(int id)
        {
            var category = await _context.TheLoais.FindAsync(id);
            if (category != null)
            {
                _context.TheLoais.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
