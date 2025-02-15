using Data.Interfaces;
using Business.Models;
using Data.Dtos;
using Data.Factories;

namespace Data.Services;

public class TimeService(ITimeRepository timeRepository) : ITimeService
{
    private readonly ITimeRepository _timeRepository = timeRepository;

    //Create
    public async Task<bool> CreateTimeAsync(TimeRegistrationForm form)
    {
        var time = TimeFactory.Create(form);
        if (time == null)
        {
            Console.WriteLine("The time period is null");
            return false;
        }

        var result = await _timeRepository.CreateAsync(time);
        return result;
    }

    //Read
    public async Task<IEnumerable<Time>> GetTimePeriodsAsync()
    {
        var timePeriods = await _timeRepository.GetAllAsync();
        return timePeriods.Select(TimeFactory.Create);
    }

    public async Task<Time?> GetTimeByIdAsync(int id)
    {
        var time = await _timeRepository.GetAsync(x => x.Id == id);
        return time != null ? TimeFactory.Create(time) : null;
    }

    //Update 
    public async Task<Time?> UpdateTimeAsync(TimeUpdateForm form)
    {
        var time = await _timeRepository.GetAsync(x => x.Id == form.Id);
        if (time == null)
        {
            return null;
        }

        time.StartDay = form.StartDay;
        time.StartMonth = form.StartMonth;
        time.StartYear = form.StartYear;
        time.EndDay = form.EndDay;
        time.EndMonth = form.EndMonth;
        time.EndYear = form.EndYear;

        var result = await _timeRepository.UpdateAsync(x => x.Id == form.Id, time);
        var updatedTime = await _timeRepository.GetAsync(x => x.Id == form.Id);
        return updatedTime != null ? TimeFactory.Create(updatedTime) : null;
    }

    //Delete
    public async Task<bool> DeleteTimeAsync(int id)
    {
        var time = await _timeRepository.GetAsync(x => x.Id == id);
        if (time == null)
        {
            return false;
        }

        var result = await _timeRepository.DeleteAsync(x => x.Id == time.Id);
        return result;
    }
}
