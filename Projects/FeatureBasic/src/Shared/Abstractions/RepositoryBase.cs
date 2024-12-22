namespace FeatureBasic.src.Shared.Abstractions
{
    public abstract class RepositoryBase<TEntity, TClass>(ILogger<TClass> logger, IAppDbContext context) : IRepository<TEntity> where TEntity : IIdentityEntity
    {
        public async Task<TEntity> GetByID(int id)
        {
            try
            {
                return await context.FindAsync(typeof(TEntity), id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetById");
            }
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetAll");
            }
        }

        public Task<int> Create(TEntity entity)
        {
            try
            {
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Create");
            }
        }

        public Task<bool> Update(TEntity entity)
        {
            try
            {
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Update");
            }
        }

        public Task<bool> Delete(int id)
        {
            try
            {
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Delete");
            }
        }
    }
}