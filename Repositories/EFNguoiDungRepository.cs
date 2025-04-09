using Microsoft.EntityFrameworkCore;
using Web_Music.Models;

namespace Web_Music.Repositories
{
    public class EFNguoiDungRepository: IUserRepositories
    {
        private readonly ApplicationDbContext _context;

        public EFNguoiDungRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NguoiDung>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync(); 
            return await _context.NguoiDungs.ToListAsync();

        }

        public async Task<NguoiDung> GetByIdAsync(int id)
        {

            return await _context.NguoiDungs.FirstOrDefaultAsync(p => p.MaNguoiDung == id);
        }

        public async Task AddAsync(NguoiDung product)
        {
            await _context.NguoiDungs.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NguoiDung product)
        {
            _context.NguoiDungs.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung != null)
            {
                _context.NguoiDungs.Remove(nguoiDung);  // Xóa người dùng khỏi DbContext
                await _context.SaveChangesAsync();      // Lưu thay đổi vào cơ sở dữ liệu
            }
        }
    }
}
