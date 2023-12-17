using Microsoft.EntityFrameworkCore;
using MiniPos.Data;
using MiniPos.Interfaces;
using MiniPos.Models;

namespace MiniPos.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository()
        {
        }

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Customers customer)
        {
            _context.Add(customer);
            return Save();
        }

        public bool Delete(int id)
        {
           
                var customer = _context.Customers.FirstOrDefault(x => x.Id == id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            
        }

        public async Task<IEnumerable<Customers>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customers> GetByIdAsync(int id)
        {
            return await _context.Customers.Include(c => c.ContactPerson).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Customers> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Customers.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Customers customers)
        {
            _context.Update(customers);
            return Save();
        }

        public async Task<Customers> GetByNameAsync(string code)
        {
            return await _context.Customers.FirstOrDefaultAsync(i => i.Code == code);            
        }
    }
}
