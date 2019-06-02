using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.GameRepository
{
    public class GameRepository: GenericRepository<Game>, IDisposable
    {
        private readonly DbEntitiesContext context;

        public GameRepository()
            : this(new DbEntitiesContext())
        {
        }

        public GameRepository(DbEntitiesContext context)
            :base(context)
        {
            this.context = context;
        }

        public Game GetGameByTitle(string title)
        {
            return context.Games
                .Where(g => g.Title.Equals(title))
                .FirstOrDefault();
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
