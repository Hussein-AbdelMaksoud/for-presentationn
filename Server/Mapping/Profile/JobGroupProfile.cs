using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.BaseDTOs;
using Server.Models.DTOs.JobGroup;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class JobGroupProfile
    {
        // Mapping from CreateBaseDTO to JobGroup
        public partial JobGroup MapCreateBaseDTOToJobGroup(CreateBaseDTO baseDTO);

        // Mapping from JobGroup to CreateBaseDTO
        public partial CreateBaseDTO MapJobGroupToCreateBaseDTO(JobGroup jobGroup);

     
        // Mapping a list of JobGroup to a list of BaseDTO
        public partial IEnumerable<JobGroupDTO> MapJobGroupListToBaseDTOList(IEnumerable<JobGroup> jobGroups);

        // Mapping a list of BaseDTO to a list of JobGroup
      //  public partial IEnumerable<CreateJobGroupDTO> MapMapJobGroupListToCreateJobGroupListDTO(IEnumerable<JobGroup> jobGroups);

        // Mapping from Jobgroup to BaseDTO
        public partial BaseDTO MapJobGroupToBaseDTO(JobGroup jobGroup);
    }
}
