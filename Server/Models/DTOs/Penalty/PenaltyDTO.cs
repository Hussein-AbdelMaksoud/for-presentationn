using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.Penalty
{
    public class PenaltyDTO
    {
        public string EmpId { get; set; }
        public int Id { get; set; }
        public int Code { get; set; }

        public int? PenaltyTypeId { get; set; }

        public int? PenaltyCaseId { get; set; }

       
        public DateOnly? StartDate { get; set; }

        
        public DateOnly? EndDate { get; set; }

       
        public int? DecisionNo { get; set; }

        
        public DateOnly? DecisionDate { get; set; }

      
        public bool? IsCanceled { get; set; }


        public DateOnly? CancelationDate { get; set; }

      
        public int? CancelationDecisionNo { get; set; }

        public string? CancelationReason { get; set; }
    }
}
