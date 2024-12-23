using FeatureBasic.src.Shared.AppData;
using Microsoft.EntityFrameworkCore;

namespace FeatureBasic.src.Shared.Abstractions
{
    public abstract class RepositoryBase<TEntity, TClass>(ILogger<TClass> logger, AppDbContext context) : IRepository<TEntity> where TEntity : class, IIdentityEntity
    {
        public async Task<TEntity?> GetByID(int id)
        {
            try
            {
                return await context.Set<TEntity>()
                    .FindAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetById");
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                return await context.Set<TEntity>()
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetAll");
                throw;
            }
        }

        public async Task<int> Create(TEntity entity)
        {
            try
            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
                return entity.ID;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Create");
                throw;
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            try
            {
                context.Update(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Update");
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                TEntity? entity = await GetByID(id);
                if (entity == null)
                {
                    return false;
                }
                context.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Delete");
                return false;
            }
        }
    }
}