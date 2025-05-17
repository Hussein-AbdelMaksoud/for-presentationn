namespace Server.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// YearReportGrade
public class YearReportGrade
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int Code { get; set; }

    [MaxLength(200)]
    public string? Name { get; set; } = String.Empty;
    public List<YearReport>? YearReports { get; set; } = new List<YearReport>();

}
