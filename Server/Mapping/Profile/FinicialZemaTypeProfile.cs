using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.FinicialZemaType;

namespace Server.Models.Profile
{
    [Mapper]
    public partial class FinicialZemaTypeProfile
    {
        // Entity -> DTO mappings
        public partial FinicialZemaTypeDTO ToDto(FinicialZemaType source);
        public partial IEnumerable<FinicialZemaTypeDTO> ToDtos(IEnumerable<FinicialZemaType> sources);

        // Reverse mapping: DTO -> Entity
        public partial FinicialZemaType ToEntity(CreateFinicialZemaTypeDTO source);
    }
}
