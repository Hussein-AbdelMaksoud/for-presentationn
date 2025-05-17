using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.Governrate;

namespace Server.Models.Profile
{
    [Mapper]
    public partial class GovernrateProfile
    {
        // Entity -> DTO mappings
        public partial GovernrateDTO ToDto(Governrate source);
        public partial IEnumerable<GovernrateDTO> ToDtos(IEnumerable<Governrate> sources);

        // Reverse mapping: DTO -> Entity
        public partial Governrate ToEntity(CreateGovernrateDTO dto);
    }
}
