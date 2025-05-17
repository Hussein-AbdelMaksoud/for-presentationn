namespace Server.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class FincialDegree
{
   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
    public int Code { get; set; }

    [Required, MaxLength(50),TextOnly]
    public string Name { get; set; }
    [ForeignKey("FincialDegreeType")]
    public int? FincialDegreeTypeId { get; set; }
    [JsonIgnore]
    public virtual FincialDegreeType? FincialDegreeType { get; set; }

    public virtual ICollection<JobDegredationData> JobDegredationData { get; set; }
}


