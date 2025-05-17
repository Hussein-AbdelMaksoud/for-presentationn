using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.MandateData;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class MandateDataProfile
    {
        /// <summary>
        /// Maps MandateData entity to MandateDataDTO.
        /// </summary>
        public partial MandateDataDTO ToDTO(MandateData entity);
        /// <summary>
        /// Maps CreateMandateDataDTO to MandateData entity.
        /// </summary>
        public partial MandateData ToCreateEntity(CreateMandateDataDTO dto);
        /// <summary>
        /// Maps a list of MandateData entities to a list of MandateDataDTOs.
        /// </summary>
        public partial IEnumerable<MandateDataDTO> ToDTOs(IEnumerable<MandateData> entities);
    }
}
