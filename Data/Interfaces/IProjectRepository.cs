using Data.Entites;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IProjectRepository
    {
        Task<bool> CreateAsync(ProjectEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ProjectEntity>> GetAllAsync();
        Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression);
        Task<bool> UpdateAsync(ProjectEntity updatedEntity);
    }
}