using System.Linq;

namespace NFTURS_HFT_2021222.Repository
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        T Read(int id);
        void Update(T item);
        void Delete(int id);

        IQueryable<T> ReadAll();
    }
}
