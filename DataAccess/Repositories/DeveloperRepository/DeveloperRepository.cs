using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.DeveloperRepository
{
    public class DeveloperRepository : GenericRepository<Developer>, IDisposable
    {
        private readonly DbEntitiesContext context;

        public DeveloperRepository()
            : this(new DbEntitiesContext())
        {
        }

        public DeveloperRepository(DbEntitiesContext context)
        {
            this.context = context;
        }

        public Developer GetDeveloperByName(string name)
        {
            return context.Developers
                .Where(d => d.Name.Equals(name))
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
