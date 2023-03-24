namespace Universe.Models.star
{
    public class StarRepository : IRepository<Star>, IDisposable
    {
        private DbUniverse context;
        private bool disposed = false;


        public StarRepository(DbUniverse context)
        {
            this.context = context;
        }
        public void Delete(int deletedId)
        {
            Star star = context.Stars.Find(deletedId);
            context.Stars.Remove(star);
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

        public Star GetByID(int id)
        {
            return context.Stars.Find(id);
        }

        public IEnumerable<Star> GetList()
        {
            return context.Stars.ToList();
        }

        public void Insert(Star inserted)
        {
            context.Stars.Add(inserted);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Star updater)
        {
            context.Entry(updater).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
