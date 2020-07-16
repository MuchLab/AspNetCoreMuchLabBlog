using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Domain.Services.IServices
{
    public interface IClassificationRepository:IRepositoryBase<Classification>, IRepositoryBase2<Classification, Guid>
    {
        Task<IEnumerable<Classification>> GetAllAsync(Guid essayId);
        void SelectClassification(IEnumerable<EssayClassification> addList, IEnumerable<EssayClassification> deleteList);
        Task<IEnumerable<Classification>> FindClassificationsByName(string[] className);
        bool EssayClassificationExist(Guid essayId, Guid classificationId);
    }
}