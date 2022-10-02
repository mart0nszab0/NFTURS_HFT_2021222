using NFTURS_HFT_2021222.Models;
using System.Linq;

namespace NFTURS_HFT_2021222.Repository
{
    public class PublisherRepository : AbstractRepository<Publisher>
    {
        public PublisherRepository(GameRentalDbContext db) : base(db)
        {
        }

        public override Publisher Read(int id)
        {
            return db.Publishers.FirstOrDefault(p => p.PublisherId == id);
        }

        public override void Update(Publisher item)
        {
            Publisher publisherToUpdate = Read(item.PublisherId);

            typeof(Publisher)
                .GetProperties()
                .ToList()
                .ForEach(g =>
                {
                    if (g.GetAccessors().FirstOrDefault(v => v.IsVirtual) == null)
                        g.SetValue(publisherToUpdate, g.GetValue(item));
                });

            db.SaveChanges();
        }
    }
}
