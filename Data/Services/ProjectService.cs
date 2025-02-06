using Business.Models;
using Data.Dtos;
using Data.Factories;
using Data.Interfaces;

namespace Data.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    //Create
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var project = ProjectFactory.Create(form);
        if (project == null)
        {
            Console.WriteLine("The project is null");
            return false;
        }

        var result = await _projectRepository.CreateAsync(project);
        return result;
    }

    //Read
    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();
        return projects.Select(ProjectFactory.Create);
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        var project = await _projectRepository.GetAsync(x => x.Id == id);
        return project != null ? ProjectFactory.Create(project) : null;
    }

    //Update 
    public async Task<Project?> UpdateProjectAsync(ProjectUpdateForm form)
    {
        var project = await _projectRepository.GetAsync(x => x.Id == form.Id);
        if (project == null)
        {
            return null;
        }

        project = ProjectFactory.Create(project.Id, form);

        await _projectRepository.UpdateAsync(project);

        project = await _projectRepository.GetAsync(x => x.Id == form.Id);
        return project != null ? ProjectFactory.Create(project) : null;
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
