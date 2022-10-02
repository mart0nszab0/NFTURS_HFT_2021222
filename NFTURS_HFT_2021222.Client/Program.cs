using ConsoleTools;
using NFTURS_HFT_2021222.Models;
using NFTURS_HFT_2021222.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NFTURS_HFT_2021222.Client
{
    internal class Program
    {
        static RestService rest;

        static void Main(string[] args)
        {
            //f
            rest = new RestService("http://localhost:32095/", "Game");

            //level1 menus
            var gameMenu = new ConsoleMenu(args, level: 1)
                .Add("Add new game", () => CreateGame())
                .Add("Read game by id", () => Read<Game>())
                .Add("Read all games", () => ReadAll<Game>())
                .Add("Update game", () => Update<Game>())
                .Add("Delete game", () => Delete<Game>())
                .Add("BestRatedGameInfo", () => BestRatedGameInfo())
                .Add("WorstRatedGameInfo", () => WorstRatedGameInfo())
                .Add("MostReviewedGameInfo", () => MostReviewedGameInfo())
                .Add("AllSoulsLikeGamesInfo", () => AllSoulsLikeGamesInfo())
                .Add("MultiplayerInfo", () => MultiplayerInfo());

            var genreMenu = new ConsoleMenu(args, level: 1)
                .Add("Add new genre", () => CreateGenre())
                .Add("Read genre by id", () => Read<Genre>())
                .Add("Read all genres", () => ReadAll<Genre>())
                .Add("Update genre", () => Update<Genre>())
                .Add("Delete genre", () => Delete<Genre>());

            var publisherMenu = new ConsoleMenu(args, level: 1)
                .Add("Add new publisher", () => CreatePublisher())
                .Add("Read publisher by id", () => Read<Publisher>())
                .Add("Read all publisher", () => ReadAll<Publisher>())
                .Add("Update publisher", () => Update<Publisher>())
                .Add("Delete publisher", () => Delete<Publisher>());

            //level 0 (main) menu
            var mainMenu = new ConsoleMenu(args, level: 0)
                .Add("Game", () => gameMenu.Show())
                .Add("Genre", () => genreMenu.Show())
                .Add("Publisher", () => publisherMenu.Show()); 

            //menu exits
            mainMenu.Add("Quit", () => mainMenu.CloseMenu());
            gameMenu.Add("Back", () => gameMenu.CloseMenu());
            genreMenu.Add("Back", () => genreMenu.CloseMenu());
            publisherMenu.Add("Back", () => publisherMenu.CloseMenu());

            //run main menu for the first time
            mainMenu.Show();
        }

        //DOESN'T WORK YET
        static void CreateGame() 
        {
            Console.Write("Enter game name: ");
            string name = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Enter game price: ");
            int price = int.Parse(Console.ReadLine());
            Console.WriteLine();

            
            Game game = new Game();
            game.Name = name;
            game.Price = price;

            rest.Post(game, "Game");

            Console.ReadKey();
        }
        static void CreateGenre() 
        {
            Console.Write("Enter genre name: ");
            string name = Console.ReadLine();
            Console.WriteLine();


            Genre genre = new Genre() { Name = name};

            rest.Post(genre, "Genre");

            Console.ReadKey();
        }
        static void CreatePublisher() 
        {
            Console.Write("Enter publisher name: ");
            string name = Console.ReadLine();
            Console.WriteLine();


            Publisher publisher = new Publisher() { Name = name };

            rest.Post(publisher, "Publisher");

            Console.ReadKey();
        }

        static void Read<T>()
        {
            Console.Write($"Enter {typeof(T).Name} ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine();

            T item = rest.Get<T>(id, typeof(T).Name);
            if (item != null)
            {

                PropertyInfo[] props = typeof(T).GetProperties();

                //Looping through the properties of the current table entry
                foreach (var prop in props)
                {
                    //Checking if it has a UserVisible custom attribute (that means it should be written out for the user)
                    if (prop.GetCustomAttribute<UserVisibleAttribute>() != null)
                    {
                        //checking if it's a navigation property (we only need the name of those, but they're not in the table we're loooping through)
                        if (prop.GetGetMethod().IsVirtual && prop.GetType().GetProperty("Name") != null)
                        {
                            //Getting the navigation property reference
                            var v = prop.GetValue(item);

                            //Writing out the "Name" property of the navigation property
                            if (prop.GetValue(item) != null)
                                Console.WriteLine($"{prop.Name}: {v.GetType().GetProperty("Name").GetValue(v)}");
                        }
                        else //If it's not a nav. property we just simple write out the property value
                        {
                            Console.WriteLine($"{prop.Name}: {prop.GetValue(item)}");
                        }
                    }
                }

            }
            else
            {
                Console.WriteLine("No such id in table");
            }

            Console.ReadKey();
        }

        static void ReadAll<T>()
        {
            //Getting all elements of the table
            IEnumerable<T> allItems = rest.Get<T>(typeof(T).Name);
            
            //Getting all the properties of the table
            PropertyInfo[] props = typeof(T).GetProperties();

            //Looping through all the table objects one-by-one
            foreach (T item in allItems)
            {
                //Looping through the properties of the current table entry
                foreach (var prop in props)
                {
                    //Checking if it has a UserVisible custom attribute (that means it should be written out for the user)
                    if (prop.GetCustomAttribute<UserVisibleAttribute>() != null)
                    {
                        //checking if it's a navigation property (we only need the name of those, but they're not in the table we're loooping through)
                        if (prop.GetGetMethod().IsVirtual && prop.GetType().GetProperty("Name") != null)
                        {
                            //Getting the navigation property reference
                            var v = prop.GetValue(item);
                            
                            //Writing out the "Name" property of the navigation property
                            if (prop.GetValue(item) != null)
                                Console.WriteLine($"{prop.Name}: {v.GetType().GetProperty("Name").GetValue(v)}");
                        }
                        else //If it's not a nav. property we just simple write out the property value
                        {
                            Console.WriteLine($"{prop.Name}: {prop.GetValue(item)}");
                        }
                    }
                }
                Console.WriteLine("");
            }
            Console.ReadKey();
        }

        static void Delete<T>()
        {
            Console.Write($"Enter {typeof(T).Name} ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine();

            T item = rest.Get<T>(id, typeof(T).Name);

            if (item != null)
                rest.Delete(id, typeof(T).Name);
            else
                Console.WriteLine("No such id in table");
        
        }
        

        static void Update<T>()
        {
            Type t = typeof(T);
            string tName = t.Name;

            Console.Write($"ID of the {tName} you want to update: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write($"New name of the {tName}: ");
            string name = Console.ReadLine();
            Console.WriteLine();

            T item = rest.Get<T>(id, tName);

            PropertyInfo prop = t.GetProperty("Name");

            if (prop != null)
                prop.SetValue(item, name);

            
            rest.Put(item, tName);


            Console.WriteLine("Update successful");
            Console.ReadKey();
        }

        static void BestRatedGameInfo()
        {
            GameInfo info = rest.Get<GameInfo>("Stat/BestRatedGameInfo")[0];
            List<PropertyInfo> props = typeof(GameInfo)
                                .GetProperties()
                                .ToList();

            props.ForEach(p => Console.WriteLine($"{p.Name}: {p.GetValue(info)}"));

            Console.ReadKey();
        }

        static void WorstRatedGameInfo()
        {
            GameInfo info = rest.Get<GameInfo>("Stat/WorstRatedGameInfo")[0];
            List<PropertyInfo> props = typeof(GameInfo)
                                .GetProperties()
                                .ToList();

            props.ForEach(p => Console.WriteLine($"{p.Name}: {p.GetValue(info)}"));

            Console.ReadKey();
        }

        static void MostReviewedGameInfo()
        {
            GameInfo info = rest.Get<GameInfo>("Stat/MostReviewedGameInfo")[0];
            List<PropertyInfo> props = typeof(GameInfo)
                                .GetProperties()
                                .ToList();

            props.ForEach(p => Console.WriteLine($"{p.Name}: {p.GetValue(info)}"));
            
            Console.ReadKey();
        }

        static void AllSoulsLikeGamesInfo()
        {
            IEnumerable<GameInfo> info = rest.Get<GameInfo>("Stat/AllSoulsLikeGamesInfo");
            List<PropertyInfo> props = typeof(GameInfo)
                                .GetProperties()
                                .ToList();
            info
                .ToList()
                .ForEach(i =>
                {
                    props.ForEach(p => Console.WriteLine($"{p.Name}: {p.GetValue(i)}"));
                    Console.WriteLine();
                });

            Console.ReadKey();
        }

        static void MultiplayerInfo()
        {
            IEnumerable<GameInfo> info = rest.Get<GameInfo>("Stat/MultiplayerInfo");
            List<PropertyInfo> props = typeof(GameInfo)
                                .GetProperties()
                                .ToList();
            info
                .ToList()
                .ForEach(i =>
                {
                    props.ForEach(p => Console.WriteLine($"{p.Name}: {p.GetValue(i)}"));
                    Console.WriteLine();
                });

            Console.ReadKey();
        }
    }
}
