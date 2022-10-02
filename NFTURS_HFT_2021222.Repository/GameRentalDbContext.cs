using Microsoft.EntityFrameworkCore;
using NFTURS_HFT_2021222.Models;


namespace NFTURS_HFT_2021222.Repository
{
    public class GameRentalDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public GameRentalDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseInMemoryDatabase("GameRentalDB")
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //connections between tables
            modelBuilder.Entity<Game>(g => g.
                                        HasOne(g => g.Publisher)
                                       .WithMany(p => p.Games)
                                       .HasForeignKey(g => g.PublisherID)
                                       .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Game>(g => g
                                       .HasOne(g => g.Genre)
                                       .WithMany(gen => gen.Games)
                                       .HasForeignKey(g => g.GenreID)
                                       .OnDelete(DeleteBehavior.Cascade));

            //seed data
            modelBuilder.Entity<Genre>().HasData(new Genre[]
            {
                new Genre(){GenreId=1, Name="MMORPG"},
                new Genre(){GenreId=2, Name="Single Player RPG"},
                new Genre(){GenreId=3, Name="Platformer"},
                new Genre(){GenreId=4, Name="Strategy"},
                new Genre(){GenreId=5, Name="Platformer"},
                new Genre(){GenreId=6, Name="FPS"},
                new Genre(){GenreId=7, Name="Souls-Like"},
                new Genre(){GenreId=8, Name="Action"},
                new Genre(){GenreId=9, Name="Open-World"},
            });

            modelBuilder.Entity<Publisher>().HasData(new Publisher[]
            {
                new Publisher(){PublisherId=1, Name="Blizzard" },
                new Publisher(){PublisherId=2, Name="2K" },
                new Publisher(){PublisherId=3, Name="Ubisoft" },
                new Publisher(){PublisherId=4, Name="Nintendo" },
                new Publisher(){PublisherId=5, Name="Activison" },
                new Publisher(){PublisherId=6, Name="Rockstar" },
                new Publisher(){PublisherId=7, Name="Bethesda" },
                new Publisher(){PublisherId=8, Name="EA" },
                new Publisher(){PublisherId=9, Name="Sega" },
                new Publisher(){PublisherId=10, Name="Sony" },
                new Publisher(){PublisherId=11, Name="From Software" },
            });

            modelBuilder.Entity<Game>().HasData(new Game[]
            {
                new Game(){GameId=1, Name="Mario", PublisherID=4, GenreID=3, NumberOfReviews=10, 
                GameRating=4.7, Mode=Game.GameMode.Singleplayer, Price=5000},

                new Game(){GameId=2, Name="AC Origins", PublisherID=3, GenreID=2 , NumberOfReviews=4,
                GameRating=4.1, Mode=Game.GameMode.Singleplayer, Price=2362},

                new Game(){GameId=3, Name="World Of Warcraft", PublisherID=2, GenreID=1 , NumberOfReviews=39,
                GameRating=3.2, Mode=Game.GameMode.Multiplayer, Price=3500},
                
                new Game(){GameId=4, Name="XCOM", PublisherID=2, GenreID=4, NumberOfReviews=2,
                GameRating=3.8, Mode=Game.GameMode.Singleplayer, Price=1300},

                new Game(){GameId=5, Name="GTA San Andreas", PublisherID=6, GenreID=9, NumberOfReviews=343,
                GameRating=4.9, Mode=Game.GameMode.Singleplayer, Price=15000},

                new Game(){GameId=6, Name="GTA V", PublisherID=6, GenreID=9, NumberOfReviews=501,
                GameRating=4.8, Mode=Game.GameMode.Singleplayer, Price=20000},

                new Game(){GameId=7, Name="Dark Souls", PublisherID=11, GenreID=7, NumberOfReviews=176,
                GameRating=4.9, Mode=Game.GameMode.Singleplayer, Price=520},

                new Game(){GameId=8, Name="Bloodborne", PublisherID=10, GenreID=7, NumberOfReviews=92,
                GameRating=4.9, Mode=Game.GameMode.Singleplayer, Price=500},

                new Game(){GameId=9, Name="GTA Online", PublisherID=6, GenreID=9, NumberOfReviews=1972,
                GameRating=2.3, Mode=Game.GameMode.Multiplayer, Price=7000}
            });
        }
    }
}
