using DataAccessLayer.IRepositories;
using DataAccessLayer.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;

namespace DataAccessLayer.UnitOfWorkFolder
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable
         where TContext : DbContext
    {
        private SqlConnection connection;
        //private SqlTransaction transaction;
        private IDbContextTransaction transaction;
        private bool disposed;
        private readonly TContext _context;
        private string _errorMessage = string.Empty;
        private Dictionary<string, object> _repositories;
        private readonly ILogger<IUnitOfWork> _logger;

        public UnitOfWork(TContext context)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                //_logger.LogError("No internet connection available.");
                throw new Exception("No internet connection available.");
            }
            _context = context;
            // _logger = logger;
            try
            {
                //using (var transaction = _context.Database.BeginTransaction())
                {
                    //connection.Open();
                    //transaction = connection.BeginTransaction();
                    disposed = false;
                }
            }
            catch (Exception ex)
            {
                //\_logger.LogError(ex, "Error occurred while initializing UnitOfWork.");
                throw new Exception("Error occurred while initializing UnitOfWork.", ex);
            }
        }
        public void BeginTransaction()
        {
            if (transaction != null)
            {
                return;
            }
            transaction = _context.Database.BeginTransaction();
        }
        public bool IsDatabaseServerAvailable(string connectionString)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                string serverName = builder.DataSource;
                Ping ping = new Ping();
                PingReply reply = ping.Send(serverName);

                return reply.Status == IPStatus.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking database server availability: {ex.Message}");
                return false;
            }
        }

        // Add your repository methods here
        public IRepository<T> GetRepository<T>() where T : class

        {
            if (_repositories == null)
                _repositories = new Dictionary<string, object>();
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {

                var repositoryInstance = new BaseRepository<T>((DbContext)_context);
                //Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

                //var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(new Type[] { typeof(T) }), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (BaseRepository<T>)_repositories[type];
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch
            {
                // An error occurred, rollback the transaction
                //transaction.Rollback();
                throw;
            }
            finally
            {
                // Dispose resources
                transaction.Dispose();
                // connection.Dispose();
            }
        }

        public void Rollback()
        {
            transaction.Rollback();
            transaction.Dispose();
            // connection.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    if (_repositories != null)
                    {
                        foreach (var repository in _repositories)
                        {
                            (repository.Value as IDisposable)?.Dispose();
                        }
                        _repositories.Clear();
                        _repositories = null;
                    }


                    if (_context != null)
                    {
                        _context.Dispose();
                    }
                }

                // Dispose unmanaged resources
                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }
                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }

                disposed = true;
            }
        }

    }
}
