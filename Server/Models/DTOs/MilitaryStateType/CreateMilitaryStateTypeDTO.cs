
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.MilitaryStateType
{
    public class CreateMilitaryStateTypeDTO 
    {
        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }
        [Required, RegularExpression("^(?=.*[a-zA-Z\\u0600-\\u06FF])[\\p{L}a-zA-Z0-9\\s]+$"), MaxLength(150)]
        public string Name { get; set; }
    }
}
