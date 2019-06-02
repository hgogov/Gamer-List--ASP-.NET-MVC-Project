using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.GameRepository;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.GameService
{
    public class GameService : GenericService<Game, IGenericRepository<Game>>, IDisposable
    {
        private readonly GameRepository gameRepository;

        public GameService()
            : base()
        {
            gameRepository = new GameRepository();
        }

        public GameService(DbEntitiesContext context)
        {
            gameRepository = new GameRepository(context);
        }


        public Game GetGameByTitle(string title)
        {
            return gameRepository.GetGameByTitle(title);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    gameRepository.Dispose();
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
