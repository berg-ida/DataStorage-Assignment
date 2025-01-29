using Data.Entites;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface ITimeRepository
    {
        Task<bool> CreateAsync(TimeEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TimeEntity>> GetAllAsync();
        Task<TimeEntity?> GetAsync(Expression<Func<TimeEntity, bool>> expression);
        Task<bool> UpdateAsync(TimeEntity updatedEntity);
    }
}