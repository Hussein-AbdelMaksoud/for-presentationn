using Server.Models.DTOs.BaseDTOs;

namespace Server.Models.DTOs.Certificate
{
    public class CertificateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EducationLevelId { get; set; }
    }
}
