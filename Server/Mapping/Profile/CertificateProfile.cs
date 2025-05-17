using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.Certificate;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class CertificateProfile
    {
        [MapProperty(nameof(CreateCertificateDTO.EducationLevelId), nameof(Certificate.educationalLevelID))]
        public partial Certificate CreateCertificateDTO_To_Certificate(CreateCertificateDTO createCertificateDTO);

        [MapProperty(nameof(Certificate.educationalLevelID), nameof(CertificateDTO.EducationLevelId))]
        public partial CertificateDTO Certificate_To_CertificateDTO(Certificate certificate);

        [MapProperty(nameof(CertificateDTO.EducationLevelId), nameof(Certificate.educationalLevelID))]
        public partial Certificate CertificateDTO_To_Certificate(CertificateDTO certificateDTO);
        public partial IEnumerable<CertificateDTO> CertificateList_To_CertificateDTOList(IEnumerable<Certificate> certificates);

    }

}
