using Business.Models;
using Data.Contexts;
using Data.Dtos;
using Data.Factories;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public class ManagerService(IManagerRepository managerRepository, DataContext context) : IManagerService
{
    private readonly IManagerRepository _managerRepository = managerRepository;
    private readonly DbContext _context = context;

    //Create
    public async Task<bool> CreateManagerAsync(ManagerRegistrationForm form)
    {
        var manager = ManagerFactory.Create(form);
        if (manager == null)
        {
            Console.WriteLine("The manager is null");
            return false;
        }

        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var result = await _managerRepository.CreateAsync(manager);
            await _managerRepository.SaveAsync();
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error creating manager :: {ex.Message}");
            return false;
        }
    }

    //Read
    public async Task<IEnumerable<Manager>> GetManagersAsync()
    {
        var managers = await _managerRepository.GetAllAsync();
        return managers.Select(ManagerFactory.Create);
    }

    public async Task<Manager?> GetManagerByIdAsync(int id)
    {
        var manager = await _managerRepository.GetAsync(x => x.Id == id);
        return manager != null ? ManagerFactory.Create(manager) : null;
    }

    //Update 
    public async Task<Manager?> UpdateManagerAsync(ManagerUpdateForm form)
    {
        var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var manager = await _managerRepository.GetAsync(x => x.Id == form.Id);
            if (manager == null)
            {
                return null;
            }

            manager.FirstName = form.FirstName;
            manager.LastName = form.LastName;

            manager = ManagerFactory.Create(manager.Id, form);

            await _managerRepository.UpdateAsync(x => x.Id == form.Id, manager);
            await _managerRepository.SaveAsync();
            await transaction.CommitAsync();

            return manager != null ?  ManagerFactory.Create(manager) : null;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error updating manager :: {ex.Message}");
            return null;
        }
    }

    //Delete
    public async Task<bool> DeleteManagerAsync(int id)
    {
        var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var manager = await _managerRepository.GetAsync(x => x.Id == id);
            if (manager == null)
            {
                return false;
            }

            var result = await _managerRepository.DeleteAsync(x => x.Id == manager.Id);
            await _managerRepository.SaveAsync();
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error deleting manager :: {ex.Message}");
            return false;
        }
    }
}
