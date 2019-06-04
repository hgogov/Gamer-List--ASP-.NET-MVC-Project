namespace DataAccess.Migrations
{
    using DataAccess.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.DbEntitiesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.DbEntitiesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.GameStatuses.AddOrUpdate(new GameStatus[]
           {
                new GameStatus() { Status = "Playing"},
                new GameStatus() { Status = "Plan To Play"},
                new GameStatus() { Status = "Finished"},
                new GameStatus() { Status = "On Hold"},
                new GameStatus() { Status = "Dropped"}
           });

            context.GameRatings.AddOrUpdate(new GameRating[]
            {
                new GameRating() { Rating = 1},
                new GameRating() { Rating = 2},
                new GameRating() { Rating = 3},
                new GameRating() { Rating = 4},
                new GameRating() { Rating = 5},
                new GameRating() { Rating = 6},
                new GameRating() { Rating = 7},
                new GameRating() { Rating = 8},
                new GameRating() { Rating = 9},
                new GameRating() { Rating = 10}
            });

            context.Developers.AddOrUpdate(new Developer[]
            {
                new Developer(){Id = 1, Name = "CD PROJEKT RED"},
                new Developer(){Name = "Nintendo"},
                new Developer(){Name = "Bethesda"},
                new Developer(){Name = "KOEI TECMO GAMES CO., LTD."},
                new Developer(){Name = "Square Enix"},
                new Developer(){Name = "Bioware"},
                new Developer(){Name = "CAPCOM CO., LTD."},
                new Developer(){Name = "Ubisoft"},
                new Developer(){Name = "Team Cherry"},
                new Developer(){Name = "Sega"},
                new Developer(){Name = "Insomniac"},
                new Developer(){Name = "Kojima Productions"},
                new Developer(){Name = "BANDAI NAMCO Entertainment"},
                new Developer(){Name = "Konami Digital Entertainment"}
            });

            context.Genres.AddOrUpdate(new Genre[]
            {
                new Genre(){Type = "Action"},
                new Genre(){Type = "Adventure"},
                new Genre(){Type = "Indie"},
                new Genre(){Type = "Simulation"},
                new Genre(){Type = "Puzzle"},
                new Genre(){Type = "Horror"},
                new Genre(){Type = "Sci-fi"},
                new Genre(){Type = "Platformer"},
                new Genre(){Type = "Co-op"},
                new Genre(){Type = "Arcade"},
                new Genre(){Type = "Survival"},
                new Genre(){Type = "Space"},
                new Genre(){Type = "Rogue-like"},
                new Genre(){Type = "FPS"},
                new Genre(){Type = "Mystery"},
                new Genre(){Type = "RPG"},
                new Genre(){Type = "Strategy"},
                new Genre(){Type = "RTS"},
                new Genre(){Type = "MOBA"},
                new Genre(){Type = "MMO"},
                new Genre(){Type = "MMORPG"},
                new Genre(){Type = "Tower Defense"},
                new Genre(){Type = "Sports"},
                new Genre(){Type = "Racing"},
                new Genre(){Type = "MMO"},
                new Genre(){Type = "MMORPG"},
            });

            context.Games.AddOrUpdate(new Game[]
            {
                new Game(){ Title ="DARK SOULS III", DeveloperId = 3, GenreId = 15, ReleaseDate = new DateTime(2016, 04, 11) , Description = "Dark Souls continues to push the boundaries with the latest, ambitious chapter in the critically-acclaimed and genre-defining series. Prepare yourself and Embrace The Darkness!",CoverImagePath = "noimage.jpg"},
                new Game(){ Title ="METAL GEAR SOLID V: THE PHANTOM PAIN", DeveloperId = 4, GenreId = 17, ReleaseDate = new DateTime(2015, 09, 01) , Description = "Ushering in a new era for the METAL GEAR franchise with cutting-edge technology powered by the Fox Engine, METAL GEAR SOLID V: The Phantom Pain, will provide players a first-rate gaming experience as they are offered tactical freedom to carry out open-world missions.", CoverImagePath = "noimage.jpg"},
                new Game(){ Title ="METAL GEAR SOLID V: GROUND ZEROES" , DeveloperId = 11, GenreId = 5, ReleaseDate = new DateTime(2014, 12, 18) , Description = "World-renowned Kojima Productions brings the Metal Gear Solid franchise to Steam with METAL GEAR SOLID V: GROUND ZEROES. Play as the legendary hero Snake and infiltrate a Cuban military base to rescue the hostages. Can you make it out alive?", CoverImagePath = "noimage.jpg"},
                new Game(){ Title ="Sonic & SEGA All-Stars Racing", DeveloperId = 10, GenreId = 6, ReleaseDate = new DateTime(2010, 03, 03) , Description = "SONIC AND SEGA ALL-STARS RACE FOR VICTORY IN A HIGH SPEED HIGH SKILL RACETRACK SHOWDOWN! TAKE TO THE TRACK BY CAR, MONSTER TRUCK, BIKE AND EVEN AEROPLANE IN SONIC & SEGA ALL-STARS RACING.", CoverImagePath = "noimage.jpg"},
                new Game(){ Title ="Hollow Knight", DeveloperId = 9, GenreId = 9, ReleaseDate = new DateTime(2017, 02, 24) , Description = "Forge your own path in Hollow Knight! An epic action adventure through a vast ruined kingdom of insects and heroes. Explore twisting caverns, battle tainted creatures and befriend bizarre bugs, all in a classic, hand-drawn 2D style.", CoverImagePath = "noimage.jpg"},
                new Game(){ Title ="Assassin's Creed Origins", DeveloperId = 8, GenreId = 19, ReleaseDate = new DateTime(2017, 10, 27) , Description = "ASSASSIN’S CREED® ORIGINS IS A NEW BEGINNING *The Discovery Tour by Assassin’s Creed®: Ancient Egypt is available now as a free update!* Ancient Egypt, a land of majesty and intrigue, is disappearing in a ruthless fight for power.", CoverImagePath = "noimage.jpg"},
                new Game(){ Title ="Devil May Cry 5", DeveloperId = 7, GenreId = 7, ReleaseDate = new DateTime(2019, 03, 08) , Description = "The ultimate Devil Hunter is back in style, in the game action fans have been waiting for.", CoverImagePath = "noimage.jpg"},
                new Game(){ Title ="Shadow of the Tomb Raider", DeveloperId = 6, GenreId = 3, ReleaseDate = new DateTime(2018, 09, 14) , Description = "As Lara Croft races to save the world from a Maya apocalypse, she must become the Tomb Raider she is destined to be.", CoverImagePath = "noimage.jpg"},
                new Game(){ Title ="Nioh", DeveloperId = 5, GenreId = 15, ReleaseDate = new DateTime(2017, 11, 07) , Description = "Ready to die? Experience the newest brutal action game from Team NINJA and Koei Tecmo Games. In the age of samurai, a lone traveler lands on the shores of Japan. He must fight his way through the vicious warriors and supernatural Yokai that infest the land in order to find that which he seeks.", CoverImagePath = "noimage.jpg"}
            });
        }
    }
}
