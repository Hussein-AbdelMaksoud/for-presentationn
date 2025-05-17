using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.MandateType;

namespace Server.Models.Profile
{
    [Mapper]
    public partial class MandateTypeProfile
    {
        /// <summary>
        /// Maps MandateType entity to MandateTypeDTO.
        /// </summary>
        public partial MandateTypeDTO ToDTO(MandateType entity);

        /// <summary>
        /// Maps CreateMandateTypeDTO to MandateType entity.
        /// </summary>
        public partial MandateType ToCreateEntity(CreateMandateTypeDTO dto);


        /// <summary>
        /// Maps a list of MandateType entities to a list of MandateTypeDTOs.
        /// </summary>
        public partial IEnumerable<MandateTypeDTO> ToDTOs(IEnumerable<MandateType> entities);

    }
}
