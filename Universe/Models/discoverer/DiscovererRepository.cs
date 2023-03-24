namespace Universe.Models.discoverer
{
    public class DiscovererRepository : IRepository<Discoverer>, IDisposable
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
        public DiscovererRepository(DbUniverse context)
        {
            this.context = context;
        }
        public void Delete(int deletedId)
        {
            Discoverer discoverer = context.Discoverers.Find(deletedId);
            context.Discoverers.Remove(discoverer);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Discoverer GetByID(int id)
        {
            return context.Discoverers.Find(id);
        }

        public IEnumerable<Discoverer> GetList()
        {
            return context.Discoverers.ToList();
        }

        public void Insert(Discoverer inserted)
        {
            context.Discoverers.Add(inserted);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Discoverer updater)
        {
            context.Entry(updater).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
