using Server.Models.DTOs.BaseDTOs;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.Department
{
    public class CreateDepartmentDTO:CreateBaseDTO
    {
        [Range(1,byte.MaxValue)]
        public byte Status { get; set; }
        public int? SubAdID { get; set; }
    }
}
