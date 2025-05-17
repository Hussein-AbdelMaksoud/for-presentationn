using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.SocialState;


namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class SocialStateProfile
    {
        public partial SocialState SocialStateDTO_To_SocialState(SocialStateDTO SocialStateDTO);

        public partial SocialStateDTO SocialState_To_SocialStateDTO(SocialState SocialState);
        public partial GetSocialStateDTO SocialState_To_GetSocialStateDTO(SocialState SocialState);
        public partial IEnumerable<GetSocialStateDTO> SocialStateList_To_GetSocialStateDTOList(IEnumerable<SocialState> SocialStates);

        public partial IEnumerable<SocialStateDTO> List_To_DTOList(IEnumerable<SocialState> SocialStates);

    }
}
