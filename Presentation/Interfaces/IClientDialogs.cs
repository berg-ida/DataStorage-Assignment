namespace Presentation.Interfaces
{
    public interface IClientDialogs
    {
        Task CreateClientOption();
        Task DeleteClientOption();
        void ExitApplicationOption();
        Task MenuOptions();
        Task UpdateClientOption();
        Task ViewAllClientsOption();
    }
}