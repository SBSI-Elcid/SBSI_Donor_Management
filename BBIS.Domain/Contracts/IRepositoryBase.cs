using System.Linq.Expressions;

namespace BBIS.Domain.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> FindOneByConditionAsync(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void AddRange(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
    }
}
