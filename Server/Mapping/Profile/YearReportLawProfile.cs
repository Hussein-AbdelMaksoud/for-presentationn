using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.YearReportLaw_;


[Mapper]
public partial class YearReportLawProfile
{
    // Entity -> DTO mappings
    public partial YearReportLawDTO ToDto(YearReportLaw source);
    public partial IEnumerable<YearReportLawDTO> ToDtos(IEnumerable<YearReportLaw> sources);

    // DTO -> Entity mappings
    public partial YearReportLaw ToEntity(CreateYearReportLawDTO dto);
}