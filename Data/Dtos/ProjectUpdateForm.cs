using Business.Models;

namespace Data.Dtos;

public class ProjectUpdateForm
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ServiceName { get; set; } = null!;

    public string Price { get; set; } = null!;

    public string Status { get; set; } = null!;

    public Time TimePeriod { get; set; } = null!;

    public Manager ProjectManager { get; set; } = null!;

    public Client Client { get; set; } = null!;

    public int TimePeriodId { get; set; }

    public int ProjectManagerId { get; set; }

    public int ClientId { get; set; }
}
