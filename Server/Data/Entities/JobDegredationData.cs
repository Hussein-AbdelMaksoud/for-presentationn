using Server.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class JobDegredationData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
    public int Code { get; set; }

    /// <summary>
    /// الرقم الكودي
    /// </summary>

    [Required]
    public string EmpId { get; set; }

    /// <summary>
    /// الدرجة المالية
    /// </summary>
    [ForeignKey("FincialDegree")]
    public int FincialDegreeId { get; set; }

    /// <summary>
    /// نوع الوظيفة
    /// </summary>
    [ForeignKey("JobType")]
    public int? JobTypeId { get; set; }

    /// <summary>
    /// الوظيفة
    /// </summary>
    [Required,MaxLength(200),TextOnly]
    public string JobName { get; set; }

    /// <summary>
    /// تاريخ شغلها (تاريخ تنفيذ القرار)
    /// </summary>
    public DateOnly? JobStartDate { get; set; }

    /// <summary>
    /// تاريخ شغلها (النهاية)
    /// </summary>
    public DateOnly? JobEndDate { get; set; }

    /// <summary>
    /// رقم القرار
    /// </summary>
    [Range(1,int.MaxValue)]
    public int DecisionNo { get; set; }

    /// <summary>
    /// تاريخ القرار
    /// </summary>
    public DateOnly? DecisionDate { get; set; }

    /// <summary>
    /// ملاحظات
    /// </summary>
    [MaxLength(250)]
    public string? Notes { get; set; }

    /// <summary>
    /// الدرجة الحالية
    /// </summary>

    [Required]
    public bool CurrentDegree { get; set; }

    [Required]
    public DateOnly FincialDegreeDate { get; set; }




    // Navigation properties
    [ForeignKey(name: "EmpId")]
    public virtual Employee Employee { get; set; }
    public virtual FincialDegree FincialDegree { get; set; }
    public virtual JobType JobType { get; set; }
}
