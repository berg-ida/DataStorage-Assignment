namespace Presentation.Interfaces
{
    public interface IProjectDialogs
    {
        Task CreateProjectOption();
        Task DeleteProjectOption();
        void ExitApplicationOption();
        Task MenuOptions();
        Task UpdateProjectOption();
        Task ViewAllProjectsOption();
    }
}