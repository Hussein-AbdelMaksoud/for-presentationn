using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.QualGrade;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class QualGradeProfile
    {
        public partial QualGrade QualGradeDTO_To_QualGrade(QualGradeDTO QualGradeDTO);

        public partial QualGradeDTO QualGrade_To_QualGradeDTO(QualGrade QualGrade);
        public partial GetQualGradeDTO QualGrade_To_GetQualGradeDTO(QualGrade QualGrade);
        public partial IEnumerable<GetQualGradeDTO> QualGradeList_To_GetQualGradeDTOList(IEnumerable<QualGrade> QualGrades);

        public partial IEnumerable<QualGradeDTO> List_To_DTOList(IEnumerable<QualGrade> QualGrades);

    }
}
