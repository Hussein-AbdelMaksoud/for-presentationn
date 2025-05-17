namespace Server.Models.DTOs.Allowance;

public class AllowanceDTO
{


    public int Id { get; set; }
    public int Code { get; set; }
    public string EmpId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;

    public int? AllowanceTypeId { get; set; }
    public string AllowanceTypeName { get; set; } = string.Empty;

    public int? DecisionNo { get; set; }
    public DateOnly? DecisionDate { get; set; }
    

}