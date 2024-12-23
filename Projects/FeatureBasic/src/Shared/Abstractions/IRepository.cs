namespace FeatureBasic.src.Shared.Abstractions
{
    public interface IRepository<T> where T : IIdentityEntity
    {
        Task<T?> GetByID(int id);

        Task<IEnumerable<T>> GetAll();

        Task<int> Create(T entity);

        Task<bool> Update(T entity);

        Task<bool> Delete(int id);
    }
}