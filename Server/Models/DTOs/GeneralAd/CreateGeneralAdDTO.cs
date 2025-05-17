using Server.Models.DTOs.BaseDTOs;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.GeneralAd
{
    public class CreateGeneralAdDTO:CreateBaseDTO
    {
        public bool Level { get; set; }
        public bool SpecialLevel { get; set; }
        [Range(1, byte.MaxValue)]
        public byte Status { get; set; }
        public int? SectorID { get; set; }

    }
}
