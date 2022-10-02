using NFTURS_HFT_2021222.Models;
using NFTURS_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFTURS_HFT_2021222.Logic
{
    public class GenreLogic : IGenreLogic
    {
        IRepository<Genre> repo;

        public GenreLogic(IRepository<Genre> repo)
        {
            this.repo = repo;
        }

        public void Create(Genre item)
        {
            if (item.Name.Length > 0)
                repo.Create(item);
            else
                throw new ArgumentException("Invalid genre name");
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Genre Read(int id)
        {
            return repo.Read(id);
        }

        public IEnumerable<Genre> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Genre item)
        {
            repo.Update(item);
        }
    }
}
