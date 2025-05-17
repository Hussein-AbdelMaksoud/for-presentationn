namespace Server.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// QualGrade
public class QualGrade
{
    /// <summary>
    /// Need to Added Manually
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    
    public int? Grade { get; set; }

    [Required]
    public int Code { get; set; }
    // التقدير
    [MaxLength(200)]
    public string? Name { get; set; } = String.Empty;
    public List<Qualification> Qualifications { get; set; } = new List<Qualification>();
}
