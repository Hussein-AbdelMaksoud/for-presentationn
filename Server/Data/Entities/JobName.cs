using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Data.Entities;

public class JobName
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
    public int Code { get; set; }

    [Required, MaxLength(50), TextOnly]
    public string Name { get; set; }
    [MaxLength(350)]
    public string JobMission { get; set; } = string.Empty;

    [ForeignKey("JobSubGroup")]
    public int? JobSubGroupId { get; set; }
    public virtual JobSubGroup? JobSubGroup { get; set; }
    public virtual List<Employee> Employees { get; set; } = new List<Employee>();

}

