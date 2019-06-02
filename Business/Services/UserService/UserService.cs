using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.UserService
{
    public class UserService : GenericService<ApplicationUser, IGenericRepository<ApplicationUser>>, IDisposable
    {
        private readonly UserRepository userRepository;

        public UserService()
            : base()
        {
            userRepository = new UserRepository();
        }
        public UserService(DbEntitiesContext context)
        {
            userRepository = new UserRepository(context);
        }

        public ApplicationUser GetUser(string id)
        {
            return userRepository.GetUser(id);
        }

        public bool CheckForMatchingUsername(string username, string userId)
        {
            return userRepository.CheckForMatchingUsername(username, userId);
        }

        public bool CheckForMatchingEmail(string email, string userId)
        {
            return userRepository.CheckForMatchingEmail(email, userId);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userRepository.Dispose();
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
