namespace Server.Models.DTOs.YearReportLaw_;

public class YearReportLawDTO
{
    public int Id { get; set; }
    public int Code { get; set; }
    public string Period { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public string Geha { get; set; } = string.Empty;
    public string EmpId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
}