using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.GenreRepository;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.GenreService
{
    public class GenreService : GenericService<Genre, IGenericRepository<Genre>>, IDisposable
    {
        private readonly GenreRepository genreRepository;

        public GenreService()
            : base()
        {
            genreRepository = new GenreRepository();
        }

        public GenreService(DbEntitiesContext context)
        {
            genreRepository = new GenreRepository(context);
        }


        public Genre GetGenreByType(string type)
        {
            return genreRepository.GetGenreByType(type);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    genreRepository.Dispose();
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
