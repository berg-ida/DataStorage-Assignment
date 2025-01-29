using Data.Entites;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IClientRepository
    {
        Task<bool> CreateAsync(ClientEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ClientEntity>> GetAllAsync();
        Task<ClientEntity?> GetAsync(Expression<Func<ClientEntity, bool>> expression);
        Task<bool> UpdateAsync(ClientEntity updatedEntity);
    }
}