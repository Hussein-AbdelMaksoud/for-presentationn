using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class Qualification
    {
        [Key]
        public int Id { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers."),Required]
        public int Code { get; set; }

        /// <summary>
        /// التخصص
        /// </summary>
        [TextOnly,Required, MaxLength(200)]
        public string Specialization { get; set; }

        /// <summary>
        ///جهة التخرج(الجهة على التقدير منها) ئ
        /// </summary>
        
        public string QualPlace { get; set; } =string.Empty;

        /// <summary>
        /// رقم القرار
        /// </summary>
        [Range(1, int.MaxValue)]
        public int DecisionNumber { get; set; }
        /// <summary>
        /// تاريخ القرار
        /// </summary>
        public DateOnly DecisionDate { get; set; }
        /// <summary>
        /// تاريخ التخرج
        /// </summary>
        public DateOnly GraduationDate { get; set; }
        /// <summary>
        /// اخر شهادة حاصل عليها؟
        /// </summary>
        public bool LastQual { get; set; }

        [ForeignKey("Certificate")]
        public int CertificateID { get; set; }

        [ForeignKey("QualGrade")]
        public int QualGradeID { get; set; }

        [ForeignKey("Employee"),Required]
        public string NationalID { get; set; }

        public QualGrade QualGrade { get; set; }
        public virtual Certificate Certificate { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
