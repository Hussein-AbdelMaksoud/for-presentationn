using Server.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.Vacation
{
    public class CreateVacationDTO
    {
        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }
        [Required]
        public bool Ended { get; set; }
        [Required]
        public String EmpId { get; set; } 
        public string? Place { get; set; }
        public int? VacationTypeId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        [Range(1,int.MaxValue)]
        public int? DecisionNo { get; set; }
        public DateOnly? DecisionDate { get; set; }
        public string? Notes { get; set; }

    }
}
