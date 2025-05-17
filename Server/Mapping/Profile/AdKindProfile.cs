using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.AdKind;


namespace Server.Models.Profile
{
    [Mapper]
    public partial class AdKindProfile
    {
        /// <summary>
        /// Maps AdKind entity to AdKindDTO.
        /// </summary>
        public partial AdKindDTO ToDTO(AdKind entity);
        /// <summary>
        /// Maps CreateAdKindDTO to AdKind entity.
        /// </summary>
        public partial AdKind ToCreateEntity(CreateAdKindDTO dto);
        /// <summary>
        /// Maps a list of AdKind entities to a list of AdKindDTOs.
        /// </summary>
        public partial IEnumerable<AdKindDTO> ToDTOs(IEnumerable<AdKind> entities);
     


    }
}
