using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.JobDegredationData
{
    public class CreateJobDegredationDataDTO
    {
        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }
        [Required]
        public string EmpId { get; set; }
        public int FincialDegreeId { get; set; }
        public int? JobTypeId { get; set; }
       
        [Required,MaxLength(200), TextOnly]
        public string JobName { get; set; }
        public DateOnly? JobStartDate { get; set; }
        public DateOnly? JobEndDate { get; set; }
        [Range(1,int.MaxValue)]
        public int? DecisionNo { get; set; }
        public DateOnly? DecisionDate { get; set; }
        public string? Notes { get; set; }
        [Required]
        public bool CurrentDegree { get; set; }
        [Required]
        public DateOnly FincialDegreeDate { get; set; }

    }
}
