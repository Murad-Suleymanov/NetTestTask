using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NetTestTask.DataAccess.Abstractions;
using NetTestTask.Domain.Abstraction.DataAccess;

namespace NetTestTask.DataAccess.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbSet<TEntity> _dbset;
        protected readonly IAppDbContext _appDbContext;

        public GenericRepository(IAppDbContext appDbContext)
        {
            _dbset = appDbContext.GetDbSet<TEntity>();
            _appDbContext = appDbContext;
        }

        public void Add(TEntity entity)
        {
            _dbset.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbset.AddRange(entities);
        }

        public void AddOrUpdate(TEntity entity)
        {
            _dbset.Update(entity);
        }

        public void AddOrUpdate(TEntity entity, Func<TEntity, bool> predicate)
        {
            var old = _dbset.SingleOrDefault(predicate);

            if (old == null)
                Add(entity);
            else
                Update(entity, predicate);
        }

        public void AddOrUpdateRange(IEnumerable<TEntity> entities)
        {
            _dbset.UpdateRange(entities);
        }

        public void Update(TEntity entity, Func<TEntity, bool> predicate)
        {
            var old = _dbset.Single(predicate);

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();
            var oldType = old.GetType();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(entity);
                var oldPropertyValue = oldType.GetProperty(property.Name).GetValue(old);
                if (!AreEqual(propertyValue, GetDefaultValue(property.PropertyType))
                  && !AreEqual(propertyValue, oldPropertyValue)
                  )
                {
                    oldType.GetProperty(property.Name).SetValue(old, propertyValue);
                }
            }
        }

        public void Attach(TEntity entity)
        {
            _dbset.Attach(entity);
        }

        public void AttachRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Attach(entity);
        }

        public bool CheckExist(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return _dbset.Any();

            return _dbset.Any(predicate);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return _dbset.Count();

            return _dbset.Count(predicate);
        }

        public void Delete(TEntity entity)
        {
            _dbset.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _dbset.RemoveRange(entities);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAll(predicate))
                Delete(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.SingleOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return GetAsQueryable().ToList();

            return GetAsQueryable().Where(predicate).ToList();
        }

        public IQueryable<TEntity> GetAsQueryable()
        {
            return _dbset;
        }

        public void Reload(TEntity entity)
        {
            _appDbContext.Entry<TEntity>(entity).Reload();
        }


        private static object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        private static bool AreEqual(object obj1, object obj2)
        {
            return (obj1 == null && obj2 == null) || (obj1 != null && obj1.Equals(obj2));
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return Task<IEnumerable<TEntity>>.Run(() =>
            {
                return GetAll();
            });
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.SingleOrDefaultAsync(predicate);

        }

        public Task ReloadAsync(TEntity entity)
        {
            return _appDbContext.Entry<TEntity>(entity).ReloadAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbset.AddAsync(entity);
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return _dbset.AddRangeAsync(entities);
        }

        public Task AddOrUpdateAsync(TEntity entity)
        {
            return Task.Run(() =>
            {
                AddOrUpdate(entity);
            });
        }

        public Task AddOrUpdateAsync(TEntity entity, Func<TEntity, bool> predicate)
        {
            return Task.Run(() =>
            {
                var old = _dbset.SingleOrDefault(predicate);

                if (old == null)
                    Add(entity);
                else
                    Update(entity, predicate);
            });
        }

        public Task AddOrUpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            return Task.Run(() =>
            {
                AddOrUpdateRange(entities);
            });
        }

        public Task UpdateAsync(TEntity entity, Func<TEntity, bool> predicate)
        {
            return Task.Run(() =>
            {
                var old = _dbset.Single(predicate);

                var entityType = entity.GetType();
                var properties = entityType.GetProperties();
                var oldType = old.GetType();
                foreach (var property in properties)
                {
                    var propertyValue = property.GetValue(entity);
                    var oldPropertyValue = oldType.GetProperty(property.Name).GetValue(old);
                    if (!AreEqual(propertyValue, GetDefaultValue(property.PropertyType))
                      && !AreEqual(propertyValue, oldPropertyValue)
                      )
                    {
                        oldType.GetProperty(property.Name).SetValue(old, propertyValue);
                    }
                }
            });
        }

        public Task DeleteAsync(TEntity entity)
        {
            return Task.Run(() =>
            {
                Delete(entity);
            });
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() =>
            {
                Delete(predicate);
            });
        }

        public Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            return Task.Run(() =>
            {
                Delete(entities);
            });
        }

        public Task CheckExistAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return _dbset.AnyAsync();

            return _dbset.AnyAsync(predicate);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return _dbset.CountAsync();

            return _dbset.CountAsync(predicate);
        }

        public void DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _dbset.Where(predicate);
            _dbset.RemoveRange(query);
        }
        public Task DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() =>
            {
                IQueryable<TEntity> query = _dbset.Where(predicate);
                _dbset.RemoveRange(query);
            });
        }
    }
}
