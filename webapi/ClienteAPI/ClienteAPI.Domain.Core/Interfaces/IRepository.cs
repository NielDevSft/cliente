using ClienteAPI.Domain.Core.Models;
using System.Linq.Expressions;

namespace ClienteAPI.Domain.Core.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        Task Add(TEntity obj);
        Task Update(TEntity obj);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params string[] includes);
        Task<List<TEntity>> FindAll(params string[] includes);
        Task<List<TEntity>> FindAllWhere(Expression<Func<TEntity, bool>> predicate, params string[] includes);
        Task Remove(Guid uuid);
        Task Remove(TEntity obj);
        Task<int> SaveChanges();
        Task<TEntity> GetById(Guid uuid, params string[] includes);
    }
}

