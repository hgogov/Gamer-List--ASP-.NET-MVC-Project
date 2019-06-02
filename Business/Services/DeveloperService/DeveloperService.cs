using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.DeveloperRepository;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.DeveloperService
{
    public class DeveloperService : GenericService<Developer, IGenericRepository<Developer>>, IDisposable
    {
        private readonly DeveloperRepository developerRepository;

        public DeveloperService()
            : base()
        {
            developerRepository = new DeveloperRepository();
        }

        public DeveloperService(DbEntitiesContext context)
        {
            developerRepository = new DeveloperRepository(context);
        }


        public Developer GetDeveloperByName(string name)
        {
            return developerRepository.GetDeveloperByName(name);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    developerRepository.Dispose();
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

