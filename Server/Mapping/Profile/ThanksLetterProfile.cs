using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.BaseDTOs;
using Server.Models.DTOs.ThanksLetter;
using Server.Models.DTOs.Vacation;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class ThanksLetterProfile
    {
        public partial ThanksLetter ThanksLetterDTO_To_ThanksLetter(CreateThanksLetterDTO ThanksLetter);

        public partial CreateThanksLetterDTO ThanksLetter_To_ThanksLetterDTO(ThanksLetter ThanksLetter);
        public partial ThanksLetterDTO ThanksLetter_To_GetThanksLetterDTO(ThanksLetter ThanksLetter);
        public partial IEnumerable<ThanksLetterDTO> ThanksLetterList_To_GetThanksLetterDTOList(IEnumerable<ThanksLetter> ThanksLetters);

        public partial IEnumerable<CreateThanksLetterDTO> List_To_DTOList(IEnumerable<ThanksLetter> ThanksLetters);
    }
}
