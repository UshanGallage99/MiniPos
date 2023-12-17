using MiniPos.Models;

namespace MiniPos.Interfaces
{
    public interface IContactPersonRepository
    {
        bool Add(ContactPerson contactPerson);
        bool Save();

        bool Delete(int Id);

        Task<ContactPerson> GetByIdAsync(int id);

        Task<ContactPerson> GetByIdAsyncNoTracking(int id);

        bool Update(ContactPerson contactPerson);
    }
}
