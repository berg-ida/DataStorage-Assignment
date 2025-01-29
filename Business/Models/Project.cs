using Data.Entites;

namespace Business.Models;

public class Project
{
    public Project(int id, string name, string serviceName, string price, string status, Time timePeriod, Manager projectManager, Client client)
    {
        Id = id;
        Name = name;
        ServiceName = serviceName;
        Price = price;
        Status = status;
        TimePeriod = timePeriod;
        ProjectManager = projectManager;
        Client = client;
    }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ServiceName { get; set; } = null!;

    public string Price { get; set; } = null!;

    public string Status { get; set; } = null!;

    public Time TimePeriod { get; set; } = null!;

    public Manager ProjectManager { get; set; } = null!;

    public Client Client { get; set; } = null!;
}
