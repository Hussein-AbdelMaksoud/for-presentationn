using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.JobType;

namespace Server.Models.Profile
{
    [Mapper]
    public partial class JobTypeProfile
    {

        /// <summary>
        /// Maps JobType entity to JobTypeDTO.
        /// </summary>
        public partial JobTypeDTO ToDTO(JobType entity);

        /// <summary>
        /// Maps CreateJobTypeDTO to JobType entity.
        /// </summary>
        public partial JobType ToCreateEntity(CreateJobTypeDTO dto);


        /// <summary>
        /// Maps a list of JobType entities to a list of JobTypeDTOs.
        /// </summary>
        public partial IEnumerable<JobTypeDTO> ToDTOs(IEnumerable<JobType> entities);

    }
}
