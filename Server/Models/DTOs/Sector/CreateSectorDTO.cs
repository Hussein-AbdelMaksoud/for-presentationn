using Server.Models.DTOs.BaseDTOs;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.Sector
{
    public class CreateSectorDTO:CreateBaseDTO
    {
        [Range(1,byte.MaxValue)]
        public byte Status { get; set; }
    }
}
