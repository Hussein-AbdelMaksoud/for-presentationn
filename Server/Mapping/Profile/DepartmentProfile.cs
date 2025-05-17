using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.Department;
using Server.Models.DTOs.Department;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class DepartmentProfile
    {
        public partial Department DepartmentDTO_To_Department(DepartmentDTO DepartmentDTO);

        public partial DepartmentDTO Department_To_DepartmentDTO(Department Department);

        public partial IEnumerable<DepartmentDTO> DepartmentList_To_DepartmentDTOList(IEnumerable<Department> Departments);

        public partial Department CreateDepartment_To_Department(CreateDepartmentDTO createDepartmentDTO);
    }
}
