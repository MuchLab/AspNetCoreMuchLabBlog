using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MuchBlog.Domain.Helpers;
using MuchBlog.Domain.Helpers.Parameters;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Domain.Services.IServices
{
    public interface IEssayRepository:IRepositoryBase<Essay>, IRepositoryBase2<Essay, Guid>
    {
        Task<IEnumerable<Essay>> GetEssaysAsync(Guid userId);
        Task<Essay> GetByIdAsync(Guid userId, Guid essayId);
        Task<PageList<Essay>> GetAll(EssayResourceParameters parameters);
        Task<PageList<Essay>> GetEssays(Guid userId, EssayResourceParameters parameters);
        int EssaysCount();
        int EssaysCount(Guid userId);
        int EssaysCountByClassification(Guid classificationId);
        Task<IEnumerable<Essay>> GetEssaysByClassificationId(Guid classificationId);
    }
}