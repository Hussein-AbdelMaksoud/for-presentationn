using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities;

public class FinicialZemaType
{

    /// <summary>
    /// Primary Key - Auto Generated
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// University Code - User Visible
    /// </summary>
    [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
    public int Code { get; set; }



    [MaxLength(150),TextOnly, Required]
    public string Name { get; set; }


    // Navigation property for the related FinicialZema entities
    public virtual ICollection<FinicialZema> FinicialZemas { get; set; } = new List<FinicialZema>();



}
