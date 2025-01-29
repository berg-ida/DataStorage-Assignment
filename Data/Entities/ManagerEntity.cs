using System.ComponentModel.DataAnnotations;

namespace Data.Entites;

public class ManagerEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

}
