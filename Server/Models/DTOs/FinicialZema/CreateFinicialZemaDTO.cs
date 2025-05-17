using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.FinicialZema;

public class CreateFinicialZemaDTO
{
    [Required,RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
    public int Code { get; set; }
    public string EmpId { get; set; }
    [Range(1, int.MaxValue)]
    public int? RequiredZemaNo { get; set; }
    public int? FinicialZemaTypeId { get; set; }
    public DateOnly? LastDecisionDate { get; set; }
    public DateOnly? NewSubmissionDate { get; set; }
    public bool Submitted { get; set; }
    public DateOnly? SubmissionDate { get; set; }
    public DateOnly? GraftGoingDate { get; set; }
    public DateOnly? GraftComingDate { get; set; }
    public string Notes { get; set; } = string.Empty;

}