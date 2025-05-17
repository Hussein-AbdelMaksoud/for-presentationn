using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.PenaltyType;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class PenaltyTypeProfile
    {
        public partial PenaltyType PenaltyTypeDTO_To_PenaltyType(PenaltyTypeDTO PenaltyTypeDTO);

        public partial PenaltyTypeDTO PenaltyType_To_PenaltyTypeDTO(PenaltyType PenaltyType);
        public partial GetPenaltyTypeDTO PenaltyType_To_GetPenaltyTypeDTO(PenaltyType PenaltyType);
        public partial IEnumerable<GetPenaltyTypeDTO> PenaltyTypeList_To_GetPenaltyTypeDTOList(IEnumerable<PenaltyType> PenaltyTypes);

        public partial IEnumerable<PenaltyTypeDTO> List_To_DTOList(IEnumerable<PenaltyType> PenaltyTypes);

    }
}
