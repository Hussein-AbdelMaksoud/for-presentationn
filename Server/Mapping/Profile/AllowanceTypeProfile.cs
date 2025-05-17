using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.AllowanceType;

namespace Server.Models.Profile
{
    [Mapper]
    public partial class AllowanceTypeProfile
    {
        // Entity -> DTO mappings
        public partial AllowanceTypeDTO ToDto(AllowanceType source);
        public partial IEnumerable<AllowanceTypeDTO> ToDtos(IEnumerable<AllowanceType> sources);

        // Reverse mapping: DTO -> Entity
        public partial AllowanceType ToEntity(CreateAllowanceTypeDTO dto);
    }
}