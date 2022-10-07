using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using NetTestTask.DataAccess.Abstractions;
using NetTestTask.DataAccess.Persistence.DBContexts;
using NetTestTask.DataAccess.Persistence.Repositories;

namespace NetTestTask.DataAccess.Persistence.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IAppDbContext _appDbContext;
        private bool _isDisposed = false;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork([NotNull] DbContextOptions options,
            ILoggerFactory loggerFactory)
        {
            _repositories = new Dictionary<Type, object>();
            _appDbContext = new AppDbContext(options, loggerFactory);
        }


        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;

            var repo = new GenericRepository<TEntity>(_appDbContext);

            _repositories.Add(typeof(TEntity), repo);

            return repo;
        }


        public void Commit()
        {  
            _appDbContext.SaveChanges();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _appDbContext.DisposeContext();
                _isDisposed = true;
            }
        }


        private TRepository GetRepository<TRepository>()
        {
            if (_repositories.Keys.Contains(typeof(TRepository)))
                return (TRepository)_repositories[typeof(TRepository)];

            var type = Assembly.GetExecutingAssembly().GetTypes()
               .FirstOrDefault(x => !x.IsAbstract
               && !x.IsInterface
               && x.Name == typeof(TRepository).Name.Substring(1));

            if (type == null)
                throw new KeyNotFoundException("Repository type is not found");

            var repository = (TRepository)Activator.CreateInstance(type, _appDbContext);

            _repositories.Add(typeof(TRepository), repository);

            return repository;
        }


    }
}
