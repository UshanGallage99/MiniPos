using Microsoft.EntityFrameworkCore;
using MiniPos.Data;
using MiniPos.Interfaces;
using MiniPos.Models;
namespace MiniPos.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Invoice invoice)
        {
            _context.Add(invoice);
            return Save();
        }

        public bool Delete(int Id)
        {
            var invoice = _context.Invoice.FirstOrDefault(x => x.Id == Id);
            if (invoice != null)
            {
                _context.Invoice.Remove(invoice);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Models.Invoice> GetByIdAsync(int id)
        {
            return await _context.Invoice.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Models.Invoice> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Invoice.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Invoice invoice)
        {
            _context.Update(invoice);
            return Save();
        }
    }
}
