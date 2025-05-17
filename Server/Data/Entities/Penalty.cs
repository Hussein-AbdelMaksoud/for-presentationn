using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class Penalty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }

        /// <summary>
        /// الرقم الكودي
        /// </summary>
      
        public string? EmpId { get; set; }

        /// <summary>
        /// اسم الجزاء
        /// </summary>
        [ForeignKey("PenaltyType")]
        public int? PenaltyTypeId { get; set; }

        /// <summary>
        /// حالة الجزاء
        /// </summary>
        [ForeignKey("PenaltyCase")]
        public int? PenaltyCaseId { get; set; }

        /// <summary>
        /// تاريخ بداية الجزاء
        /// </summary>
        public DateOnly? StartDate { get; set; }

        /// <summary>
        /// تاريخ نهاية الجزاء
        /// </summary>
        public DateOnly? EndDate { get; set; }

        /// <summary>
        /// رقم القرار
        /// </summary>
        [Range(1,int.MaxValue)]
        public int? DecisionNo { get; set; }

        /// <summary>
        /// تاريخ القرار
        /// </summary>
        public DateOnly? DecisionDate { get; set; }

        /// <summary>
        /// الاعفاء من الجزاء
        /// </summary>
        public bool? IsCanceled { get; set; }

        /// <summary>
        /// تاريخ الاعفاء من الجزاء
        /// </summary>
        public DateOnly? CancelationDate { get; set; }

        /// <summary>
        /// رقم قرار الاعفاء من الجزاء
        /// </summary>
        [Range(1,int.MaxValue)]
        public int? CancelationDecisionNo { get; set; }

        /// <summary>
        /// سبب الغاء الجزاء
        /// </summary>
        [MaxLength(200)]
        public string? CancelationReason { get; set; }

        // Navigation properties
        [ForeignKey("EmpId")]
        public virtual Employee? Employee { get; set; }
        public virtual PenaltyType? PenaltyType { get; set; }
        public virtual PenaltyCase? PenaltyCase { get; set; }
    }
}
