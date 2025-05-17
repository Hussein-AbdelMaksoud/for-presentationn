using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.HealthState;

namespace Server.Models.Profile
{
    [Mapper]
    public partial class HealthStateProfile
    {
        /// <summary>
        /// Maps HealthState entity to HealthStateDTO.
        /// </summary>
        public partial HealthStateDTO ToDTO(HealthState entity);
       
        /// <summary>
        /// Maps CreateHealthStateDTO to HealthState entity.
        /// </summary>
        public partial HealthState ToCreateEntity(CreateHealthStateDTO dto);


        /// <summary>
        /// Maps a list of HealthState entities to a list of HealthStateDTOs.
        /// </summary>
        public partial IEnumerable<HealthStateDTO> ToDTOs(IEnumerable<HealthState> entities);

        
    }
}
