using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Data.Entities
{
    public class YearReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Code { get; set; }

        public String? EmpId { get; set; }

        public int Year { get; set; }

        public double Degree { get; set; }


     
        public string? Notes { get; set; }

        [ForeignKey("YearReportGrade")]
        public int? GradeId {  get; set; }
        public virtual YearReportGrade? YearReportGrade {  get; set; }

        [ForeignKey ("EmpId")]
        public virtual Employee? Employee { get; set; }


    }
}
