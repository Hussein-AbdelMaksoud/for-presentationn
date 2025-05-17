using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.Employee;


namespace Server.Models.Profile
{
    [Mapper]
    public partial class EmployeeProfile
    {
        public partial EmployeeDTO ToDTO(Employee entity);


        public partial IEnumerable<EmployeeDTO> ToDTOs(IEnumerable<Employee> entities);
        public partial Employee ToEntity(CreateEmployeeDTO dto);
        [MapProperty(nameof(CreateEmployeeDTO.NationalId), nameof(JobDegredationData.EmpId))]
        public partial JobDegredationData ToJobDegredationData(CreateEmployeeDTO dto);




    }
}