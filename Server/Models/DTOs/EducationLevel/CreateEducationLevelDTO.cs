using Server.Models.DTOs.BaseDTOs;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.EducationLevel
{
    public class CreateEducationLevelDTO
    {
        [Required,TextOnly, MaxLength(50)]
        public string Name { get; set; }
        [Range(1, int.MaxValue)]
        public int SortId { get; set; }

    }
}
