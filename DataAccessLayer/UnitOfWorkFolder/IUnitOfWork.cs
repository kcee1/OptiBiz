using DataAccessLayer.IRepositories;

namespace DataAccessLayer.UnitOfWorkFolder
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Commit();
        void BeginTransaction();
        void Rollback();

        int SaveChanges();
        Task<int> SaveAsync();

    }


    public interface IUnitOfWork<TContext> : IUnitOfWork
    {

    }
}
