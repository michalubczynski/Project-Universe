using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Universe.Models;

namespace DAL_DataAccessLayer
{
    public class FakeRepository<T> : IRepository<T> where T : class
    {
        private List<T> _entities;
        private readonly Func<T, int> _getEntityIdFunc; //delegat do identyfikacji encji


        public FakeRepository(Func<T, int> getEntityIdFunc)
        {
         
            _entities = new List<T>();
            _getEntityIdFunc = getEntityIdFunc; // delegat, który będzie identyfikować unikalny identyfikator encji dla danego typu.

        }

        public void Delete(int deletedId)
        {
            T entityToDelete = _entities.FirstOrDefault(e => _getEntityIdFunc(e) == deletedId);
            if (entityToDelete != null)
            {
                _entities.Remove(entityToDelete);
            }
        }

        public T GetByID(int id)
        {
            return _entities.FirstOrDefault(e => _getEntityIdFunc(e) == id);
        }

        public IEnumerable<T> GetList()
        {
            return _entities.ToList();
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return _entities.ToList();
        }

        public async Task<int> SaveAsync()
        {
            return 0; // Fake repository does not save changes
        }

        public void Save()
        {
            // Fake repository does not save changes
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _entities.Count();
            }
            else
            {
                return _entities.Count(predicate.Compile());
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _entities.Any();
            }
            else
            {
                return _entities.Any(predicate.Compile());
            }
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _entities.FirstOrDefault();
            }
            else
            {
                return _entities.FirstOrDefault(predicate.Compile());
            }
        }

        public async Task<T> GetByIDAsync(int? id)
        {
            if (id.HasValue)
            {
                return _entities.FirstOrDefault(e => _getEntityIdFunc(e) == id.Value);
            }
            return null;
        }

        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _entities.Any();
            }
            else
            {
                return _entities.Any(predicate.Compile());
            }
        }

        public IQueryable<T> GetAll()
        {
            return _entities.AsQueryable();
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            return _entities.AsQueryable();
        }

        public T GetById(object id)
        {
            if (id is int entityId)
            {
                return _entities.FirstOrDefault(e => _getEntityIdFunc(e) == entityId);
            }
            return null;
        }

        public void Insert(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            // No-op for fake repository
        }

        public void DeleteByT(T entityToDelete)
        {
            _entities.Remove(entityToDelete);
        }

        public async Task InsertAsync(T entity)
        {
            _entities.Add(entity);
            await Task.CompletedTask; // Fake repository does not perform asynchronous operations
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    } 
}
