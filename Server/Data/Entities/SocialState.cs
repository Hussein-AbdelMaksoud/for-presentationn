namespace Server.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// SoucialState
public class SocialState
{
    /// <summary>
    /// Need to Added Manually
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int Code { get; set; }
    // الحالة الاجتماعية
    [MaxLength(100)]
    public string? Name { get; set; } = String.Empty;

    public virtual List<Employee>? Employees { get; set; } = new List<Employee>();
}
