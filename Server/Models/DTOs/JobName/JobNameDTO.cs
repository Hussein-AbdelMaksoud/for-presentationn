using Server.Models.DTOs.BaseDTOs;
using System.ComponentModel.DataAnnotations;
namespace Server.Models.DTOs.JobName
{
    public class JobNameDTO:BaseDTO
    {
       
        public string JobMission { get; set; }
        public int? JobSubGroupId { get; set; }
    }
}
