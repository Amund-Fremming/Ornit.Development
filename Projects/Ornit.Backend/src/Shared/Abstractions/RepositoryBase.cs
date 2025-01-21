using Microsoft.EntityFrameworkCore;
using Ornit.Backend.src.Shared.AppData;
using Ornit.Backend.src.Shared.ResultPattern;

namespace Ornit.Backend.src.Shared.Abstractions
{
    public abstract class RepositoryBase<T>(ILogger<RepositoryBase<T>> logger, AppDbContext context) : IRepository<T> where T : class, IIdentity
    {
        public async Task<Result<T>> GetById(int id)
        {
            try
            {
                var entity = await context.Set<T>()
                    .FindAsync(id);

                if (entity != null)
                {
                    return entity;
                }
                return new Error($"{typeof(T)} with id {id}, does not exist.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetById");
                return new Error(ex.Message, ex);
            }
        }

        public async Task<Result<IEnumerable<T>>> GetAll()
        {
            try
            {
                return await context.Set<T>()
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetAll");
                return new Error(ex.Message, ex);
            }
        }

        public async Task<Result> Create(T entity)
        {
            try
            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Create");
                throw;
            }
        }

        public async Task<Result> Update(T entity)
        {
            try
            {
                context.Update(entity);
                await context.SaveChangesAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Update");
                return new Error(ex.Message, ex);
            }
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                var result = await GetById(id);
                if (result.IsError)
                {
                    return result.Error;
                }
                var entity = result.Data;
                context.Remove(entity);
                await context.SaveChangesAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Delete");
                return new Error(ex.Message, ex);
            }
        }
    }
}