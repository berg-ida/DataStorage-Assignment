using Business.Models;
using Data.Dtos;
using Data.Entites;
using Data.Interfaces;
using Data.Repositories;

namespace Data.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    //Create
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var timeEntity = new TimeEntity
        {
            StartDay = form.TimePeriod.StartDay,
            StartMonth = form.TimePeriod.StartMonth,
            StartYear = form.TimePeriod.StartYear,
            EndDay = form.TimePeriod.EndDay,
            EndMonth = form.TimePeriod.EndMonth,
            EndYear = form.TimePeriod.EndYear,
        };

        var managerEntity = new ManagerEntity
        {
            FirstName = form.ProjectManager.FirstName,
            LastName = form.ProjectManager.LastName,
        };

        var clientEntity = new ClientEntity
        {
            CompanyName = form.Client.CompanyName,
        };

        var project = new ProjectEntity
        {
            Name = form.Name,
            ServiceName = form.ServiceName,
            Price = form.Price,
            Status = form.Status,
            TimePeriod = timeEntity,
            ProjectManager = managerEntity,
            Client = clientEntity
        };

        var result = await _projectRepository.CreateAsync(project);
        return result;
    }

    //Read
    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();
        return projects.Select(x => new Project(x.Id, x.Name, x.ServiceName, x.Price, x.Status,
            new Time
            {
                Id = x.TimePeriod.Id,
                StartDay = x.TimePeriod.StartDay,
                StartMonth = x.TimePeriod.StartMonth,
                StartYear = x.TimePeriod.StartYear,
                EndDay = x.TimePeriod.EndDay,
                EndMonth = x.TimePeriod.EndMonth,
                EndYear = x.TimePeriod.EndYear,
            },
            new Manager
            {
                Id = x.ProjectManager.Id,
                FirstName = x.ProjectManager.FirstName,
                LastName = x.ProjectManager.LastName,
            },
            new Client
            {
                Id = x.Client.Id,
                CompanyName = x.Client.CompanyName,
            }
            ));
    }

    //Update 
    public async Task<Project?> UpdateProjectAsync(ProjectUpdateForm form)
    {
        var project = await _projectRepository.GetAsync(x => x.Id == form.Id);
        if (project == null)
        {
            return null;
        }

        var timeEntity = new TimeEntity
        {
            StartDay = form.TimePeriod.StartDay,
            StartMonth = form.TimePeriod.StartMonth,
            StartYear = form.TimePeriod.StartYear,
            EndDay = form.TimePeriod.EndDay,
            EndMonth = form.TimePeriod.EndMonth,
            EndYear = form.TimePeriod.EndYear,
        };

        var managerEntity = new ManagerEntity
        {
            FirstName = form.ProjectManager.FirstName,
            LastName = form.ProjectManager.LastName,
        };

        var clientEntity = new ClientEntity
        {
            CompanyName = form.Client.CompanyName,
        };

        project = new ProjectEntity
        {
            Name = form.Name,
            ServiceName = form.ServiceName,
            Price = form.Price,
            Status = form.Status,
            TimePeriod = timeEntity,
            ProjectManager = managerEntity,
            Client = clientEntity
        };

        await _projectRepository.UpdateAsync(project);

        project = await _projectRepository.GetAsync(x => x.Id == form.Id);
        return project != null ? new Project(project.Id, project.Name, project.ServiceName, project.Price, project.Status,
            new Time
            (
                project.TimePeriod.Id,
                project.TimePeriod.StartDay,
                project.TimePeriod.StartMonth,
                project.TimePeriod.StartYear,
                project.TimePeriod.EndDay,
                project.TimePeriod.EndMonth,
                project.TimePeriod.EndYear
            ),
            new Manager
            (
                project.ProjectManager.Id,
                project.ProjectManager.FirstName,
                project.ProjectManager.LastName
            ),
            new Client
            (
                project.Client.Id,
                project.Client.CompanyName
            )) : null;
    }

    //Delete
    public async Task<bool> DeleteProjectAsync(int id)
    {
        var project = await _projectRepository.GetAsync(x => x.Id == id);
        if (project == null)
        {
            return false;
        }

        var result = await _projectRepository.DeleteAsync(project.Id);
        return result;
    }


}
