using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.MilitaryStateType;

namespace Server.Models.Profile
{
    [Mapper]
    public partial class MilitaryStateTypeProfile
    {
        /// <summary>
        /// Maps MilitaryStateType entity to MilitaryStateTypeDTO.
        /// </summary>
        public partial MilitaryStateTypeDTO ToDTO(MilitaryStateType entity);

        /// <summary>
        /// Maps CreateMilitaryStateTypeDTO to MilitaryStateType entity.
        /// </summary>
        public partial MilitaryStateType ToCreateEntity(CreateMilitaryStateTypeDTO dto);


        /// <summary>
        /// Maps a list of MilitaryStateType entities to a list of MilitaryStateTypeDTOs.
        /// </summary>
        public partial IEnumerable<MilitaryStateTypeDTO> ToDTOs(IEnumerable<MilitaryStateType> entities);

    }
}