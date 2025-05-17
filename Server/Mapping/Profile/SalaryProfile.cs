using Server.Data.Entities;
using Riok.Mapperly.Abstractions;
using Server.Models.DTOs.Salary;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class SalaryProfile
    {
        public partial Salary SalaryDTO_To_Salary(CreateSalaryDTO SalaryDTO);

        public partial CreateSalaryDTO Salary_To_SalaryDTO(Salary Salary);
        public partial SalaryDTO Salary_To_GetSalaryDTO(Salary Salary);
        public partial IEnumerable<SalaryDTO> SalaryList_To_GetSalaryDTOList(IEnumerable<Salary> Salarys);

        public partial IEnumerable<CreateSalaryDTO> List_To_DTOList(IEnumerable<Salary> Salarys);
    }

}
