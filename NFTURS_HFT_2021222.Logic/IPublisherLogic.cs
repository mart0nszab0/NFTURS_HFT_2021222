using NFTURS_HFT_2021222.Models;
using System.Collections.Generic;

namespace NFTURS_HFT_2021222.Logic
{
    public interface IPublisherLogic
    {
        void Create(Publisher item);
        void Delete(int id);
        Publisher Read(int id);
        IEnumerable<Publisher> ReadAll();
        void Update(Publisher item);
    }
}