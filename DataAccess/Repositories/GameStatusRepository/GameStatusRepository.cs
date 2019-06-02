using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.GameStatusRepository
{
    public class GameStatusRepository : GenericRepository<GameStatus>, IDisposable
    {
        private readonly DbEntitiesContext context;

        public GameStatusRepository()
            : this(new DbEntitiesContext())
        {
        }
        public GameStatusRepository(DbEntitiesContext context)
        {
            this.context = context;
        }
        
        public int GetStatusIdBySelectedStatus(string status)
        {
            return context.GameStatuses.Where(gs => gs.Status.Equals(status)).Select(gs => gs.Id).FirstOrDefault();
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
