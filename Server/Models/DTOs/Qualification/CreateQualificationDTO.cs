using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.Qualification
{
    public class CreateQualificationDTO
    {
        [RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers."),Required]
        public int Code { get; set; }
        [Required,TextOnly,MaxLength(200)]
        public string Specialization { get; set; }

        public string QualPlace { get; set; } = string.Empty;
        [Range(1, int.MaxValue)]
        public int DecisionNumber { get; set; }

        public DateOnly DecisionDate { get; set; }

        public DateOnly GraduationDate { get; set; }

        public bool LastQual { get; set; }
        public int CertificateID { get; set; }

        public int QualGradeID { get; set; }

        public string? NationalID { get; set; } = string.Empty;
    }
}
