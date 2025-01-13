using Ornit.Backend.src.Shared.ResultPattern;

namespace Ornit.Backend.src.Shared.Abstractions
{
    public interface IRepository<T> where T : IIdentityEntity
    {
        Task<Result<T>> GetById(int id);

        Task<Result<IEnumerable<T>>> GetAll();

        Task<Result> Create(T entity);

        Task<Result> Update(T entity);

        Task<Result> Delete(int id);
    }
}