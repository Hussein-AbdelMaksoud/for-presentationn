using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.NonExistanceType;

namespace Server.Models.Profile
{
    [Mapper]
    public partial class NonExistanceTypeProfile
    {
        /// <summary>
        /// Maps NonExistanceType entity to NonExistanceTypeDTO.
        /// </summary>
        public partial NonExistanceTypeDTO ToDTO(NonExistanceType entity);

        /// <summary>
        /// Maps CreateNonExistanceTypeDTO to NonExistanceType entity.
        /// </summary>
        public partial NonExistanceType ToCreateEntity(CreateNonExistanceTypeDTO dto);


        /// <summary>
        /// Maps a list of NonExistanceType entities to a list of NonExistanceTypeDTOs.
        /// </summary>
        public partial IEnumerable<NonExistanceTypeDTO> ToDTOs(IEnumerable<NonExistanceType> entities);

    }
}
