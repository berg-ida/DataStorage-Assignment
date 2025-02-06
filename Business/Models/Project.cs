namespace Business.Models;

public class Project
{
    public Project(int id, string name, string serviceName, string price, string status, Time timePeriod, Manager projectManager, Client client, int timePeriodId, int projectManagerId, int clientId)
    {
        Id = id;
        Name = name;
        ServiceName = serviceName;
        Price = price;
        Status = status;
        TimePeriod = timePeriod;
        ProjectManager = projectManager;
        Client = client;
        TimePeriodId = timePeriodId;
        ProjectManagerId = projectManagerId;
        ClientId = clientId;
    }

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
