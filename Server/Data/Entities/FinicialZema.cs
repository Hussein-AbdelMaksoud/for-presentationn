using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities;


public class FinicialZema
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



    public string EmpId { get; set; }

    [Range(1, int.MaxValue)]
    public int? RequiredZemaNo { get; set; }

    // Foreign key for FinicialZemaType
    [ForeignKey("FinicialZemaType")]
    public int? FinicialZemaTypeId { get; set; }

    public DateOnly? LastDecisionDate { get; set; }
    public DateOnly? NewSubmissionDate { get; set; }

    /// <summary>
    /// تم تقديم الإقرار
    /// </summary>
    public bool Submitted { get; set; }


    /// <summary>
    /// تاريخ تقديم الإقرار
    /// </summary>
    public DateOnly? SubmissionDate { get; set; }

    /// <summary>
    /// تاريخ الذهاب للكسب الغير مشروع
    /// </summary>
    public DateOnly? GraftGoingDate { get; set; }

    /// <summary>
    /// تاريخ العودة من الكسب الغير مشروع
    /// </summary>
    public DateOnly? GraftComingDate { get; set; }

    public string? Notes { get; set; }

    // Navigation property to FinicialZemaType
    public virtual FinicialZemaType? FinicialZemaType { get; set; }

    [ForeignKey("EmpId")]
    public virtual Employee Employee { get; set; }

}