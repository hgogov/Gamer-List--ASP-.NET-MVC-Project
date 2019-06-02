using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.GameRatingRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.GameRatingService
{
    public class GameRatingService : GenericService<GameRating, IGenericRepository<GameRating>>, IDisposable
    {
        private readonly GameRatingRepository gameRatingRepository;

        public GameRatingService()
            : base()
        {
            gameRatingRepository = new GameRatingRepository();
        }

        public GameRatingService(DbEntitiesContext context)
        {
            gameRatingRepository = new GameRatingRepository(context);
        }

        public int GetGameRatingByRating(int rating)
        {
            return gameRatingRepository.GetGameRatingByRating(rating);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    gameRatingRepository.Dispose();
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
