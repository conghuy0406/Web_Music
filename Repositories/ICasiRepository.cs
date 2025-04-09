using Web_Music.Models;

namespace Web_Music.Repositories
{
    public interface ICasiRepository
    {
        Task<IEnumerable<CaSi>> GetAllAsync();
        Task<CaSi> GetByIdAsync(int id);
        Task AddAsync(CaSi product);
        Task UpdateAsync(CaSi product);
        Task DeleteAsync(int id);
    }
}
