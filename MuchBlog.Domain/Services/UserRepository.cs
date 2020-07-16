using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Infrastructure.Data;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Domain.Services
{
    /// <summary>
    /// 使用RepositoryBase来实现仓储的公共部分，
    /// IUserRepository来扩展逻辑，和使用依赖注入框架
    /// </summary>
    public class UserRepository:RepositoryBase<User, Guid>,IUserRepository
    {
        private readonly DbContext _dbContext;

        public UserRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByAuthorizeAsync(string username, string password)
        {
            return await _dbContext.Set<User>()
                .Where(x => x.UserName.Equals(username) && x.Password.Equals(password))
                .SingleOrDefaultAsync();
        }
    }
}