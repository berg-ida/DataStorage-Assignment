using Business.Models;
using Data.Dtos;
using Data.Factories;
using Data.Interfaces;

namespace Data.Services;

public class ManagerService(IManagerRepository managerRepository) : IManagerService
{
    private readonly IManagerRepository _managerRepository = managerRepository;

    //Create
    public async Task<bool> CreateManagerAsync(ManagerRegistrationForm form)
    {
        var manager = ManagerFactory.Create(form);
        if (manager == null)
        {
            Console.WriteLine("The manager object is null");
            return false;
        }

        var result = await _managerRepository.CreateAsync(manager);
        return result;
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
        var manager = await _managerRepository.GetAsync(x => x.Id == form.Id);
        if (manager == null)
        {
            return null;
        }

        manager.FirstName = form.FirstName;
        manager.LastName = form.LastName;

        var result = await _managerRepository.UpdateAsync(manager);

        if (result)
        {
            var updatedManager = await _managerRepository.GetAsync(x => x.Id == form.Id);
            return updatedManager != null ? ManagerFactory.Create(updatedManager) : null;
        }
        
        return null;
    }

    //Delete
    public async Task<bool> DeleteManagerAsync(int id)
    {
        var manager = await _managerRepository.GetAsync(x => x.Id == id);
        if (manager == null)
        {
            return false;
        }

        var result = await _managerRepository.DeleteAsync(manager.Id);
        return result;
    }
}
