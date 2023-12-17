using MiniPos.Models;

namespace MiniPos.Interfaces
{
    public interface IInvoiceLineRepository
    {
        bool Add(InvoiceLine invoiceLine);
        bool Save();

        bool Delete(int Id);

        Task<InvoiceLine> GetByIdAsync(int id);

        Task<InvoiceLine> GetByIdAsyncNoTracking(int id);

        bool Update(InvoiceLine invoiceLine);
    }
}
