using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.EducationLevel;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class EducationLevelProfile
    {
        public partial EducationalLevel Map(CreateEducationLevelDTO createEducationLevelDTO);
        public partial EducationLevelDTO Map(EducationalLevel EducationLevel);

        public partial EducationalLevel Map(EducationLevelDTO EducationLevelDTO);
        public partial IEnumerable<EducationLevelDTO> Map(IEnumerable<EducationalLevel> EducationLevels);
    }
}
