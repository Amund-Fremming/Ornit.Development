using NucleusResults.Core;

namespace FeatureResult.src.Shared.Abstractions
{
    public interface IRepository<T> where T : IIdentityEntity
    {
        Task<NucleusResult<T>> GetByID(int id);

        Task<NucleusResult<IEnumerable<T>>> GetAll();

        Task<NucleusResult> Create(T entity);

        Task<NucleusResult> Update(T entity);

        Task<NucleusResult> Delete(int id);
    }
}