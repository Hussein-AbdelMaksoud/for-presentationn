using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.Vacation
{
    public class VacationDTO
    {
        public int Id { get; set; }

        public int Code { get; set; }
        public bool Ended { get; set; }
        public String EmpId { get; set; } 
        public string? Place { get; set; }
        public int? VacationTypeId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public string? DecisionNo { get; set; }
        public DateOnly? DecisionDate { get; set; }
        public string? Notes { get; set; }
    }
}
