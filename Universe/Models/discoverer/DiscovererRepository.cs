namespace Universe.Models.discoverer
{
    public class DiscovererRepository : IRepository<Discoverer>, IDisposable
    {
        private UniverseContext context;

        public DiscovererRepository(UniverseContext context)
        {
            this.context = context;
        }

        public void Delete(int discovererID)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Discoverer GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Discoverer> GetList()
        {
            //return context.Discoverers.ToList();
            throw new NotImplementedException();

        }

        public void Insert(Discoverer discoverer)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Discoverer discoverer)
        {
            throw new NotImplementedException();
        }
    }
}
