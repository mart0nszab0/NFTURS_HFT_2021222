using System.Linq;

namespace NFTURS_HFT_2021222.Repository
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : class
    {
        protected GameRentalDbContext db;

        public AbstractRepository(GameRentalDbContext db)
        {
            this.db = db;
        }

        public void Create(T item)
        {
            db.Set<T>().Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Set<T>().Remove(Read(id));
            db.SaveChanges();
        }

        public IQueryable<T> ReadAll()
        {
            return db.Set<T>();
        }

        public abstract void Update(T item);
        public abstract T Read(int id);
    }
}
