using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.ExistaceCase;

namespace Server.Models.Profile
{
    [Mapper]
    public partial class ExistaceCaseProfile
    {
        // Entity -> DTO mappings
        public partial ExistaceCaseDTO ToDto(ExistaceCase source);
        public partial IEnumerable<ExistaceCaseDTO> ToDtos(IEnumerable<ExistaceCase> sources);

        // Reverse mapping: DTO -> Entity
        public partial ExistaceCase ToEntity(CreateExistaceCaseDTO dto);
    }
}

