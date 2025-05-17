using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.YearReportLaw_;

public class CreateYearReportLawDTO
{
    [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
    public int Code { get; set; }
    public string Period { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public string Geha { get; set; } = string.Empty;
    [Required]
    public string EmpId { get; set; }
}