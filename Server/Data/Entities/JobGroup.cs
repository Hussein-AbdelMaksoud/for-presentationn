using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities;

public class JobGroup
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
    public int Code { get; set; }
    [Required, MaxLength(100), TextOnly]
    public string Name { get; set; } 

    public virtual List<JobSubGroup> JobSubGroups { get; set; } = new List<JobSubGroup>();
    public virtual List<Employee> Employees { get; set; } = new List<Employee>();


}

