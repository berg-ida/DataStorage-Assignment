using Business.Models;
using Data.Dtos;

namespace Data.Interfaces
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
        Task<bool> DeleteProjectAsync(int id);
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project?> UpdateProjectAsync(ProjectUpdateForm form);
    }
}