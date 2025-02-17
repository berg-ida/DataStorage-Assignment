using Business.Models;
using Data.Contexts;
using Data.Dtos;
using Data.Factories;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public class ClientService(IClientRepository clientRepository, DataContext context) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly DbContext _context = context;

    //Create
    public async Task<bool> CreateClientAsync(ClientRegistrationForm form)
    {
        var client = ClientFactory.Create(form);
        if (client == null)
        {
            Console.WriteLine("The client is null");
            return false;
        }

        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var result = await _clientRepository.CreateAsync(client);
            await _clientRepository.SaveAsync();
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error creating client :: {ex.Message}");
            return false;
        }
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
        var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var client = await _clientRepository.GetAsync(x => x.Id == form.Id);
            if (client == null)
            {
                return null;
            }

            client.CompanyName = form.CompanyName;

            client = ClientFactory.Create(client.Id, form);

            await _clientRepository.UpdateAsync(x => x.Id == form.Id, client);
            await _clientRepository.SaveAsync();
            await transaction.CommitAsync();

            return client != null ? ClientFactory.Create(client) : null;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error updating client :: {ex.Message}");
            return null;
        }
    }

    //Delete
    public async Task<bool> DeleteClientAsync(int id)
    {
        var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var client = await _clientRepository.GetAsync(x => x.Id == id);
            if (client == null)
            {
                return false;
            }

            var result = await _clientRepository.DeleteAsync(x => x.Id == client.Id);
            await _clientRepository.SaveAsync();
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error deleting client :: {ex.Message}");
            return false;
        }
    }
}

