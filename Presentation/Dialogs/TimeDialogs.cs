using Business.Models;
using Data.Factories;
using Data.Interfaces;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class TimeDialogs(ITimeService timeService) : ITimeDialogs
{
    private readonly ITimeService _timeService = timeService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-----------TIME SYSTEM-----------");
            Console.WriteLine("");
            Console.WriteLine($"{"[1]",-10} Create a time period.");
            Console.WriteLine($"{"[2]",-10} View all time periods.");
            Console.WriteLine($"{"[3]",-10} Update a time period.");
            Console.WriteLine($"{"[4]",-10} Delete a time period.");
            Console.WriteLine($"{"[5]",-10} Quit application.");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateTimePeriodOption();
                    break;

                case "2":
                    await ViewAllTimePeriodsOption();
                    break;

                case "3":
                    await UpdateTimePeriodOption();
                    break;

                case "4":
                    await DeleteTimePeriodOption();
                    break;

                case "5":
                    ExitApplicationOption();
                    break;
            }
        }
    }

    public async Task CreateTimePeriodOption()
    {
        var time = TimeFactory.CreateRegistrationForm();

        Console.Clear();
        Console.WriteLine("-----------CREATE TIME-----------");
        Console.WriteLine("");
        Console.WriteLine("Please give the timeperiod a... (dd/mm/yyyy)");
        Console.Write("Start day: ");
        time.StartDay = Console.ReadLine()!;
        Console.Write("Start month: ");
        time.StartMonth = Console.ReadLine()!;
        Console.Write("Start year: ");
        time.StartYear = Console.ReadLine()!;
        Console.Write("End day: ");
        time.EndDay = Console.ReadLine()!;
        Console.Write("End month: ");
        time.EndMonth = Console.ReadLine()!;
        Console.Write("End year: ");
        time.EndYear = Console.ReadLine()!;

        var result = await _timeService.CreateTimeAsync(time);
        if (result)
        {
            Console.WriteLine("Time period was created sucessfully.");
        }
        else
        {
            Console.WriteLine("Time period was not created.");
        }
        Console.ReadLine();
    }

    public async Task ViewAllTimePeriodsOption()
    {
        Console.Clear();
        Console.WriteLine("---------VIEW ALL TIMES----------");
        Console.WriteLine("");

        var timePeriods = await _timeService.GetTimePeriodsAsync();

        if (timePeriods.Any())
        {
            foreach (var time in timePeriods)
            {
                Console.WriteLine($"{"Id: ",-10} {time.Id}");
                Console.WriteLine($"{"Time: ",-10} " +
                    $"{time.StartDay}/" +
                    $"{time.StartMonth}/" +
                    $"{time.StartYear} - " +
                    $"{time.EndDay}/" +
                    $"{time.EndMonth}/" +
                    $"{time.EndYear}");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("No projects found.");
        }
        Console.ReadLine();
    }

    public async Task UpdateTimePeriodOption()
    {
        Console.Clear();
        Console.WriteLine("----------UPDATE TIME------------");
        Console.WriteLine("");

        Console.WriteLine("What is the id of the time period you want to update?");
        var stringId = Console.ReadLine()!;

        if (int.TryParse(stringId, out int intId))
        {
            var time = await _timeService.GetTimeByIdAsync(intId);
            if (time == null)
            {
                Console.WriteLine("No time period found with this id.");
            }
            else
            {
                Console.WriteLine($"{"Time: ",-10} " +
                    $"{time.StartDay}/" +
                    $"{time.StartMonth}/" +
                    $"{time.StartYear} - " +
                    $"{time.EndDay}/" +
                    $"{time.EndMonth}/" +
                    $"{time.EndYear}");
                Console.WriteLine("");

                var timeUpdateForm = TimeFactory.CreateUpdateForm();
                timeUpdateForm.Id = time.Id;
                timeUpdateForm.StartDay = time.StartDay;
                timeUpdateForm.StartMonth = time.StartMonth;
                timeUpdateForm.StartYear = time.StartYear;
                timeUpdateForm.EndDay = time.EndDay;
                timeUpdateForm.EndMonth = time.EndMonth;
                timeUpdateForm.EndYear = time.EndYear;

                Console.Clear();
                Console.WriteLine("----------UPDATE TIME------------");
                Console.WriteLine("");
                Console.WriteLine("Please give the timeperiod a... (dd/mm/yyyy)");
                Console.Write("Start day: ");
                var startDay = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(startDay) && startDay != time.StartDay)
                    timeUpdateForm.StartDay = startDay;
                Console.Write("Start month: ");
                var startMonth = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(startMonth) && startMonth != time.StartMonth)
                    timeUpdateForm.StartMonth = startMonth;
                Console.Write("Start year: ");
                var startYear = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(startYear) && startYear != time.StartYear)
                    timeUpdateForm.StartYear = startYear;
                Console.Write("End day: ");
                var endDay = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(endDay) && endDay != time.EndDay)
                    timeUpdateForm.EndDay = endDay;
                Console.Write("End month: ");
                var endMonth = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(endMonth) && endMonth != time.EndMonth)
                    timeUpdateForm.EndMonth = endMonth;
                Console.Write("End year: ");
                var endYear = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(endYear) && endYear != time.EndYear)
                    timeUpdateForm.EndYear = endYear;

                time = await _timeService.UpdateTimeAsync(timeUpdateForm);
                if (time != null)
                {
                    Console.WriteLine("Time was updated sucessfully.");

                }
                else
                {
                    Console.WriteLine("Time was not updated.");
                }
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("No time period found with this id.");

        }
    }

    public async Task DeleteTimePeriodOption()
    {
        Console.Clear();
        Console.WriteLine("-----------DELETE TIME-----------");
        Console.WriteLine("");

        Console.WriteLine("What is the id of the time period you want to delete?");
        var stringId = Console.ReadLine()!;

        if (int.TryParse(stringId, out int intId))
        {
            var time = await _timeService.GetTimeByIdAsync(intId);
            if (time == null)
            {
                Console.WriteLine("No time period found with this id.");
            }
            else
            {
                var result = await _timeService.DeleteTimeAsync(time.Id);
                if (result)
                {
                    Console.WriteLine("Time period was deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Time period was not deleted.");
                }
            }
        }
        else
        {
            Console.WriteLine("No time period found with this id.");
        }
        Console.ReadLine();
    }

    public void ExitApplicationOption()
    {
        Console.Clear();
        Console.WriteLine("----------EXIT APPLICATION----------");
        Console.WriteLine("");
        Console.WriteLine("Do you want to exit the application? (y/n)");
        var exitOption = Console.ReadLine()!;

        if (exitOption.ToLower() == "y")
        {
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Going back to the menu.");
            Console.ReadLine();
        }
    }
}
