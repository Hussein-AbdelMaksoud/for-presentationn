using Server.Models.DTOs.BaseDTOs;

namespace Server.Models.DTOs.SubAd
{
    public class SubAdDTO:BaseDTO
    {
        public bool Level { get; set; }
        public bool SpecialLevel { get; set; }
        public byte Status { get; set; }
        public int? GeneralAdId { get; set; }
    }
}
