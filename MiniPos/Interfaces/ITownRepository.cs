using MiniPos.Models;

namespace MiniPos.Interfaces
{
    public interface ITownRepository
    {
        Task<IEnumerable<Town>> GetAll();

        bool Delete(int Id);

        bool Add(Town town);

        bool Save();

        Task<Town> GetByIdAsync(int id);

        Task<Town> GetByIdAsyncNoTracking(int id);

        bool Update(Town town);

        Task<Town> GetByNameAsync(string code);
    }
}
