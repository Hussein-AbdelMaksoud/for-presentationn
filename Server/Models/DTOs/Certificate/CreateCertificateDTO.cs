using Server.Models.DTOs.BaseDTOs;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.Certificate
{
    public class CreateCertificateDTO
    {
        [TextOnly, MaxLength(150)]
        public string Name { get; set; }
        public int? EducationLevelId { get; set; }
    }
}
