using Business.Models;
using Data.Dtos;
using Data.Entites;

namespace Data.Factories;

public class ProjectFactory
{
    public static ProjectRegistrationForm CreateRegistrationForm() => new();
    public static ProjectUpdateForm CreateUpdateForm() => new()
    {
        TimePeriod = new Time
        {
            StartDay = string.Empty,
            StartMonth = string.Empty,
            StartYear = string.Empty,
            EndDay = string.Empty,
            EndMonth = string.Empty,
            EndYear = string.Empty,
        },
        ProjectManager = new Manager
        {
            FirstName = string.Empty,
            LastName = string.Empty,
        },
        Client = new Client
        {
            CompanyName = string.Empty,
        }
    };

    public static ProjectEntity Create(ProjectRegistrationForm form) => new()
    {
        Name = form.Name,
        ServiceName = form.ServiceName,
        Price = form.Price,
        Status = form.Status,
        TimePeriodId = form.TimePeriodId,
        ProjectManagerId = form.ProjectManagerId,
        ClientId = form.ClientId,
        TimePeriod = new TimeEntity
        {
            StartDay = form.TimePeriod.StartDay,
            StartMonth = form.TimePeriod.StartMonth,
            StartYear = form.TimePeriod.StartYear,
            EndDay = form.TimePeriod.EndDay,
            EndMonth = form.TimePeriod.EndMonth,
            EndYear = form.TimePeriod.EndYear
        },
        ProjectManager = new ManagerEntity
        {
            FirstName = form.ProjectManager.FirstName,
            LastName = form.ProjectManager.LastName,
        },
        Client = new ClientEntity
        {
            CompanyName = form.Client.CompanyName,
        }
    };

    public static ProjectEntity Create(int id, ProjectUpdateForm form) => new()
    {
        Id = id,
        Name = form.Name,
        ServiceName = form.ServiceName,
        Price = form.Price,
        Status = form.Status,
        TimePeriodId = form.TimePeriodId,
        ProjectManagerId = form.ProjectManagerId,
        ClientId = form.ClientId,
        TimePeriod = new TimeEntity
        {
            StartDay = form.TimePeriod.StartDay,
            StartMonth = form.TimePeriod.StartMonth,
            StartYear = form.TimePeriod.StartYear,
            EndDay = form.TimePeriod.EndDay,
            EndMonth = form.TimePeriod.EndMonth,
            EndYear = form.TimePeriod.EndYear
        },
        ProjectManager = new ManagerEntity
        {
            FirstName = form.ProjectManager.FirstName,
            LastName = form.ProjectManager.LastName,
        },
        Client = new ClientEntity
        {
            CompanyName = form.Client.CompanyName,
        }
    };

    public static Project Create(ProjectEntity entity) =>
        new(entity.Id, entity.Name, entity.ServiceName, entity.Price, entity.Status,
            new Time (
                entity.TimePeriod.Id,
                entity.TimePeriod.StartDay,
                entity.TimePeriod.StartMonth,
                entity.TimePeriod.StartYear,
                entity.TimePeriod.EndDay,
                entity.TimePeriod.EndMonth,
                entity.TimePeriod.EndYear
            ),
            new Manager (
                entity.ProjectManager.Id,
                entity.ProjectManager.FirstName,
                entity.ProjectManager.LastName
            ),
            new Client (
                entity.Client.Id,
                entity.Client.CompanyName
            ),
            entity.TimePeriodId,
            entity.ProjectManagerId,
            entity.ClientId
            );
}
