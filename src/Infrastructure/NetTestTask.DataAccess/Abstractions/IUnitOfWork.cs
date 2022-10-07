namespace NetTestTask.DataAccess.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;

        void Commit();
    }
}
