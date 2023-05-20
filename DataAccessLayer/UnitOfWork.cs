using Microsoft.EntityFrameworkCore;
using Models;
using Universe.Models.spaceobject;
namespace Universe.Models
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DbUniverse _context;
		private Dictionary<Type, object> _repositories;
		private bool _disposed = false;

		public UnitOfWork(DbUniverse context)
		{
			_context = context;
			_repositories = new Dictionary<Type, object>();
		}
		public UnitOfWork()
		{
			_repositories = new Dictionary<Type, object>();
		}
		public void AddRepository<T>(IRepository<T> repo) where T : DbEntity
		{
			_repositories.Add(typeof(T), repo);
		}
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
