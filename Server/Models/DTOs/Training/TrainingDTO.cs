
namespace Server.Models.DTOs.Training
{
    public class TrainingDTO
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Grade { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int Type { get; set; }
        public string? NationalId { get; set; } = string.Empty;
        public string? EmployeeName { get; set; } = string.Empty;

    }
}
