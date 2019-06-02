using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.GameStatusRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.GameStatusService
{
    public class GameStatusService : GenericService<GameStatus, IGenericRepository<GameStatus>>, IDisposable
    {
        private readonly GameStatusRepository gameStatusRepository;

        public GameStatusService()
            : base()
        {
            gameStatusRepository = new GameStatusRepository();
        }

        public GameStatusService(DbEntitiesContext context)
        {
            gameStatusRepository = new GameStatusRepository(context);
        }

        public int GetStatusIdBySelectedStatus(string status)
        {
            return gameStatusRepository.GetStatusIdBySelectedStatus(status);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    gameStatusRepository.Dispose();
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
