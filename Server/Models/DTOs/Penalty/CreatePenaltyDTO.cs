using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.Penalty
{
    public class CreatePenaltyDTO
    {
        [Required]
        public string EmpId { get; set; }
        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }

        public int? PenaltyTypeId { get; set; }

        public int? PenaltyCaseId { get; set; }

       
        public DateOnly? StartDate { get; set; }

        
        public DateOnly? EndDate { get; set; }

        [Range(1,int.MaxValue)]
        public int? DecisionNo { get; set; }

        
        public DateOnly? DecisionDate { get; set; }

      
        public bool? IsCanceled { get; set; }


        public DateOnly? CancelationDate { get; set; }

        [Range(1,int.MaxValue)]
        public int? CancelationDecisionNo { get; set; }

        public string? CancelationReason { get; set; }
    }
}
