namespace Server.Models.DTOs.MandateData
{
    public class MandateDataDTO
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string EmpId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public int? MandateTypeId { get; set; }
        public string MandateTypeName { get; set; } = string.Empty;
        public int? FacultyId { get; set; }
        public string FacultyName { get; set; } = string.Empty;
        public bool IsMandated { get; set; }
        public string Geha { get; set; } = string.Empty;
        public string MandateJob { get; set; } = string.Empty;
        public string DecisionNo { get; set; } = string.Empty;
        public DateOnly? DecisionDate { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

    }
}
