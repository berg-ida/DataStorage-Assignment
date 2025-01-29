namespace Business.Models;

public class Time
{
    public Time() { }
    public Time(int id, string startDay, string startMonth, string startYear, string? endDay, string? endMonth, string? endYear)
    {
        Id = id;
        StartDay = startDay;
        StartMonth = startMonth;
        StartYear = startYear;
        EndDay = endDay;
        EndMonth = endMonth;
        EndYear = endYear;
    }

    public int Id { get; set; }

    public string StartDay { get; set; } = null!;

    public string StartMonth { get; set; } = null!;

    public string StartYear { get; set; } = null!;

    public string? EndDay { get; set; }

    public string? EndMonth { get; set; }

    public string? EndYear { get; set; }

}
