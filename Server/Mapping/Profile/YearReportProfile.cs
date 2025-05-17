using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.YearReport;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class YearReportProfile
    {
        public partial YearReport YearReportDTO_To_YearReport(CreateYearReportDTO YearReportDTO);

        public partial CreateYearReportDTO YearReport_To_YearReportDTO(YearReport YearReport);

        public partial IEnumerable<CreateYearReportDTO> List_To_DTOList(IEnumerable<YearReport> YearReports);

        public partial YearReportDTO YearReport_To_GetYearReportDTO(YearReport YearReport);
        public partial IEnumerable<YearReportDTO> YearReportList_To_GetYearReportDTOList(IEnumerable<YearReport> YearReports);
   
    
    }
}
