using Server.Models.DTOs.BaseDTOs;

namespace Server.Models.DTOs.Department
{
    public class DepartmentDTO : BaseDTO
    {
        public byte Status { get; set; }
        public int? SubAdID { get; set; }
    }
}
