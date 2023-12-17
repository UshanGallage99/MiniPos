using Microsoft.EntityFrameworkCore;
using MiniPos.Data;
using MiniPos.Interfaces;
using MiniPos.Models;

namespace MiniPos.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Products products)
        {
            _context.Add(products);
            return Save();
        }

        public bool Delete(int Id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == Id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Products>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Products> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Products products)
        {
            _context.Update(products);
            return Save();
        }
        public async Task<Products> GetByNameAsync(string code)
        {
            return await _context.Products.FirstOrDefaultAsync(i => i.Code == code);
        }

    }
}
