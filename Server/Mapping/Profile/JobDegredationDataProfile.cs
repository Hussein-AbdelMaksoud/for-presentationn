using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.JobDegredationData;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class JobDegredationDataProfile
    {
        public partial JobDegredationData CreateJobDegDto_TOJobDeg(CreateJobDegredationDataDTO jobDegDTO);

        public partial CreateJobDegredationDataDTO JobDeg_TOCreateJobDeg(JobDegredationData jobDeg);
        public partial IEnumerable<JobDegredationData> CreateJobDegDToList_TOJobDegList(IEnumerable<CreateJobDegredationDataDTO> jobDegs);

        public partial JobDegredationDataDTO JobDeg_ToJobDegDTO(JobDegredationData jobDeg);

        public partial IEnumerable<JobDegredationDataDTO> JobDegList_ToJobDegDTOList(IEnumerable<JobDegredationData> jobDegs);


    }
}
