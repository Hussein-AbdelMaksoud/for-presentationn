
namespace Server.Models.DTOs.Qualification
{
    public class QualificationDTO
    {
        public int Id { get; set; }

        public int Code { get; set; }

        public string Specialization { get; set; }

        public string QualPlace { get; set; } = string.Empty;

        public int DecisionNumber { get; set; }

        public DateOnly DecisionDate { get; set; }

        public DateOnly GraduationDate { get; set; }

        public bool LastQual { get; set; }

        public int CertificateID { get; set; }

        public int QualGradeID { get; set; }

        public string NationalID { get; set; }

        public string EmpName { get; set; }
    }
}
