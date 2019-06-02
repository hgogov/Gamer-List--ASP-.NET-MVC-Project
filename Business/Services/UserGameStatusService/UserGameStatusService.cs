using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.UserGameStatusRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.UserGameStatusService
{
    public class UserGameStatusService : GenericService<UserGameStatus, IGenericRepository<UserGameStatus>>, IDisposable
    {
        private readonly UserGameStatusRepository userGameStatusRepository;

        public UserGameStatusService()
            : base()
        {
            userGameStatusRepository = new UserGameStatusRepository();
        }

        public UserGameStatusService(DbEntitiesContext context)
        {
            userGameStatusRepository = new UserGameStatusRepository(context);
        }

        public UserGameStatus GetExistingUserGameStatus(string userId, int gameId)
        {
            return userGameStatusRepository.GetExistingUserGameStatus(userId, gameId);
        }

        public List<UserGameStatus> GetAllUserGameStatusesByGameId(int gameId)
        {
            return userGameStatusRepository.GetAllUserGameStatusesByGameId(gameId);
        }

        public List<Game> GetAllGamesForUserById(string userId)
        {
            return userGameStatusRepository.GetAllGamesForUserById(userId);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userGameStatusRepository.Dispose();
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
