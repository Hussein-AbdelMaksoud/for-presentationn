using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.BaseDTOs;
using Server.Models.DTOs.FincialDegree;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class FincialDegreeProfile
    {
        // Mapping from CreateDTO to FincialDegree
        public partial FincialDegree MapCreateFincialDTOToFincialDegree(CreateFincialDegreeDTO createFinDegDTO);

        // Mapping from FincialDegree to CreateDTO
        public partial CreateFincialDegreeDTO MapFincialDegreeToCreateCreateFincialDegreeDTO(FincialDegree fincialDegree);

        public partial IEnumerable<FincialDegreeDTO> MapFincialDegreeListToFincialDegreeDTOList(IEnumerable<FincialDegree> fincialDegrees);


        // Mapping from FincialDegreeType to FincialDTODTO
        public partial FincialDegreeDTO MapFincialDegreeToFincialDegreeDTO(FincialDegree fincialDegree);
    }
}
