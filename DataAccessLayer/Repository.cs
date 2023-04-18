using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using System.Linq.Expressions;
using Universe.Models.starsystem;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Universe.Models
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        private DbUniverse context;
        private readonly DbSet<T> _dbSet;


        public Repository(DbUniverse context)
        {
            this.context = context;
            _dbSet = context.Set<T>();

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

/*        public void Insert(T inserted)
        {

            context.Set<T>().Add(inserted);
        }*/

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
        public void Save()
        {
            context.SaveChanges();
        }

/*        public void Update(T updated)
        {
            context.Entry(updated).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }*/

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

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            var query = GetAll();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteByT(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public async Task InsertAsync(T entity)
        {
             await _dbSet.AddAsync(entity);
        }
    }
}
