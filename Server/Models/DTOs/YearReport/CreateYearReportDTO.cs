

using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.YearReport
{
    public class CreateYearReportDTO
    {
        [Required]
        public string EmpId { get; set; }
        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]

        public int Code { get; set; }

        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "Please enter a valid year between 1900 and 2099")]
        public int Year { get; set; }

        [Range(1, int.MaxValue)]
        public double Degree { get; set; }

        public int? GradeId { get; set; }
        public string? Notes { get; set; }



    }
}
