using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.BaseDTOs
{
    public class CreateBaseDTO
    {
        [RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }


        [Required,TextOnly, MaxLength(150)]
        public string Name { get; set; }
    }
}
