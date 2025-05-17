
namespace Server.Models.DTOs.Lagna
{
    public class LagnaDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Code { get; set; }
        public string EmpId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string MemberType { get; set; } = string.Empty;
        public int? DecisionNo { get; set; }
        public DateOnly? DecisionDate { get; set; }



    }
}
