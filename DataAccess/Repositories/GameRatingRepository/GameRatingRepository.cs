using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.GameRatingRepository
{
    public class GameRatingRepository : GenericRepository<GameRating>, IDisposable
    {
        private readonly DbEntitiesContext context;

        public GameRatingRepository()
            : this(new DbEntitiesContext())
        {
        }
        public GameRatingRepository(DbEntitiesContext context)
        {
            this.context = context;
        }

        public int GetGameRatingByRating(int rating)
        {
            var gameRating = context.GameRatings.Where(gr => gr.Rating.Equals(rating)).FirstOrDefault();
            return gameRating.Id;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
