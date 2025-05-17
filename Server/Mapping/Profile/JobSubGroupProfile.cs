using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs;
using Server.Models.DTOs.BaseDTOs;
using Server.Models.DTOs.JobSubGroup;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class JobSubGroupProfile
    {
        // Mapping from CreateBaseDTO to JobSubGroup
        public partial JobSubGroup MapCreateJobSubGroupDTOToJobSubGroup(CreateJobSubGroupDTO baseDTO);

        public partial CreateJobSubGroupDTO MapJobSubGroupToCreateJobSubGroupDTO(JobSubGroup jobSubGroup);

        // Mapping a list of JobSubGroup to a list of JobSubGroupDTO
        public partial IEnumerable<JobSubGroupDTO> MapJobSubGroupListToJobSubGroupDTOList(IEnumerable<JobSubGroup> jobSubGroups);

     //   public partial IEnumerable<CreateJobSubGroupDTO> MapJobSubGroupListToCreateJobSubGroupDTOList(IEnumerable<JobSubGroup> jobSubGroups);  
      
        // Mapping from JobSubgroup to JobSubGroupDTO
        public partial JobSubGroupDTO JobSubGroup_To_JobSubGroupDTO(JobSubGroup jobSubGroup);


      
    }
}
