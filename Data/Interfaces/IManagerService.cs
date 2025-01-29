using Business.Models;
using Data.Dtos;

namespace Data.Interfaces
{
    public interface IManagerService
    {
        Task<bool> CreateManagerAsync(ManagerRegistrationForm form);
        Task<bool> DeleteManagerAsync(int id);
        Task<IEnumerable<Manager>> GetManagersAsync();
        Task<Manager?> UpdateManagerAsync(ManagerUpdateForm form);
    }
}