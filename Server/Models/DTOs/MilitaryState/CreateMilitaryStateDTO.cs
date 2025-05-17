using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.MilitaryState
{
    public class CreateMilitaryStateDTO
    {

        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }
        public string EmpId { get; set; }
        public int MilitaryStateTypeId { get; set; }
        public DateOnly? Date { get; set; }
        public string Notes { get; set; } = string.Empty;
        public bool CurrentMilitaryState { get; set; }
    }
}
