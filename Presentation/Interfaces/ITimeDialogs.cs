namespace Presentation.Interfaces
{
    public interface ITimeDialogs
    {
        Task CreateTimePeriodOption();
        Task DeleteTimePeriodOption();
        void ExitApplicationOption();
        Task MenuOptions();
        Task UpdateTimePeriodOption();
        Task ViewAllTimePeriodsOption();
    }
}