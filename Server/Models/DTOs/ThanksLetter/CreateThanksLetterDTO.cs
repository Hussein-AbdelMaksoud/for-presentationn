using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.ThanksLetter
{
    public class CreateThanksLetterDTO
    {
        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }
        [Required]
        public String EmpId { get; set; } 

        public string? LetterName { get; set; }
   
        public string? Geha { get; set; }
        public DateOnly? DecisionDate { get; set; }
        [Range(1,int.MaxValue)]
        public int? DecisionNo { get; set; }
 
    }
}
