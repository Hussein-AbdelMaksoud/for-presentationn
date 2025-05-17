using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.SubAd;
using System.Collections.Generic;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class SubadProfile
    {
        public partial SubAd SubAdDTO_To_SubAd(SubAdDTO subadDTO);

        public partial SubAdDTO SubAd_To_SubAdDTO(SubAd subAd);

        public partial IEnumerable<SubAdDTO> SubAdList_To_SubAdDTOList(IEnumerable<SubAd> subAds);

        public partial SubAd CreateSubAd_To_SubAd(CreateSubAdDTO createSubAdDTO);
    }
}
