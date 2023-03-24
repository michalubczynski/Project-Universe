
namespace Universe.Models.planet
{
    public class PlanetRepository : IRepository<Planet>, IDisposable
    {
        private DbUniverse context;
        private bool disposed = false;


        public PlanetRepository(DbUniverse context)
        {
            this.context = context;
        }
        public void Delete(int deletedId)
        {
            Planet planet = context.Planets.Find(deletedId);
            context.Planets.Remove(planet);
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

        public Planet GetByID(int id)
        {
            return context.Planets.Find(id);
        }

        public IEnumerable<Planet> GetList()
        {
            return context.Planets.ToList();
        }

        public void Insert(Planet inserted)
        {
            context.Planets.Add(inserted);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Planet updater)
        {
            context.Entry(updater).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
