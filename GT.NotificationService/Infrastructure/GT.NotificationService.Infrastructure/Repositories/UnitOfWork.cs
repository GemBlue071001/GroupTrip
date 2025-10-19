using GT.NotificationService.Infrastructure.Base;
using GT.NotificationService.Infrastructure.Interface;
using Microsoft.Extensions.Configuration;
using GT.NotificationService.Domain.Base;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace GT.NotificationService.Infrastructure.Repositories
{
    public class UnitOfWork(GroupTripDbContext dbContext, IConfiguration configuration) : IUnitOfWork
    {
        private bool disposed = false;
        private readonly GroupTripDbContext _dbContext = dbContext;


        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();
        }

    }
}

