using Data.Interfaces;
using Business.Models;
using Data.Dtos;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Data.Entites;

namespace Data.Services;

public class TimeService(ITimeRepository timeRepository) : ITimeService
{
    private readonly ITimeRepository _timeRepository = timeRepository;

    //Create
    public async Task<bool> CreateTimeAsync(TimeRegistrationForm form)
    {
        var time = new TimeEntity
        {
            StartDay = form.StartDay,
            StartMonth = form.StartMonth,
            StartYear = form.StartYear,
            EndDay = form.EndDay,
            EndMonth = form.EndMonth,
            EndYear = form.EndYear,
        };

        var result = await _timeRepository.CreateAsync(time);
        return result;
    }

    //Read
    public async Task<IEnumerable<Time>> GetTimesAsync()
    {
        var timePeriods = await _timeRepository.GetAllAsync();
        return timePeriods.Select(x => new Time(x.Id, x.StartDay, x.StartMonth, x.StartYear, x.EndDay, x.EndMonth, x.EndYear));
    }

    //Update 
    public async Task<Time?> UpdateTimeAsync(TimeUpdateForm form)
    {
        var time = await _timeRepository.GetAsync(x => x.Id == form.Id);
        if (time == null)
        {
            return null;
        }

        time = new TimeEntity
        {
            StartDay = form.StartDay,
            StartMonth = form.StartMonth,
            StartYear = form.StartYear,
            EndDay = form.EndDay,
            EndMonth = form.EndMonth,
            EndYear = form.EndYear,
        };

        await _timeRepository.UpdateAsync(time);

        time = await _timeRepository.GetAsync(x => x.Id == form.Id);
        return time != null ? new Time(time.Id, time.StartDay, time.StartMonth, time.StartYear, time.EndDay, time.EndMonth, time.EndYear) : null;
    }

    //Delete
    public async Task<bool> DeleteTimeAsync(int id)
    {
        var time = await _timeRepository.GetAsync(x => x.Id == id);
        if (time == null)
        {
            return false;
        }

        var result = await _timeRepository.DeleteAsync(time.Id);
        return result;
    }
}
