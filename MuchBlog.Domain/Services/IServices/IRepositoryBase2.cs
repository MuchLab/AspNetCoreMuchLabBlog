using System.Threading.Tasks;

namespace MuchBlog.Domain.Services.IServices
{
    public interface IRepositoryBase2<TEntity, TId>
    {
        Task<bool> IsExistAsync(TId id);
        Task<TEntity> GetByIdAsync(TId id);
    }
}