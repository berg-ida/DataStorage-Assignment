using Business.Models;
using Data.Dtos;
using Data.Entites;

namespace Data.Factories;

public class ManagerFactory
{
    public static ManagerRegistrationForm CreateRegistrationForm() => new();
    public static ManagerUpdateForm CreateUpdateForm() => new();

    public static ManagerEntity Create(ManagerRegistrationForm form) => new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName,
    };

    public static ManagerEntity Create(int id, ManagerUpdateForm form) => new()
    {
        Id = id,
        FirstName = form.FirstName,
        LastName = form.LastName,
    };

    public static Manager Create(ManagerEntity entity) =>
        new(entity.Id, entity.FirstName, entity.LastName);
}
