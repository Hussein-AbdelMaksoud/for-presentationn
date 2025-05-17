using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Server.Data.Entities;


public class FincialDegreeType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
    public int Code { get; set; }

    [Required, MaxLength(30),TextOnly]
    public string Name { get; set; } 
    [JsonIgnore]
    public virtual List<FincialDegree> FincialDegree { get; set; } = new List<FincialDegree>();
}

