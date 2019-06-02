using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserGameRatingRepository
{
    public class UserGameRatingRepository : GenericRepository<UserGameRating>, IDisposable
    {
        private readonly DbEntitiesContext context;
        public UserGameRatingRepository()
            : this(new DbEntitiesContext())
        {
        }
        public UserGameRatingRepository(DbEntitiesContext context)
        {
            this.context = context;
        }

        public UserGameRating GetExistingUserGameRating(string userId, int gameId)
        {
            return context.UserGameRatings.Where(ugr => ugr.UserId.Equals(userId) && ugr.GameId.Equals(gameId)).FirstOrDefault();
        }

        public List<UserGameRating> GetAllUserGameRatingsByGameId(int gameId)
        {
            return context.UserGameRatings.Where(ugr => ugr.GameId == gameId).ToList();
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
