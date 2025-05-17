namespace Server.Models.DTOs.YearReport
{
    public class YearReportDTO
    {
        public string EmpId { get; set; }
        public int Id { get; set; }
        public int Code { get; set; }

        public int Year { get; set; }

        public double Degree { get; set; }

        public int? GradeId { get; set; }
        public string? Notes { get; set; }
    }
}
