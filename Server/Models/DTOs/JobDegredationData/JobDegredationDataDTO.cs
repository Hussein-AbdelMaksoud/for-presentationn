namespace Server.Models.DTOs.JobDegredationData
{
    public class JobDegredationDataDTO
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string EmpId { get; set; }
        public int FincialDegreeId { get; set; }
        public int? JobTypeId { get; set; }
        public string JobName { get; set; }
        public DateOnly? JobStartDate { get; set; }
        public DateOnly? JobEndDate { get; set; }
        public string? DecisionNo { get; set; }
        public DateOnly? DecisionDate { get; set; }
        public string? Notes { get; set; }
        public bool CurrentDegree { get; set; }
        public DateOnly FincialDegreeDate { get; set; }

    }
}
