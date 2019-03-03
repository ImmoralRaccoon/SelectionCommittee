using System;
using System.Linq;
using System.Threading.Tasks;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.DAL.Entities.Base;

namespace SelectionCommittee.DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext DbContext;
        private bool _disposed;

        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public abstract Task<TEntity> GetAsync(int id);

        public abstract IQueryable<TEntity> GetAll();

        public abstract void Delete(int id);

        public abstract Task AddAsync(TEntity entity);

        public abstract void Update(TEntity entity);

        public abstract Task<bool> ContainsEntityWithId(int id);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                DbContext?.Dispose();
            }

            _disposed = true;
        }

        ~Repository()
        {
            Dispose(false);
        }
    }
}