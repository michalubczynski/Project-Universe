using Microsoft.EntityFrameworkCore;

namespace Universe.Models.galaxy
{
    public class GalaxyRepository : IRepository<Galaxy>, IDisposable
    {
        public void Delete(int deletedId)
        {
            using (var db = new DbUniverse())
            {
                var discoverer = db.Discoverers.
                    Where(d => d.DiscovererId == deletedId).
                    FirstOrDefault();
                db.Discoverers.Remove(discoverer);

                // use save
                db.SaveChanges();
            }

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Galaxy GetByID(int id)
        {
            using (var db = new DbUniverse())
            {
                return db.Galaxies.Where(g => g.GalaxyId == id).FirstOrDefault();
            }
        }

        public IEnumerable<Galaxy> GetList()
        {
            using (var db = new DbUniverse())
            {
                return db.Galaxies.ToList();
            }
        }

        public void Insert(Galaxy inserted)
        {
            using (var db = new DbUniverse())
            {
                db.Galaxies.Add(inserted);

                // use save
                db.SaveChanges();
            }
        }
        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Galaxy updater)
        {
            throw new NotImplementedException();
        }
    }
}
