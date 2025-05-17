using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.YearReportGrade;

namespace Server.Mapping.Profile
{
    [Mapper]
    
    public partial class YearReportGradeProfile
    {
        public partial YearReportGrade YearReportGradeDTO_To_YearReportGrade(YearReportGradeDTO YearReportGradeDTO);

        public partial YearReportGradeDTO YearReportGrade_To_YearReportGradeDTO(YearReportGrade YearReportGrade);
        public partial GetYearReportGradeDTO YearReportGrade_To_GetYearReportGradeDTO(YearReportGrade YearReportGrade);
        public partial IEnumerable<GetYearReportGradeDTO> YearReportGradeList_To_GetYearReportGradeDTOList(IEnumerable<YearReportGrade> YearReportGrades);

        public partial IEnumerable<YearReportGradeDTO> List_To_DTOList(IEnumerable<YearReportGrade> YearReportGrades);

    }
}
