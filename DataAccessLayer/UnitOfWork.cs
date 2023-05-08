using Microsoft.EntityFrameworkCore;
using Models;
using Universe.Models.discoverer;
using Universe.Models.galaxy;
using Universe.Models.planet;
using Universe.Models.ship;
using Universe.Models.spaceobject;
using Universe.Models.star;
using Universe.Models.starsystem;

namespace Universe.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbUniverse _context;
        private Dictionary<Type, object> _repositories;


        public UnitOfWork(DbUniverse context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }
        private bool _disposed = false;

        public IRepository<T> GetRepository<T>() where T : DbEntity
        {
            if (_repositories.TryGetValue(typeof(T), out var repository))
            {
                return (IRepository<T>)repository;
            }

            var newRepository = new Repository<T>(_context);
            _repositories.Add(typeof(T), newRepository);

            return newRepository;

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
             await _context.SaveChangesAsync(cancellationToken);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Update<T>(T entity) where T : DbEntity
        {
            _context.Entry(entity).State = EntityState.Modified;
            GetRepository<T>().Update(entity);
        }
    }
}
