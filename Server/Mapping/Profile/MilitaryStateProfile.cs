using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.MilitaryState;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class MilitaryStateProfile
    {
        /// <summary>
        /// Maps MilitaryState entity to MilitaryStateDTO.
        /// </summary>
        public partial MilitaryStateDTO ToDTO(MilitaryState entity);
        /// <summary>
        /// Maps CreateMilitaryStateDTO to MilitaryState entity.
        /// </summary>
        public partial MilitaryState ToCreateEntity(CreateMilitaryStateDTO dto);
        /// <summary>
        /// Maps a list of MilitaryState entities to a list of MilitaryStateDTOs.
        /// </summary>
        public partial IEnumerable<MilitaryStateDTO> ToDTOs(IEnumerable<MilitaryState> entities);
    }

}
