using FeatureResult.src.Shared.AppData;
using Microsoft.EntityFrameworkCore;
using NucleusResults.Core;

namespace FeatureResult.src.Shared.Abstractions
{
    public abstract class RepositoryBase<TEntity, TClass>(ILogger<TClass> logger, AppDbContext context) : IRepository<TEntity> where TEntity : class, IIdentityEntity
    {
        public async Task<NucleusResult<TEntity>> GetByID(int id)
        {
            try
            {
                var entity = await context.Set<TEntity>()
                    .FindAsync(id);

                if (entity != null)
                {
                    return entity;
                }
                return new Error(new KeyNotFoundException(""), $"{typeof(TEntity)} with id {id}, does not exist.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetById");
                return new Error(ex, ex.Message);
            }
        }

        public async Task<NucleusResult<IEnumerable<TEntity>>> GetAll()
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
                return new Error(ex, ex.Message);
            }
        }

        public async Task<NucleusResult> Create(TEntity entity)
        {
            try
            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
                return NucleusResult.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Create");
                throw;
            }
        }

        public async Task<NucleusResult> Update(TEntity entity)
        {
            try
            {
                context.Update(entity);
                await context.SaveChangesAsync();
                return NucleusResult.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Update");
                return new Error(ex, ex.Message);
            }
        }

        public async Task<NucleusResult> Delete(int id)
        {
            try
            {
                var result = await GetByID(id);
                if (result.IsError)
                {
                    return result.Error;
                }
                var entity = result.Data;
                context.Remove(entity);
                await context.SaveChangesAsync();
                return NucleusResult.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Delete");
                return new Error(ex, ex.Message);
            }
        }
    }
}