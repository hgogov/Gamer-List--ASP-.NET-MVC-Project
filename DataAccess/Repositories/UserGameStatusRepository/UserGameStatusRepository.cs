using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserGameStatusRepository
{
    public class UserGameStatusRepository : GenericRepository<UserGameStatus>, IDisposable
    {
        private readonly DbEntitiesContext context;
        public UserGameStatusRepository()
            : this(new DbEntitiesContext())
        {
        }
        public UserGameStatusRepository(DbEntitiesContext context)
        {
            this.context = context;
        }

        public UserGameStatus GetExistingUserGameStatus(string userId, int gameId)
        {
            return context.UserGameStatuses.Where(ugr => ugr.UserId.Equals(userId) && ugr.GameId.Equals(gameId)).FirstOrDefault();
        }

        public List<UserGameStatus> GetAllUserGameStatusesByGameId(int gameId)
        {
            return context.UserGameStatuses.Where(ugr => ugr.GameId == gameId).ToList();
        }

        public List<Game> GetAllGamesForUserById(string userId)
        {
            return context.UserGameStatuses.Where(ugs => ugs.UserId.Equals(userId)).Select(ugs => ugs.Game).ToList();
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
