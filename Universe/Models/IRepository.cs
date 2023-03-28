using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Universe.Models
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetList();
        Task<IEnumerable<T>> GetListAsync();
        T GetByID(int id);
        void Delete(int deletedId);
        void Insert(T inserted);
        void Update(T updated);
        void Save();
        Task<int> SaveAsync();
        Task<T> GetByIDAsync(int? id);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null);
        bool Any(Expression<Func<T, bool>> predicate = null);

    }
}
