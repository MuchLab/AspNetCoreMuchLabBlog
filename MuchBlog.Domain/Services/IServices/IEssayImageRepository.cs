using System;
using System.Threading.Tasks;
using MuchBlog.Domain.Services.IServices;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Domain.Services.IService
{
    public interface IEssayImageRepository:IRepositoryBase<EssayImage>, IRepositoryBase2<EssayImage, Guid>
    {
        Task<EssayImage> GetByIdAsync(Guid userId, Guid essayId, Guid essayImageId);
    }
}