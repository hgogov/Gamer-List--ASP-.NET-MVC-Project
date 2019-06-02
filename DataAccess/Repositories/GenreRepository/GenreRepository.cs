using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.GenreRepository
{
    public class GenreRepository : GenericRepository<Genre>, IDisposable
    {
        private readonly DbEntitiesContext context;

        public GenreRepository()
            : this(new DbEntitiesContext())
        {
        }

        public GenreRepository(DbEntitiesContext context)
        {
            this.context = context;
        }

        public Genre GetGenreByType(string type)
        {
            return context.Genres
                .Where(g => g.Type.Equals(type))
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
