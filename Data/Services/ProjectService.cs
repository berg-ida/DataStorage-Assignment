using Business.Models;
using Data.Contexts;
using Data.Dtos;
using Data.Factories;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public class ProjectService(IProjectRepository projectRepository, DataContext context) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly DbContext _context = context;

    //Create
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var project = ProjectFactory.Create(form);
        if (project == null)
        {
            Console.WriteLine("The project is null");
            return false;
        }

        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var result = await _projectRepository.CreateAsync(project);
            await _projectRepository.SaveAsync();
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error creating project :: {ex.Message}");
            return false;
        }
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
        var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            var project = await _projectRepository.GetAsync(x => x.Id == form.Id);
            if (project == null)
            {
                return null;
            }

            project = ProjectFactory.Create(project.Id, form);

            await _projectRepository.UpdateAsync(x => x.Id == form.Id, project);
            await _projectRepository.SaveAsync();
            await transaction.CommitAsync();

            return project != null ? ProjectFactory.Create(project) : null;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error updating project :: {ex.Message}");
            return null;
        }  
    }

    //Delete
    public async Task<bool> DeleteProjectAsync(int id)
    {
        var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var project = await _projectRepository.GetAsync(x => x.Id == id);
            if (project == null)
            {
                return false;
            }

            var result = await _projectRepository.DeleteAsync(x => x.Id == project.Id);
            await _projectRepository.SaveAsync();
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error deleting project :: {ex.Message}");
            return false;
        }
    }
}
