using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.Qualification;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class QualificationProfile
    {
        public partial Qualification QualificationDTO_To_Qualification(QualificationDTO QualificationDTO);
        [MapProperty(nameof(Qualification.Employee.Name), nameof(QualificationDTO.EmpName))]
        public partial QualificationDTO Qualification_To_QualificationDTO(Qualification qualification);

        [MapProperty(nameof(Qualification.Employee.Name), nameof(QualificationDTO.EmpName))]
        public partial IEnumerable<QualificationDTO> QualificationList_To_QualificationDTOList(IEnumerable<Qualification> Qualifications);

        public partial Qualification CreateQualification_To_Qualification(CreateQualificationDTO createQualificationDTO);
    }
}
