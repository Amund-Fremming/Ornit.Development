namespace FeatureBasic.src.Shared.Abstractions
{
    public interface IRepository<TEntity> where TEntity : IIdentityEntity
    {
        Task<TEntity> GetByID(int id);

        Task<IEnumerable<TEntity>> GetAll();

        Task<int> Create(TEntity entity);

        Task<bool> Update(TEntity entity);

        Task<bool> Delete(int id);
    }
}