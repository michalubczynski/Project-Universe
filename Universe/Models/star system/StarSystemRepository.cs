using Universe.Models.starsystem;

namespace Universe.Models.star_system
{
    public class StarSystemRepository : IRepository<StarSystem>, IDisposable
    {
        private DbUniverse context;
        private bool disposed = false;
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
        public StarSystemRepository( DbUniverse context )
        {
            this.context = context;
        }
        public void Delete(int deletedId)
        {
            StarSystem starSystem = context.StarSystems.Find(deletedId);
            context.StarSystems.Remove(starSystem);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public StarSystem GetByID(int id)
        {
            return context.StarSystems.Find(id);
        }

        public IEnumerable<StarSystem> GetList()
        {
            return context.StarSystems.ToList();
        }

        public void Insert(StarSystem inserted)
        {
            context.StarSystems.Add(inserted);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(StarSystem updater)
        {
            context.Entry(updater).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
