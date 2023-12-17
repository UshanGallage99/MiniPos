using MiniPos.Models;

namespace MiniPos.Interfaces
{
    public interface IInvoiceRepository
    {
        bool Add(Invoice invoice);
        bool Save();

        bool Delete(int Id);

        Task<Invoice> GetByIdAsync(int id);

        Task<Invoice> GetByIdAsyncNoTracking(int id);

        bool Update(Invoice invoice);
    }
}
