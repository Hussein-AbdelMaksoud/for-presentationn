namespace Server.Models.DTOs.FinicialZema;

public class FinicialZemaDTO
{

    public int Id { get; set; }
    public int Code { get; set; }
    public string EmpId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public int? RequiredZemaNo { get; set; }
    public int? FinicialZemaTypeId { get; set; }
    public string FinicialZemaTypeName { get; set; } = string.Empty;
    public DateOnly? LastDecisionDate { get; set; }
    public DateOnly? NewSubmissionDate { get; set; }
    public bool Submitted { get; set; }
    public DateOnly? SubmissionDate { get; set; }
    public DateOnly? GraftGoingDate { get; set; }
    public DateOnly? GraftComingDate { get; set; }
    public string Notes { get; set; } = string.Empty;
}