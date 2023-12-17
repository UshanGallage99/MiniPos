using Microsoft.EntityFrameworkCore;
using MiniPos.Data;
using MiniPos.Interfaces;
using MiniPos.Models;

namespace MiniPos.Repository
{
    public class TownRepository : ITownRepository
    {
        private readonly ApplicationDbContext _context;

        public TownRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Town town)
        {
            _context.Add(town);
            return Save();
        }

        public bool Delete(int Id)
        {
            var town = _context.Town.FirstOrDefault(x => x.Id == Id);
            if (town != null)
            {
                _context.Town.Remove(town);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Town>> GetAll()
        {
            return await _context.Town.ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<Town> GetByIdAsync(int id)
        {
            return await _context.Town.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Town> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Town.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Update(Town town)
        {
            _context.Update(town);
            return Save();
        }

        public async Task<Town> GetByNameAsync(string code)
        {
            return await _context.Town.FirstOrDefaultAsync(i => i.Code == code);
        }
    }
}
