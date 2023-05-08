using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Universe.Models.spaceobject;

namespace Universe.Models
{
    public interface IRepository<T> : IDisposable where T : DbEntity
    {
        IEnumerable<T> GetList();
        Task<IEnumerable<T>> GetListAsync();
        T GetByID(int id);
        void Delete(int deletedId);
        void Insert(T inserted);
        void Update(T updated);
        void Save();
        void DeleteByT(T entityToDelete);
        bool Any(Expression<Func<T, bool>> predicate = null);
        Task<int> SaveAsync();
        Task InsertAsync(T entity);
        Task<T> GetByIDAsync(int? id);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
    }
}
