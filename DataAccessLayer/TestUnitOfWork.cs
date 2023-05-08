using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universe.Models;

namespace DAL_DataAccessLayer
{
    public class TestUnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories;

        public TestUnitOfWork()
        {
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.TryGetValue(typeof(T), out var repository))
            {
                return (IRepository<T>)repository;
            }

            // Tworzenie klasy fake dla repozytorium
            var fakeRepository = new FakeRepository<T>(entity => );
            _repositories.Add(typeof(T), fakeRepository);

            return fakeRepository;
        }

        public void SaveChanges()
        {
            // Implementacja dla testowego UnitOfWork (opcjonalne)
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Implementacja dla testowego UnitOfWork (opcjonalne)
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Implementacja dla testowego UnitOfWork (opcjonalne)
        }

        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
