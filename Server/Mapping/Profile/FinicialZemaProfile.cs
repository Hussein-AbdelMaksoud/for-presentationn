using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.FinicialZema;

namespace Server.Mapping.Profile;

[Mapper]
public partial class FinicialZemaProfile
{
    // Entity -> DTO mappings
    public partial FinicialZemaDTO ToDto(FinicialZema source);

    public partial IEnumerable<FinicialZemaDTO> ToDtos(IEnumerable<FinicialZema> sources);

    // DTO -> Entity mappings
    public partial FinicialZema ToEntity(CreateFinicialZemaDTO dto);
}
