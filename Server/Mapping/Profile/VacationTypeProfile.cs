using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.VacationType;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class VacationTypeProfile
    {
        public partial VacationType VacationTypeDTO_To_VacationType(VacationTypeDTO VacationTypeDTO);

        public partial VacationTypeDTO VacationType_To_VacationDTO(VacationType VacationType);
        public partial GetVacationTypeDTO VacationType_To_GetVacationDTO(VacationType VacationType);
        public partial IEnumerable<GetVacationTypeDTO> VacationTypeList_To_GetVacationTypeDTOList(IEnumerable<VacationType> VacationTypes);

        public partial IEnumerable<VacationTypeDTO> List_To_DTOList(IEnumerable<VacationType> VacationTypes);

    }
}
