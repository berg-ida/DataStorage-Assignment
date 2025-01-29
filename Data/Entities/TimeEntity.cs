using System.ComponentModel.DataAnnotations;

namespace Data.Entites;

public class TimeEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string StartDay { get; set; } = null!;

    [Required]
    public string StartMonth { get; set; } = null!;

    [Required]
    public string StartYear { get; set; } = null!;

    public string? EndDay { get; set; }

    public string? EndMonth { get; set; }

    public string? EndYear { get; set; }
}
