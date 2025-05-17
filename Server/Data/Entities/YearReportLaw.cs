using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities;

public class YearReportLaw
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
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
    /// الفترة
    /// </summary>
    public string Period { get; set; } = string.Empty;

    /// <summary>
    /// التقدير
    /// </summary>
    public string Grade { get; set; } = string.Empty;

    /// <summary>
    /// الجهة
    /// </summary>
    public string Geha { get; set; } = string.Empty;

    [ForeignKey("EmpId")]
    public virtual Employee Employee { get; set; }




}
