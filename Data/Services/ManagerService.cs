using Business.Models;
using Data.Dtos;
using Data.Entites;
using Data.Interfaces;

namespace Data.Services;

public class ManagerService(IManagerRepository managerRepository) : IManagerService
{
    private readonly IManagerRepository _managerRepository = managerRepository;

    //Create
    public async Task<bool> CreateManagerAsync(ManagerRegistrationForm form)
    {
        var manager = new ManagerEntity
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
        };

        var result = await _managerRepository.CreateAsync(manager);
        return result;
    }

    //Read
    public async Task<IEnumerable<Manager>> GetManagersAsync()
    {
        var projectManagers = await _managerRepository.GetAllAsync();
        return projectManagers.Select(x => new Manager(x.Id, x.FirstName, x.LastName));
    }

    //Update 
    public async Task<Manager?> UpdateManagerAsync(ManagerUpdateForm form)
    {
        var manager = await _managerRepository.GetAsync(x => x.Id == form.Id);
        if (manager == null)
        {
            return null;
        }

        manager = new ManagerEntity
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
        };

        await _managerRepository.UpdateAsync(manager);

        manager = await _managerRepository.GetAsync(x => x.Id == form.Id);
        return manager != null ? new Manager(manager.Id, manager.FirstName, manager.LastName) : null;
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
