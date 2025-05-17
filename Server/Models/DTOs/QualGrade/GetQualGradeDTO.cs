using Server.Models.DTOs.BaseDTOs;

namespace Server.Models.DTOs.QualGrade
{
    public class GetQualGradeDTO:BaseDTO
    {
        public int ? Grade { get; set; }
    }
}
