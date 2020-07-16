using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MuchBlog.Domain.Services.IService
{
    public interface IRepositoryBase<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        
        Task<bool> SaveAsync();
    }
}