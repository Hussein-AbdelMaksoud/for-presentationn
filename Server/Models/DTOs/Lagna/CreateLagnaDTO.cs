

using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.Lagna
{
    public class CreateLagnaDTO 
    {
        [Required(ErrorMessage = "Lagna Code is required."), RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }
        [Required(ErrorMessage = "Lagna Name is required."), RegularExpression("^(?=.*[a-zA-Z\\u0600-\\u06FF])[\\p{L}a-zA-Z0-9\\s]+$"),MaxLength(150)]
        public string Name { get; set; } 
        public string EmpId { get; set; }
        public string MemberType { get; set; } = string.Empty;
        [Range(1, int.MaxValue)]
        public int? DecisionNo { get; set; }
        public DateOnly? DecisionDate { get; set; }
    }
}
