using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.Vacation;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class VacationProfile
    {
        public partial Vacation VacationDTO_To_Vacation (CreateVacationDTO VacationDTO);

        public partial CreateVacationDTO Vacation_To_VacationDTO(Vacation Vacation);
        public partial VacationDTO Vacation_To_GetVacationDTO(Vacation Vacation);
        public partial IEnumerable<VacationDTO> VacationList_To_GetVacationDTOList(IEnumerable<Vacation> Vacations);

        public partial IEnumerable<CreateVacationDTO> List_To_DTOList(IEnumerable<Vacation> Vacations);

     
    }
}
