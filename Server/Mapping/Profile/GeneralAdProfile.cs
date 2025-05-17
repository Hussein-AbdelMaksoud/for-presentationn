using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs;
using Server.Models.DTOs.GeneralAd;

namespace Server.Models.Profile
{
    [Mapper]
    public partial class GeneralAdProfile
    {
        public partial GeneralAd CreateGeneralAdDTO_To_GeneralAd(CreateGeneralAdDTO generalAdDTO);

        public partial CreateGeneralAdDTO GeneralAd_To_CreateGeneralAdDTO(GeneralAd generalAd);

        public partial IEnumerable<CreateGeneralAdDTO> GeneralAdList_To_CreateGeneralAdList(IEnumerable<GeneralAd> generalAds);

        public partial GeneralAdDTO GeneralAd_To_GeneralAdDTO(GeneralAd generalAd);
        public partial IEnumerable<GeneralAdDTO> GeneralAdList_To_GeneralAdListDTO(IEnumerable<GeneralAd> generalAds);



    }
}
