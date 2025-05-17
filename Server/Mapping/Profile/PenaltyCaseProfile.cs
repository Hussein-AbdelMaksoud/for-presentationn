using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.PenaltyCase;
using Server.Models.DTOs.Vacation;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class PenaltyCaseProfile
    {
        public partial PenaltyCase PenaltyCaseDTO_To_PenaltyCase(PenaltyCaseDTO PenaltyCaseDTO);

        public partial PenaltyCaseDTO PenaltyCase_To_PenaltyCaseDTO(PenaltyCase PenaltyCase);
        public partial GetPenaltyCaseDTO PenaltyCase_To_GetPenaltyCaseDTO(PenaltyCase PenaltyCase);
        public partial IEnumerable<GetPenaltyCaseDTO> PenaltyCaseList_To_GetPenaltyCaseDTOList(IEnumerable<PenaltyCase> PenaltyCases);

        public partial IEnumerable<PenaltyCaseDTO> List_To_DTOList(IEnumerable<PenaltyCase> PenaltyCases);

    }
}
