using Server.Models.DTOs.BaseDTOs;

namespace Server.Models.DTOs.JobSubGroup
{
    public class CreateJobSubGroupDTO:CreateBaseDTO
    {
        public int? JobGroupsId { get; set; }
    }
}
