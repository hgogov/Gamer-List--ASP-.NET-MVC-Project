using DataAccess.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbEntitiesContext : IdentityDbContext<ApplicationUser>
    {
        public DbEntitiesContext()
            : base("name=DbEntitiesContext")
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GameRating> GameRatings { get; set; }
        public DbSet<UserGameRating> UserGameRatings { get; set; }
        public DbSet<GameStatus> GameStatuses { get; set; }
        public DbSet<UserGameStatus> UserGameStatuses { get; set; }

        //static DbEntitiesContext()
        //{

        //    Database.SetInitializer<DbEntitiesContext>(new IdentitySeedData());
        //}

        public static DbEntitiesContext Create()
        {
            return new DbEntitiesContext();
        }
    }
}
