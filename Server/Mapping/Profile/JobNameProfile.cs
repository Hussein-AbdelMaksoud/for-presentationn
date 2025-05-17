using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.BaseDTOs;
using Server.Models.DTOs.JobName;
using Server.Models.DTOs.JobSubGroup;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class JobNameProfile
    {
        public partial JobName MapCreateJobNameDTOToJobName(CreateJobNameDTO baseDTO);

        public partial CreateJobNameDTO MapJobNameToJobNameDTO(JobName jobName);

      
        // Mapping a list of JobName to a list of JobNameDTO
        public partial IEnumerable<JobNameDTO> MapJobNameListToJobNameDTOList(IEnumerable<JobName> jobNames);

        public partial IEnumerable<CreateJobNameDTO> MapJobNameListToCreateJobNameDTOList(IEnumerable<JobName> baseDTOs);

        // Mapping from JobName to JobNameDTO
        public partial JobNameDTO JobName_To_JobNameDTO(JobName jobName);
    }
}
