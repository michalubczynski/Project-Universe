using Microsoft.EntityFrameworkCore;
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
            throw new NotImplementedException();
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

        public void Insert(T inserted)
        {

            context.Set<T>().Add(inserted);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T updated)
        {
            context.Entry(updated).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }
    }
}
