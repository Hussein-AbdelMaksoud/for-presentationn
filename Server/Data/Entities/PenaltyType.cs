namespace Server.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// PenaltyType
public class PenaltyType
{
    /// <summary>
    /// Need to Added Manually
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int Code { get; set; }
    // اسم الجزاء
    [MaxLength(200)]
    public string? Name { get; set; } = String.Empty;
    public virtual List<Penalty> Penalties { get; set; } =new List<Penalty>();
}