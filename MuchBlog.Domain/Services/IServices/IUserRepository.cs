using System;
using System.Threading.Tasks;
using MuchBlog.Domain.Services.IServices;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Domain.Services.IService
{
    public interface IUserRepository:IRepositoryBase<User>, IRepositoryBase2<User, Guid>
    {
        Task<User> GetUserByAuthorizeAsync(string username, string password);
    }
}