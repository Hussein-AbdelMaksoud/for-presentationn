namespace Server.Models.DTOs.MilitaryState
{
    public class MilitaryStateDTO
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string EmpId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public int MilitaryStateTypeId { get; set; }
        public string MilitaryStateTypeName { get; set; } = string.Empty;
        public DateOnly? Date { get; set; }
        public string Notes { get; set; } = string.Empty;
        public bool CurrentMilitaryState { get; set; }

       

    }
}
