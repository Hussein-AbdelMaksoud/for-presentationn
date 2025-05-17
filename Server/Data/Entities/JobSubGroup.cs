using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Data.Entities;

public class JobSubGroup
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
    public int Code { get; set; }

    [Required, MaxLength(100), TextOnly]
    public string Name { get; set; } 

    [ForeignKey("JobGroups")]
    public int? JobGroupsId { get; set; }
    [JsonIgnore]
    public virtual JobGroup? JobGroups { get; set; }
    [JsonIgnore]
    public virtual List<JobName>? JobNames { get; set; } = new List<JobName>();
    public virtual List<Employee> Employees { get; set; } = new List<Employee>();



}

