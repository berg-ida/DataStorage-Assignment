using Business.Models;
using Data.Dtos;
using Data.Entites;

namespace Data.Factories;
public class ClientFactory
{
    public static ClientRegistrationForm CreateRegistrationForm() => new();
    public static ClientUpdateForm CreateUpdateForm() => new();

    public static ClientEntity Create(ClientRegistrationForm form) => new()
    {
        CompanyName = form.CompanyName,
    };

    public static ClientEntity Create(int id, ClientUpdateForm form) => new()
    {
        Id = id,
        CompanyName = form.CompanyName,
    };

    public static Client Create(ClientEntity entity) =>
        new(entity.Id, entity.CompanyName);
}
