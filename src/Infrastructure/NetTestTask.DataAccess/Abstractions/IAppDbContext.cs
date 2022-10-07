using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using NetTestTask.Domain.Abstraction.DataAccess;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NetTestTask.DataAccess.Abstractions
{
    public interface IAppDbContext
    {
        ChangeTracker ChangeTracker { get; }

        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;

        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class, IEntity;

        int SaveChanges();

        void DisposeContext();

        DatabaseFacade AppDatabase { get; set; }
    }
}
