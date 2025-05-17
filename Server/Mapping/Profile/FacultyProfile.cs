using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.Faculty;

namespace Server.Models.Profile
{
    [Mapper]
    public partial class FacultyProfile
    {
        // Entity -> DTO mappings
        public partial FacultyDTO ToDto(Faculty source);
        public partial IEnumerable<FacultyDTO> ToDtos(IEnumerable<Faculty> sources);

        // Reverse mapping: DTO -> Entity
        public partial Faculty ToEntity(CreateFacultyDTO dto);
    }
}
