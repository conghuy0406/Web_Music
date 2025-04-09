using Web_Music.Models;

namespace Web_Music.Repositories
{
    public interface IUserRepositories
    {
        Task<IEnumerable<NguoiDung>> GetAllAsync();
        Task<NguoiDung> GetByIdAsync(int id);
        Task AddAsync(NguoiDung product);
        Task UpdateAsync(NguoiDung product);
        Task DeleteAsync(int id);
    }
}
