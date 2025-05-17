namespace Server.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PenaltyCase
{
   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int  Id { get; set; }

    [Required]
    public int Code { get; set; }
    // حالة الجزاء
    [MaxLength(200)]
    public string? Name { get; set; } = String.Empty;

    public virtual List<Penalty> Penalties { get; set; } = new List<Penalty>();
}

