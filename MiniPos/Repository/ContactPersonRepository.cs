using Microsoft.EntityFrameworkCore;
using MiniPos.Data;
using MiniPos.Interfaces;
using MiniPos.Models;
namespace MiniPos.Repository
{
    public class ContactPersonRepository : IContactPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactPersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(ContactPerson contactPerson)
        {
            _context.Add(contactPerson);
            return Save();
        }

        public bool Delete(int Id)
        {
            var contactPerson = _context.ContactPerson.FirstOrDefault(x => x.Id == Id);
            if (contactPerson != null)
            {
                _context.ContactPerson.Remove(contactPerson);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Models.ContactPerson> GetByIdAsync(int id)
        {
            return await _context.ContactPerson.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Models.ContactPerson> GetByIdAsyncNoTracking(int id)
        {
            return await _context.ContactPerson.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(ContactPerson contactPerson)
        {
            _context.Update(contactPerson);
            return Save();
        }
    }
}
