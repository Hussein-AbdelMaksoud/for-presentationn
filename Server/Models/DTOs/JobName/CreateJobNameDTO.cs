using Server.Models.DTOs.BaseDTOs;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.JobName
{
    public class CreateJobNameDTO:CreateBaseDTO
    {
        public string JobMission { get; set; } = string.Empty;
        public int? JobSubGroupId { get; set; }
    }
}
