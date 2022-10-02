using NFTURS_HFT_2021222.Models;
using System.Collections.Generic;

namespace NFTURS_HFT_2021222.Logic
{
    public interface IGenreLogic
    {
        void Create(Genre item);
        void Delete(int id);
        Genre Read(int id);
        IEnumerable<Genre> ReadAll();
        void Update(Genre item);
    }
}