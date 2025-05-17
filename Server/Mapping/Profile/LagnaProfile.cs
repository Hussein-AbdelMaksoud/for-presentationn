using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.Lagna;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class LagnaProfile
    {
        /// <summary>
        /// Maps Lagna entity to LagnaDTO.
        /// </summary>
        public partial LagnaDTO ToDTO(Lagna entity);
        /// <summary>
        /// Maps CreateLagnaDTO to Lagna entity.
        /// </summary>
        public partial Lagna ToCreateEntity(CreateLagnaDTO dto);
        /// <summary>
        /// Maps a list of Lagna entities to a list of LagnaDTOs.
        /// </summary>
        public partial IEnumerable<LagnaDTO> ToDTOs(IEnumerable<Lagna> entities);
    }
}
