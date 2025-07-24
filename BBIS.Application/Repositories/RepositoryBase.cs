using BBIS.Database;
using BBIS.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BBIS.Application.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public readonly BBDbContext dbContext;
        public RepositoryBase(BBDbContext bloodbankDbContext) 
        {
            dbContext = bloodbankDbContext; 
        }
              
        public void Create(T entity) => dbContext.Set<T>().Add(entity);
        public void Update(T entity) => dbContext.Set<T>().Update(entity);
        public void Delete(T entity) => dbContext.Set<T>().Remove(entity);

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await dbContext.Set<T>()
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> FindOneByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await dbContext.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);
        }

        public void AddRange(List<T> entities)
        {
            dbContext.Set<T>().AddRange(entities);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbContext.Set<T>().RemoveRange(entities);
        }
    }
}
