using MiniPos.Models;

namespace MiniPos.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customers> GetByNameAsync(string code);

        Task<IEnumerable<Customers>> GetAll();

        bool Delete(int Id);

        bool Add(Customers customer);

        bool Save();

        Task<Customers> GetByIdAsync(int id);

        Task<Customers> GetByIdAsyncNoTracking(int id);

        bool Update(Customers customers);
    }
}
