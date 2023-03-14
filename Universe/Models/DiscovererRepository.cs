namespace Universe.Models
{
    public class DiscovererRepository : IDiscovererRepository, IDisposable
    {
        private UniverseContext context;

        public DiscovererRepository(UniverseContext context)
        {
            this.context = context;
        }

        public void DeleteDiscoverer(int discovererID)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Discoverer GetDiscovererByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Discoverer> GetDiscovererList()
        {
            //return context.Discoverers.ToList();
            throw new NotImplementedException();

        }

        public void InsertDiscoverer(Discoverer discoverer)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateDiscoverer(Discoverer discoverer)
        {
            throw new NotImplementedException();
        }
    }
}
