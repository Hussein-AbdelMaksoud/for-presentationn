using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.Allowance;


namespace Server.Models.Profile
{
    [Mapper]
    public partial class AllowanceProfile
    {
        // Entity -> DTO

        public partial AllowanceDTO ToDto(Allowance Entity);

        public partial IEnumerable<AllowanceDTO> ToDtos(IEnumerable<Allowance> Entities);



        // DTO -> Entity mappings
        public partial Allowance ToEntity(CreateAllowanceDTO dto);
    }
}