using FeatureBasic.src.Features.Data;

namespace FeatureBasic.src.Shared.Abstractions
{
    public abstract class RepositoryBase<TEntity, TClass>(ILogger<TClass> logger, AppDbContext context) : IRepository<TEntity> where TEntity : IIdentityEntity
    {
        public async Task<TEntity> GetByID(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetById");
                throw;
            }
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetAll");
                throw;
            }
        }

        public Task<int> Create(TEntity entity)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Create");
                throw;
            }
        }

        public Task<bool> Update(TEntity entity)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Update");
                throw;
            }
        }

        public Task<bool> Delete(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Delete");
                throw;
            }
        }
    }
}