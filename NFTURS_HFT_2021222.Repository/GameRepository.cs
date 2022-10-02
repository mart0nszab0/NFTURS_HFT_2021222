using NFTURS_HFT_2021222.Models;
using System.Linq;

namespace NFTURS_HFT_2021222.Repository
{
    public class GameRepository : AbstractRepository<Game>
    {
        public GameRepository(GameRentalDbContext db) : base(db)
        {
        }

        public override Game Read(int id)
        {
            return db.Games.FirstOrDefault(g => g.GameId == id);
        }
        
        public override void Update(Game item)
        {
            ;
            Game gameToUpdate = Read(item.GameId);
            ;
            typeof(Game)
                .GetProperties()
                .ToList()
                .ForEach(g => 
                {
                    if (g.GetAccessors().FirstOrDefault(v => v.IsVirtual) == null)
                        g.SetValue(gameToUpdate, g.GetValue(item));
                });
            ;
            db.SaveChanges();
        }
    }
}
