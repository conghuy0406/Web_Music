using Web_Music.Models;

namespace Web_Music.Repositories
{
    public interface IAlbumRepository
    {
        Task<IEnumerable<Album>> GetAllAsync();
        Task<Album> GetByIdAsync(int id);
        Task AddAsync(Album product);
        Task UpdateAsync(Album product);
        Task DeleteAsync(int id);
    }
}
