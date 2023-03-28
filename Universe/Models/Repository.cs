using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Universe.Models.starsystem;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Universe.Models
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        private DbUniverse context;

        public Repository(DbUniverse context)
        {
            this.context = context;
        }

        private bool disposed = false;

        public void Delete(int deletedId)
        {
            T? entityToDelete = context.Find(typeof(T), deletedId) as T;
            if (entityToDelete != null)
            {
                context.Remove(entityToDelete);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T GetByID(int id)
        {
            return context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetList()
        {
            return context.Set<T>().ToList();
        }
        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public void Insert(T inserted)
        {

            context.Set<T>().Add(inserted);
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T updated)
        {
            context.Entry(updated).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await context.CountAsync<T>(); 
            }
            else
            {
                return await context.CountAsync(predicate);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? await context.AnyAsync<T>() : await context.AnyAsync(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? await context.FirstOrDefaultAsync<T>() : await context.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetByIDAsync(int? id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = (IQueryable<T>)context;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.Any();
        }
    }
}
