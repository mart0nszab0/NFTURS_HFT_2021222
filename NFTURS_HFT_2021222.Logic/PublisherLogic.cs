using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFTURS_HFT_2021222.Models;
using NFTURS_HFT_2021222.Repository;

namespace NFTURS_HFT_2021222.Logic
{
    public class PublisherLogic : IPublisherLogic
    {
        IRepository<Publisher> repo;

        public PublisherLogic(IRepository<Publisher> repo)
        {
            this.repo = repo;
        }

        public void Create(Publisher item)
        {
            if (item.Name.Length > 0)
                repo.Create(item);
            else
                throw new ArgumentException("Invalid publisher name");
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Publisher Read(int id)
        {
            return repo.Read(id);
        }

        public IEnumerable<Publisher> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Publisher item)
        {
            repo.Update(item);
        }
    }
}
