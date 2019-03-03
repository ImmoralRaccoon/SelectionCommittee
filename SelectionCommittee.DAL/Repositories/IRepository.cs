using System;
using System.Linq;
using System.Threading.Tasks;
using SelectionCommittee.DAL.Entities.Base;

namespace SelectionCommittee.DAL.Repositories
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        Task<TEntity> GetAsync(int id);
        IQueryable<TEntity> GetAll();
        void Delete(int id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task<bool> ContainsEntityWithId(int id);
    }
}