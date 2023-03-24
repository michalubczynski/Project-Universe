namespace Universe.Models.galaxy
{
    public class GalaxyRepository : IRepository<Galaxy>, IDisposable
    {
        private DbUniverse context;
        private bool disposed = false;


        public GalaxyRepository(DbUniverse context)
        {
            this.context = context;
        }
        public void Delete(int deletedId)
        {
            Galaxy galaxy = context.Galaxies.Find(deletedId);
            context.Galaxies.Remove(galaxy);
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

        public Galaxy GetByID(int id)
        {
            return context.Galaxies.Find(id);
        }

        public IEnumerable<Galaxy> GetList()
        {
            return context.Galaxies.ToList();
        }

        public void Insert(Galaxy inserted)
        {
            context.Galaxies.Add(inserted);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Galaxy updater)
        {
            context.Entry(updater).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
