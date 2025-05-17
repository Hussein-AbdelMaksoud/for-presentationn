using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities;

public class Allowance
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
    /// الرقم الكودى
    /// </summary>

    [Required]
    public string EmpId { get; set; }




    /// <summary>
    /// اسم العلاوة
    /// </summary>


    [ForeignKey("AllowanceType")] // Foreign Key
    public int? AllowanceTypeId { get; set; }



    /// <summary>
    /// رقم القرار
    /// </summary>
    public int? DecisionNo { get; set; }



    /// <summary>
    /// تاريخ القرار
    /// </summary>
    ///

    public DateOnly? DecisionDate { get; set; }


    // Virtual navigation property for lazy loading
    public virtual AllowanceType? AllowanceType { get; set; }

    [ForeignKey("EmpId")]
    public virtual Employee Employee { get; set; }
}