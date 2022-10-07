using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using NetTestTask.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NetTestTask.DataAccess.Persistence.DBContexts
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly Dictionary<Type, object> _dbSets;

        public DatabaseFacade AppDatabase { get; set; }

        public AppDbContext([NotNull] DbContextOptions options,
            ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
            _dbSets = new Dictionary<Type, object>();
            AppDatabase = Database;
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class, IEntity
        {
            if (_dbSets.Keys.Contains(typeof(TEntity)))
                return _dbSets[typeof(TEntity)] as DbSet<TEntity>;

            var dbSet = this.Set<TEntity>();

            _dbSets.Add(typeof(TEntity), dbSet);

            return dbSet;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), x => !x.IsAbstract
                                                && !x.IsInterface
                                                && typeof(IEntityConfiguration).IsAssignableFrom(x));
        }

#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //optionsBuilder.LogTo((query) =>
            //    {
            //        File.AppendAllText("Query.txt", query);
            //    });
        }
#endif

        public void DisposeContext()
        {
            this.Dispose();
        }
    }
}
