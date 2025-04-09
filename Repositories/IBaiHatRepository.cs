using Web_Music.Models;

namespace Web_Music.Repositories
{
    public interface IBaiHatRepository
    {
        Task<IEnumerable<BaiHat>> GetAllAsync();
        Task<BaiHat> GetByIdAsync(int id);
        Task AddAsync(BaiHat product);
        Task UpdateAsync(BaiHat product);
        Task DeleteAsync(int id);

    }
}
