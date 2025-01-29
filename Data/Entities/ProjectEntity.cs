using System.ComponentModel.DataAnnotations;

namespace Data.Entites;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string ServiceName { get; set; } = null!;

    [Required]
    public string Price { get; set; } = null!;

    [Required]
    public string Status { get; set; } = null!;

    [Required]
    public int TimeId { get; set; }

    [Required]
    public TimeEntity TimePeriod { get; set; } = null!;

    [Required]
    public int ManagerId { get; set; }

    [Required]
    public ManagerEntity ProjectManager { get; set; } = null!;

    [Required]
    public int ClientId { get; set; }

    [Required]
    public ClientEntity Client { get; set; } = null!;

}
