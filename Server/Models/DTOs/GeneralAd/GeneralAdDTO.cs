using Server.Models.DTOs.BaseDTOs;

namespace Server.Models.DTOs
{
    public class GeneralAdDTO: BaseDTO
    {
        public bool Level { get; set; }
        public bool Speciallevel { get; set; }
        public byte Status { get; set; }
        public int? SectorID { get; set; }

    }
}
