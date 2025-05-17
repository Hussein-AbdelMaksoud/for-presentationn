using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.Sector;

namespace Server.Models.Profile
{
    [Mapper]
    public partial class SectorProfile
    {
        public partial Sector CreateSectorDTO_To_Sector(CreateSectorDTO SectorDTO);


        public partial CreateSectorDTO Sector_To_CreateSectorDTO(Sector sector);

        public partial IEnumerable<Sector> CreateSectorDTOList_To_SectorList(IEnumerable<CreateSectorDTO> sectorDTOs);


        public partial SectorDTO Sector_To_SectorDTO(Sector sector);

        public partial IEnumerable<SectorDTO> SectorList_To_SectorDTOList(IEnumerable<Sector> sectors);

    }
}
