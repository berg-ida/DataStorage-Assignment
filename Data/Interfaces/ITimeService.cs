using Business.Models;
using Data.Dtos;

namespace Data.Interfaces
{
    public interface ITimeService
    {
        Task<bool> CreateTimeAsync(TimeRegistrationForm form);
        Task<bool> DeleteTimeAsync(int id);
        Task<Time?> GetTimeByIdAsync(int id);
        Task<IEnumerable<Time>> GetTimePeriodsAsync();
        Task<Time?> UpdateTimeAsync(TimeUpdateForm form);
    }
}