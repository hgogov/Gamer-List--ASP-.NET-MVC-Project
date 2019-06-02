using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.UserGameRatingRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.UserGameRatingService
{
    public class UserGameRatingService : GenericService<UserGameRating, IGenericRepository<UserGameRating>>, IDisposable
    {
        private readonly UserGameRatingRepository userGameRatingRepository;

        public UserGameRatingService()
            : base()
        {
            userGameRatingRepository = new UserGameRatingRepository();
        }

        public UserGameRatingService(DbEntitiesContext context)
        {
            userGameRatingRepository = new UserGameRatingRepository(context);
        }

        public UserGameRating GetExistingUserGameRating(string userId, int gameId)
        {
            return userGameRatingRepository.GetExistingUserGameRating(userId, gameId);
        }

        public List<UserGameRating> GetAllUserGameRatingsByGameId(int gameId)
        {
            return userGameRatingRepository.GetAllUserGameRatingsByGameId(gameId);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userGameRatingRepository.Dispose();
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
