using Microsoft.EntityFrameworkCore;

namespace Universe.Models
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetList();
        T GetByID(int id);
        void Delete(int deletedId);
        void Insert(T inserted);
        void Update(T updated);
        void Save();
    }
}
