using System.ComponentModel.DataAnnotations;

namespace Data.Entites;

public class ClientEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string CompanyName { get; set; } = null!;

}
