using System.ComponentModel.DataAnnotations;

namespace Data.Dtos;

public class ManagerRegistrationForm
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
