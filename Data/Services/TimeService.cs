using Data.Interfaces;
using Business.Models;
using Data.Dtos;
using Data.Factories;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;

namespace Data.Services;

public class TimeService(ITimeRepository timeRepository, DataContext context) : ITimeService
{
    private readonly ITimeRepository _timeRepository = timeRepository;
    private readonly DbContext _context = context;

    //Create
    public async Task<bool> CreateTimeAsync(TimeRegistrationForm form)
    {
        var time = TimeFactory.Create(form);
        if (time == null)
        {
            Console.WriteLine("The time period is null");
            return false;
        }

        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var result = await _timeRepository.CreateAsync(time);
            await _timeRepository.SaveAsync();
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error creating time period :: {ex.Message}");
            return false;
        }
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
        var transaction = await _context.Database.BeginTransactionAsync();

        try
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

            time = TimeFactory.Create(time.Id, form);

            await _timeRepository.UpdateAsync(x => x.Id == form.Id, time);
            await _timeRepository.SaveAsync();
            await transaction.CommitAsync();

            return time != null ? TimeFactory.Create(time) : null;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error updating time period :: {ex.Message}");
            return null;
        }
    }

    //Delete
    public async Task<bool> DeleteTimeAsync(int id)
    {
        var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var time = await _timeRepository.GetAsync(x => x.Id == id);
            if (time == null)
            {
                return false;
            }

            var result = await _timeRepository.DeleteAsync(x => x.Id == time.Id);
            await _timeRepository.SaveAsync();
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error deleting time period :: {ex.Message}");
            return false;
        }
    }
}
