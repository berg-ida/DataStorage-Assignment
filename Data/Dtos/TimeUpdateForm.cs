namespace Data.Dtos;

public class TimeUpdateForm
{
    public int Id { get; set; }

    public string StartDay { get; set; } = null!;

    public string StartMonth { get; set; } = null!;

    public string StartYear { get; set; } = null!;

    public string? EndDay { get; set; }

    public string? EndMonth { get; set; }

    public string? EndYear { get; set; }
}
