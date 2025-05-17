using Server.Models.DTOs.BaseDTOs;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.SubAd
{
    public class CreateSubAdDTO :CreateBaseDTO
    {
        public bool Level { get; set; }
        public bool SpecialLevel { get; set; }
        [Range(1, byte.MaxValue)]
        public byte Status { get; set; }
        public int? GeneralAdId { get; set; }
    }
}
