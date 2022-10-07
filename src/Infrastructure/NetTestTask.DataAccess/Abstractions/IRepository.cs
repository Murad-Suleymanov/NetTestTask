using System.Linq.Expressions;
using NetTestTask.Domain.Abstraction.DataAccess;

namespace NetTestTask.DataAccess.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAsQueryable();

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        void Reload(TEntity entity);

        Task ReloadAsync(TEntity entity);

        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void AddOrUpdate(TEntity entity);

        Task AddOrUpdateAsync(TEntity entity);

        void AddOrUpdate(TEntity entity, Func<TEntity, bool> predicate);

        Task AddOrUpdateAsync(TEntity entity, Func<TEntity, bool> predicate);

        void AddOrUpdateRange(IEnumerable<TEntity> entities);

        Task AddOrUpdateRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity, Func<TEntity, bool> predicate);

        Task UpdateAsync(TEntity entity, Func<TEntity, bool> predicate);

        void Attach(TEntity entity);

        void AttachRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        void DeleteRange(Expression<Func<TEntity, bool>> predicate);

        Task DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        void Delete(IEnumerable<TEntity> entities);

        Task DeleteAsync(IEnumerable<TEntity> entities);

        bool CheckExist(Expression<Func<TEntity, bool>> predicate = null);

        Task CheckExistAsync(Expression<Func<TEntity, bool>> predicate = null);

        int Count(Expression<Func<TEntity, bool>> predicate = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

    }
}
