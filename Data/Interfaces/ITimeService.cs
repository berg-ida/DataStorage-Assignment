using Business.Models;
using Data.Dtos;

namespace Data.Interfaces
{
    public interface ITimeService
    {
        Task<bool> CreateTimeAsync(TimeRegistrationForm form);
        Task<bool> DeleteTimeAsync(int id);
        Task<IEnumerable<Time>> GetTimesAsync();
        Task<Time?> UpdateTimeAsync(TimeUpdateForm form);
    }
}