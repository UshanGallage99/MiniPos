using MiniPos.Models;

namespace MiniPos.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAll();

        bool Delete(int Id);

        bool Add(Products products);

        bool Save();

        Task<Products> GetByIdAsync(int id);

        Task<Products> GetByIdAsyncNoTracking(int id);

        bool Update(Products products);

        Task<Products> GetByNameAsync(string code);
    }
}
