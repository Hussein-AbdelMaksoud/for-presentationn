using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.BaseDTOs;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class FincialDegreeTypeProfile
    {
        // Mapping from CreateBaseDTO to FincialDegreeType
        public partial FincialDegreeType MapCreateBaseDTOToFincialDegreeType(CreateBaseDTO baseDTO);

        // Mapping from FincialDegreeType to CreateBaseDTO
        public partial CreateBaseDTO MapFincialDegreeTypeToCreateBaseDTO(FincialDegreeType fincialDegreeType);

        // Mapping a list of FincialDegreeType to a list of BaseDTO
        public partial IEnumerable<BaseDTO> MapFincialDegreeTypeListToBaseDTOList(IEnumerable<FincialDegreeType> fincialDegreeTypes);

        // Mapping a list of BaseDTO to a list of FincialDegreeType
        //  public partial IEnumerable<CreateFincialDegreeTypeDTO> MapBaseListToCreateFincialDegreeTypeDTOList(IEnumerable<FincialDegreeType> baseDTOs);

        // Mapping from FincialDegreeType to BaseDTO
        public partial BaseDTO MapFincialDegreeTypeToBaseDTO(FincialDegreeType fincialDegreeType);

    }

}
