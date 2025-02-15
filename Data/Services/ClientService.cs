using Business.Models;
using Data.Dtos;
using Data.Factories;
using Data.Interfaces;

namespace Data.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    //Create
    public async Task<bool> CreateClientAsync(ClientRegistrationForm form)
    {
        var client = ClientFactory.Create(form);
        if (client == null)
        {
            Console.WriteLine("The client object is null");
            return false;
        }

        var result = await _clientRepository.CreateAsync(client);
        return result;
    }

    //Read
    public async Task<IEnumerable<Client>> GetClientsAsync()
    {
        var clients = await _clientRepository.GetAllAsync();
        return clients.Select(ClientFactory.Create);
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        var client = await _clientRepository.GetAsync(x => x.Id == id);
        return client != null ? ClientFactory.Create(client) : null;
    }

    //Update 
    public async Task<Client?> UpdateClientAsync(ClientUpdateForm form)
    {
        var client = await _clientRepository.GetAsync(x => x.Id == form.Id);
        if (client == null)
        {
            return null;
        }

        client.CompanyName = form.CompanyName;
        
        var result = await _clientRepository.UpdateAsync(x => x.Id == form.Id, client);
        var updatedClient = await _clientRepository.GetAsync(x => x.Id == form.Id);
        return updatedClient != null ? ClientFactory.Create(updatedClient) : null; 
    }

    //Delete
    public async Task<bool> DeleteClientAsync(int id)
    {
        var client = await _clientRepository.GetAsync(x => x.Id == id);
        if (client == null)
        {
            return false;
        }

        var result = await _clientRepository.DeleteAsync(x => x.Id == client.Id);
        return result;
    }
}

