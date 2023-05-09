using System.Diagnostics;
using System.Threading;
using Universe.Models.spaceobject;

namespace Universe.Models
{
	public interface IUnitOfWork : IDisposable
	{
		public void AddRepository<T>(IRepository<T> repo) where T : DbEntity;
		IRepository<T> GetRepository<T>() where T : DbEntity; //pobranie repozytorium dla określonego typu encji. Metoda ta zwraca obiekt repozytorium dla danego typu, który pozwala na wykonywanie operacji CRUD (Create, Read, Update, Delete) na encjach tego typu.
		void SaveChanges(); //zapisuje wszystkie zmiany dokonane w trakcie działania Unit of Work w bazie danych.
		Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)); //asynchroniczne zapisanie wszystkich zmian dokonanych w trakcie działania Unit of Work w bazie danych.Parametr cancellationToken pozwala na anulowanie operacji zapisu, jeśli zajdzie taka potrzeba.
		void Update<T>(T entity) where T : DbEntity;
	}
}
