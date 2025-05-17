using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.Allowance;

public class CreateAllowanceDTO
{
    public int Code { get; set; }
    public string EmpId { get; set; }
    public int? AllowanceTypeId { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "number must be greater than 0")]
    public int? DecisionNo { get; set; }
    public DateOnly? DecisionDate { get; set; }

}