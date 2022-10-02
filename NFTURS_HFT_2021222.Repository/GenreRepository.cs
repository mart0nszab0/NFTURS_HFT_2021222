using NFTURS_HFT_2021222.Models;
using System.Linq;

namespace NFTURS_HFT_2021222.Repository
{
    public class GenreRepository : AbstractRepository<Genre>
    {
        public GenreRepository(GameRentalDbContext db) : base(db)
        {
        }

        public override Genre Read(int id)
        {
            return db.Genres.FirstOrDefault(g => g.GenreId == id);
        }

        public override void Update(Genre item)
        {
            Genre genreToUpdate = Read(item.GenreId);

            typeof(Genre)
                .GetProperties()
                .ToList()
                .ForEach(g =>
                {
                    if (g.GetAccessors().FirstOrDefault(v => v.IsVirtual) == null)
                        g.SetValue(genreToUpdate, g.GetValue(item));
                });

            db.SaveChanges();
        }
    }
}
