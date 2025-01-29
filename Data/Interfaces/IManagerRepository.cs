using Data.Entites;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IManagerRepository
    {
        Task<bool> CreateAsync(ManagerEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ManagerEntity>> GetAllAsync();
        Task<ManagerEntity?> GetAsync(Expression<Func<ManagerEntity, bool>> expression);
        Task<bool> UpdateAsync(ManagerEntity updatedEntity);
    }
}