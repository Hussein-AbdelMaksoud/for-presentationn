using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class MandateData
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
        /// National ID of the Employee
        /// </summary>
        [Required]
        public string EmpId { get; set; }

        /// <summary>
        /// Mandate Type (e.g., Internal, External)
        /// </summary>
        [ForeignKey("MandateType")]
        public int? MandateTypeId { get; set; }

        /// <summary>
        /// Faculty for Mandate (Inside University)
        /// </summary>
        [ForeignKey("Faculty")]
        public int? FacultyId { get; set; }

        /// <summary>
        /// Indicates if the Employee is Mandated
        /// </summary>
        public bool IsMandated { get; set; }

        /// <summary>
        /// Mandate Organization (Outside University)
        /// </summary>
        public string Geha { get; set; } = string.Empty;

        /// <summary>
        /// Job Title for Mandate
        /// </summary>
        public string MandateJob { get; set; } = string.Empty;

        /// <summary>
        /// Decision Number for Mandate
        /// </summary>
        [Range(1, int.MaxValue)]
        public int? DecisionNo { get; set; }

        /// <summary>
        /// Decision Date for Mandate
        /// </summary>
        public DateOnly? DecisionDate { get; set; }

        /// <summary>
        /// Start Date of Mandate
        /// </summary>
        public DateOnly? StartDate { get; set; }

        /// <summary>
        /// End Date of Mandate
        /// </summary>
        public DateOnly? EndDate { get; set; }

        // Navigation Properties

        /// <summary>
        /// Navigation Property for Employee
        /// </summary>
        [ForeignKey("EmpId")]
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// Navigation Property for MandateType
        /// </summary>
        public virtual MandateType? MandateType { get; set; }

        /// <summary>
        /// Navigation Property for Faculty
        /// </summary>
        public virtual Faculty? Faculty { get; set; }
    }
}
