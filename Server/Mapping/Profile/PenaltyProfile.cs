using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.Penalty;


namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class PenaltyProfile
    {
        public partial Penalty PenaltyDTO_To_Penalty(CreatePenaltyDTO PenaltyDTO);

        public partial CreatePenaltyDTO Penalty_To_PenaltyDTO(Penalty Penalty);
        public partial PenaltyDTO Penalty_To_GetPenaltyDTO(Penalty Penalty);
        public partial IEnumerable<PenaltyDTO> PenaltyList_To_GetPenaltyDTOList(IEnumerable<Penalty> Penaltys);

        public partial IEnumerable<CreatePenaltyDTO> List_To_DTOList(IEnumerable<Penalty> Penaltys);
    }

}
