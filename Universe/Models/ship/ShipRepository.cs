using Universe.Models.star;

namespace Universe.Models.ship
{
    public class ShipRepository : IRepository<Ship>, IDisposable
    {
        private DbUniverse context;
        private bool disposed = false;


        public ShipRepository(DbUniverse context)
        {
            this.context = context;
        }
        public void Delete(int deletedId)
        {
            Ship ship = context.Ships.Find(deletedId);
            context.Ships.Remove(ship);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Ship GetByID(int id)
        {
            return context.Ships.Find(id);
        }

        public IEnumerable<Ship> GetList()
        {
            return context.Ships.ToList();
        }

        public void Insert(Ship inserted)
        {
            context.Ships.Add(inserted);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Ship updater)
        {
            context.Entry(updater).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
