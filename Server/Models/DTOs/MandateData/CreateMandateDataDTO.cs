using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.MandateData
{
    public class CreateMandateDataDTO
    {
        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }
        [Required]
        public string EmpId { get; set; }
        public int? MandateTypeId { get; set; }
        public int? FacultyId { get; set; }
        public bool IsMandated { get; set; }
        public string Geha { get; set; } = string.Empty;
        public string MandateJob { get; set; } = string.Empty;
        [Range(1, int.MaxValue)]
        public int? DecisionNo { get; set; }
        public DateOnly? DecisionDate { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

    }
}
