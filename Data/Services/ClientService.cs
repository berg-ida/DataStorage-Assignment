using Business.Models;
using Data.Dtos;
using Data.Entites;
using Data.Interfaces;

namespace Data.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    //Create
    public async Task<bool> CreateClientAsync(ClientRegistrationForm form)
    {
        var client = new ClientEntity
        {
            CompanyName = form.CompanyName,
        };

        var result = await _clientRepository.CreateAsync(client);
        return result;
    }

    //Read
    public async Task<IEnumerable<Client>> GetClientsAsync()
    {
        var clients = await _clientRepository.GetAllAsync();
        return clients.Select(x => new Client(x.Id, x.CompanyName));
    }

    //Update 
    public async Task<Client?> UpdateClientAsync(ClientUpdateForm form)
    {
        var client = await _clientRepository.GetAsync(x => x.Id == form.Id);
        if (client == null)
        {
            return null;
        }

        client = new ClientEntity
        {
            CompanyName = form.CompanyName
        };

        await _clientRepository.UpdateAsync(client);

        client = await _clientRepository.GetAsync(x => x.Id == form.Id);
        return client != null ? new Client(client.Id, client.CompanyName) : null;
    }

    //Delete
    public async Task<bool> DeleteClientAsync(int id)
    {
        var client = await _clientRepository.GetAsync(x => x.Id == id);
        if (client == null)
        {
            return false;
        }

        var result = await _clientRepository.DeleteAsync(client.Id);
        return result;
    }
}

