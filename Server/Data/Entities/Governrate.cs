using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities;

public class Governrate
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


    /// <summary>
    /// المحافظات
    /// </summary>

    [MaxLength(150),Required,TextOnly]
    public string Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();


}