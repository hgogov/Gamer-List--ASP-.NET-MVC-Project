using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbEntitiesContext context;

        public GenericRepository()
            : this(new DbEntitiesContext())
        {
        }
        public GenericRepository(DbEntitiesContext context)
        {
            this.context = context;
        }

        protected DbSet<T> DbSet
        {
            get
            {
                return context.Set<T>();
            }
        }

        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetByID(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Deleted;
            context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
