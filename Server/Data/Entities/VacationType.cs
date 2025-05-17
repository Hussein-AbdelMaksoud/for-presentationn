namespace Server.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// VacationType
public class VacationType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int Code { get; set; }

    // نوع الاجازه
    [MaxLength(100)]
    public string? Name { get; set; } = String.Empty;
    public List<Vacation>? Vacations { get; set; } = new List<Vacation>();
}
