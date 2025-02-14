namespace Presentation.Interfaces
{
    public interface IManagerDialogs
    {
        Task CreateManagerOption();
        Task DeleteManagerOption();
        void ExitApplicationOption();
        Task MenuOptions();
        Task UpdateManagerOption();
        Task ViewAllManagersOption();
    }
}