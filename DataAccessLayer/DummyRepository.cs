using Models;
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
    public class DummyRepository<T> : IRepository<T> where T : DbEntity
    {
        private DbUniverse _context;
        public DummyRepository(DbUniverse context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }
        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            if (_context == null) throw new InvalidOperationException();
            throw new InvalidOperationException();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (_context == null) throw new InvalidOperationException();
            throw new InvalidOperationException();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (_context == null) throw new InvalidOperationException();
            throw new InvalidOperationException();
        }

        public void Delete(int deletedId)
        {
            if (_context == null) throw new InvalidOperationException();
        }

        public void DeleteByT(T entityToDelete)
        {
            if (_context == null) throw new InvalidOperationException();
        }

        public void Dispose()
        {
            if (_context == null) throw new InvalidOperationException();
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (_context == null) throw new InvalidOperationException();
            throw new InvalidOperationException();
        }

        public T GetByID(int id)
        {
            if (_context == null) throw new InvalidOperationException();
            throw new InvalidOperationException();
        }

        public Task<T> GetByIDAsync(int? id)
        {
            if (_context == null) throw new InvalidOperationException();
            throw new InvalidOperationException();
        }

        public IEnumerable<T> GetList()
        {
            if (_context == null) throw new InvalidOperationException();
            throw new InvalidOperationException();
        }

        public Task<IEnumerable<T>> GetListAsync()
        {
            if (_context == null) throw new InvalidOperationException();
            throw new InvalidOperationException();
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            if (_context == null) throw new InvalidOperationException();
            throw new InvalidOperationException();
        }

        public void Insert(T inserted)
        {
            if (_context == null) throw new InvalidOperationException();
        }

        public Task InsertAsync(T entity)
        {
            if (_context == null) throw new InvalidOperationException();
            throw new InvalidOperationException();
        }

        public void Save()
        {
            if (_context == null) throw new InvalidOperationException();
        }

        public Task<int> SaveAsync()
        {
            if (_context == null) throw new InvalidOperationException();
            throw new InvalidOperationException();
        }

        public void Update(T updated)
        {
            if (_context == null) throw new InvalidOperationException();
        }
    }
}
