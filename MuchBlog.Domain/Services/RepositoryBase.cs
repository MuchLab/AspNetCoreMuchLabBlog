using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Domain.Services.IServices;

namespace MuchBlog.Domain.Services
{
    public abstract class RepositoryBase<TEntity, TId>
        : IRepositoryBase<TEntity>, IRepositoryBase2<TEntity, TId> where TEntity:class
    {
        private readonly DbContext _dbContext;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_dbContext
                .Set<TEntity>()
                .AsEnumerable());
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _dbContext.Set<TEntity>()
                .FindAsync(id);
        }

        public Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Task.FromResult(_dbContext
                .Set<TEntity>()
                .Where(expression)
                .AsEnumerable());
        }

        public void Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<bool> IsExistAsync(TId id)
        {
            return await _dbContext
                .Set<TEntity>()
                .FindAsync(id) != null;
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}