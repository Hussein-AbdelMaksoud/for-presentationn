using Microsoft.EntityFrameworkCore;
using Server.Data.Enums;
using Server.Data.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities;

public class Employee
{
    /// <summary>
    /// الرقم القومي
    /// </summary>
    [Key]
    [Required,NationalId]

    public string NationalId { get; set; }
    /// <summary>
    /// رقم ملف الموظف
    /// </summary>
    [TextOrNumber]
    public string FileId { get; set; }

    /// <summary>
    /// الإسم رباعيا
    /// </summary>
    [TextOnly]

    public string Name { get; set; }

    /// <summary>
    /// كلية
    /// </summary>
    ///
    [ForeignKey("Faculty")]
    [Required]

    public int FacultyId { get; set; }

    /// <summary>
    /// المجموعة الوظيفية
    /// </summary>
    [ForeignKey("JobGroup")]
    [Required]

    public int JobGroupId { get; set; }

    /// <summary>
    /// المجموعة النوعية
    /// </summary>
    [ForeignKey("JobSubGroup")]
    [Required]

    public int JobSubGroupId { get; set; }

    /// <summary>
    /// مسمى الوظيفة الحالية
    /// </summary>
    [ForeignKey("JobName")]
    [Required]
    public int JobNameId { get; set; }

    /// <summary>
    /// الإدارات الفرعية
    /// </summary>
    [ForeignKey("SubAd")]
    public int? SubAdId { get; set; }

    /// <summary>
    /// الأقسام
    /// </summary>
    [ForeignKey("Department")]
    public int? DepartmentId { get; set; }

    /// <summary>
    /// الوجود في العمل
    /// </summary>
    public bool IsExist { get; set; }

    [ForeignKey("ExistaceCase")]
    public int? ExistaceCaseId { get; set; }


    [ForeignKey("NonExistanceType")]
    public int? NonExistanceTypeId { get; set; }


    /// <summary>
    /// الرقم التأميني
    /// </summary>
    [Range(1, int.MaxValue)]
    public int? TaminNo { get; set; }

    /// <summary>
    /// تاريخ التعيين
    /// </summary>
    public DateOnly? AppointDate { get; set; }

    /// <summary>
    /// تاريخ نهاية الخدمة
    /// </summary>
    public DateOnly? WorkEndDate { get; set; }
    /// <summary>
    /// النوع
    /// </summary>


    [Column(TypeName = "nvarchar(10)")]
    [Required]
    public Gender Gender { get; set; }


    /// <summary>
    /// تاريخ الميلاد
    /// </summary>
    public DateOnly? BirthDate { get; set; }

    /// <summary>
    /// محل الميلاد
    /// </summary>
    public string BirthPlace { get; set; } = string.Empty;

    /// <summary>
    /// هاتف السكن
    /// </summary>
    public string Tel { get; set; } = string.Empty;

    /// <summary>
    /// الموبيل
    /// </summary>
    [RegularExpression(@"^0(10[0-9]{8}|11[0-9]{8}|12[0-9]{8}|15[0-9]{8})$",
   ErrorMessage = "Invalid Mobile number.")]
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// تاريخ إستلام العمل بالجامعة
    /// </summary>
    public DateOnly? WorkDate { get; set; }



    [ForeignKey("HealthState")]
    public int? HealthStateId { get; set; }

    /// <summary>
    /// رقم قرار التعيين
    /// </summary>
    [Range(1, int.MaxValue)]
    public int? AppointDN { get; set; }

    /// <summary>
    /// تاريخ الخبرة
    /// </summary>
    public DateOnly? ExperanceDate { get; set; }

    /// <summary>
    /// رقم قرار الخبرة
    /// </summary>
    [Range(1, int.MaxValue)]
    public int? ExperanceDN { get; set; }

    /// <summary>
    /// مسلسل
    /// </summary>
    [Range(1, int.MaxValue)]
    public int? Serial { get; set; }

    /// <summary>
    /// الحالة الاجتماعية
    /// </summary>

    [ForeignKey("SocialState")]
    public int? SocialStateId { get; set; }




    /// <summary>
    /// المحافظة
    /// </summary>
    [ForeignKey("Governrate")]
    public int? GovernrateId { get; set; }

    /// <summary>
    /// العنوان
    /// </summary>
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// تاريخ استلام العمل أول مرة
    /// </summary>
    public DateOnly? WorkDateFt { get; set; }

    /// <summary>
    /// تاريخ إعادة التعيين
    /// </summary>
    public DateOnly? ReAppointDate { get; set; }

    /// <summary>
    /// تاريخ التعيين بالضم
    /// </summary>
    public DateOnly? CombinationDate { get; set; }

    /// <summary>
    /// نص تاريخ التعيين
    /// </summary>
    public string AppointDateTxt { get; set; } = string.Empty;

    /// <summary>
    /// نص تاريخ استلام العمل بالجامعة
    /// </summary>
    public string WorkDateTxt { get; set; } = string.Empty;

    /// <summary>
    /// نص تاريخ استلام العمل أول مرة
    /// </summary>
    public string WorkDateFtTxt { get; set; } = string.Empty;

    /// <summary>
    /// نص تاريخ إعادة التعيين
    /// </summary>
    public string ReAppointDateTxt { get; set; } = string.Empty;

    /// <summary>
    /// ( نص تاريخ التعيين بالضم (صفة التعيين
    /// </summary>
    public string CombinationDateTxt { get; set; } = string.Empty;

    /// <summary>
    /// نص تاريخ الخبرة
    /// </summary>
    public string ExperanceDateTxt { get; set; } = string.Empty;
    /// <summary>
    /// تسلسل درجات القيد
    /// </summary>
    public string BoundDegree { get; set; } = string.Empty;

    /// <summary>
    /// مجالات الخبرة
    /// </summary>
    public string ExperanceDomain { get; set; } = string.Empty;

    /// <summary>
    /// رقم قرار نهاية الخدمة
    /// </summary>
    [Range(1, int.MaxValue)]
    public int? WorkEndDec { get; set; }

    /// <summary>
    /// تاريخ رقم قرار نهاية الخدمة
    /// </summary>
    public DateOnly? WorkEndDeDate { get; set; }

    /// <summary>
    /// مدة مشتراه
    /// </summary>
    public string YearBuy { get; set; } = string.Empty;

    /// <summary>
    /// تاريخ الوظيفة المسكن عليها
    /// </summary>
    [Required]
    public DateOnly FJobDate { get; set; }

    /// <summary>
    /// المركز
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// القرية
    /// </summary>
    public string Village { get; set; } = string.Empty;

    /// <summary>
    /// مدة مؤقتة
    /// </summary>
    public string YearEmp { get; set; } = string.Empty;


    public bool IsDeleted { get; set; }
    public DateTime? DeleteTime { get; set; }

    [Precision(18, 2)]
    public decimal? LastBalance { get; set; }
    [Precision(18, 2)]
    public decimal? CurrentBalance { get; set; }
    [Precision(18, 2)]
    public decimal? LastYearBalance { get; set; }
    [Precision(18, 2)]
    public decimal? SickBalance { get; set; }

    /// <summary>
    /// المدة المحتفظ بها يوم
    /// </summary>
    [Range(1, int.MaxValue)]
    public int? ReservedDays { get; set; }

    /// <summary>
    /// المدة المحتفظ بها شهر
    /// </summary>
    [Range(1, int.MaxValue)]
    public int? ReservedMonths { get; set; }

    /// <summary>
    /// المدة المحتفظ بها سنه
    /// </summary>
    [Range(1, int.MaxValue)]
    public int? ReservedYears { get; set; }

    /// <summary>
    /// نوع الاعاقة
    /// </summary>
    public string DisabilityType { get; set; } = string.Empty;

    public string DisabilityFamilyMember { get; set; } = string.Empty;
    /// <summary>
    /// تاريخ الحصول على الدرجة
    /// </summary>
    public DateOnly? DegreeDate { get; set; }









    public virtual Governrate Governrate { get; set; }
    public virtual ExistaceCase ExistaceCase { get; set; }
    public virtual Faculty Faculty { get; set; }


    public virtual NonExistanceType? NonExistanceType { get; set; }
    public virtual HealthState? HealthState { get; set; }



    public virtual ICollection<Lagna> Lagnas { get; set; } = new List<Lagna>();
    public virtual ICollection<MilitaryState> MilitaryStates { get; set; } = new List<MilitaryState>();
    public virtual ICollection<MandateData> MandateDatas { get; set; } = new List<MandateData>();



    public virtual SocialState SocialState { get; set; }
    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();
    public virtual List<Vacation> Vacations { get; set; } = new List<Vacation>();
    public virtual ICollection<ThanksLetter> ThanksLetters { get; set; } = new List<ThanksLetter>();
    public virtual ICollection<YearReport> YearReports { get; set; } = new List<YearReport>();
    public virtual ICollection<Penalty> Penalties { get; set; } = new List<Penalty>();
    public virtual List<Training> Trainings { get; set; } = new List<Training>();
    public virtual List<Qualification> Qualifications { get; set; } = new List<Qualification>();
    public virtual Department Department { get; set; }
    public virtual SubAd SubAd { get; set; }



    public virtual ICollection<FinicialZema> FinicialZemas { get; set; }
    public virtual ICollection<YearReportLaw> YearReportLaws { get; set; }
    public virtual ICollection<Allowance> Allowances { get; set; }

    public virtual ICollection<JobDegredationData> JobDegredationData { get; set; }
    public virtual JobGroup JobGroup { get; set; }
    public virtual JobSubGroup JobSubGroup { get; set; }
    public virtual JobName JobName { get; set; }



}

