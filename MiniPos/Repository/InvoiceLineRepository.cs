using Microsoft.EntityFrameworkCore;
using MiniPos.Data;
using MiniPos.Interfaces;
using MiniPos.Models;
namespace MiniPos.Repository
{
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceLineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(InvoiceLine invoiceLine)
        {
            _context.Add(invoiceLine);
            return Save();
        }

        public bool Delete(int Id)
        {
            var invoiceLine = _context.InvoiceLine.FirstOrDefault(x => x.Id == Id);
            if (invoiceLine != null)
            {
                _context.InvoiceLine.Remove(invoiceLine);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Models.InvoiceLine> GetByIdAsync(int id)
        {
            return await _context.InvoiceLine.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Models.InvoiceLine> GetByIdAsyncNoTracking(int id)
        {
            return await _context.InvoiceLine.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(InvoiceLine invoiceLine)
        {
            _context.Update(invoiceLine);
            return Save();
        }
    }
}
