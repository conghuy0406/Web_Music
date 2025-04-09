using Web_Music.Models;

namespace Web_Music.Repositories
{
    public interface ITheLoaiRepository
    {
        Task<IEnumerable<TheLoai>> GetAllAsync();
        Task<TheLoai> GetByIdAsync(int id);
        Task AddAsync(TheLoai product);
        Task UpdateAsync(TheLoai product);
        Task DeleteAsync(int id);
    }
}
