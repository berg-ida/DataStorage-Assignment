using Business.Models;
using Data.Dtos;

namespace Data.Interfaces
{
    public interface IClientService
    {
        Task<bool> CreateClientAsync(ClientRegistrationForm form);
        Task<bool> DeleteClientAsync(int id);
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client?> UpdateClientAsync(ClientUpdateForm form);
    }
}