using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<ApplicationUser>, IDisposable
    {
        private readonly DbEntitiesContext context;

        public UserRepository()
            : this(new DbEntitiesContext())
        {
        }

        public UserRepository(DbEntitiesContext context)
        {
            this.context = context;
        }

        public ApplicationUser GetUser(string id)
        {
            return context.Users.Find(id);
        }

        public bool CheckForMatchingUsername(string username, string userId)
        {
            var match = context.Users.Where(u => u.UserName.Equals(username) && !u.Id.Equals(userId)).FirstOrDefault();
            if (match != null)
                return true;

            return false;
        }

        public bool CheckForMatchingEmail(string email, string userId)
        {
            var match = context.Users.Where(u => u.Email.Equals(email) && !u.Id.Equals(userId)).FirstOrDefault();
            if (match != null)
                return true;

            return false;
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
