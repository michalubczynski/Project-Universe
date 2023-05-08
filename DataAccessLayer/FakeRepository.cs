using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Universe.Models;
using Universe.Models.spaceobject;

namespace DAL_DataAccessLayer
{
    public class FakeRepository<T> : IRepository<T> where T : DbEntity
    {
        private List<T> _entities;


        public FakeRepository()
        {
            _entities = new List<T>();
        }

        public void Delete(int deletedId)
        {
            T entityToDelete = _entities.FirstOrDefault(e => e.Id == deletedId);
            if (entityToDelete != null)
            {
                _entities.Remove(entityToDelete);
            }
        }

        public T GetByID(int id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
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
                return _entities.FirstOrDefault(e => e.Id == id.Value);
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
                return _entities.FirstOrDefault(e => e.Id == entityId);
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
