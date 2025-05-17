using Server.Data.Enums;

namespace Server.Models.DTOs.Employee;

public class CreateEmployeeDTO
{
    [NationalId]
    public string NationalId { get; set; }

    public string FileId { get; set; }

    [TextOnly]
    public string Name { get; set; }

    public int FacultyId { get; set; }

    public int JobGroupId { get; set; }

    public int JobSubGroupId { get; set; }

    public int JobNameId { get; set; }

    public int? SubAdId { get; set; }

    public int? DepartmentId { get; set; }

    public bool IsExist { get; set; }

    public int? ExistaceCaseId { get; set; }

    public int? NonExistanceTypeId { get; set; }

    public int? TaminNo { get; set; }

    public DateOnly AppointDate { get; set; }

    public DateOnly WorkEndDate { get; set; }

    public Gender Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string BirthPlace { get; set; } = string.Empty;

    public string Tel { get; set; } = string.Empty;

    public string Mobile { get; set; } = string.Empty;

    public DateOnly? WorkDate { get; set; }

    public int? HealthStateId { get; set; }

    public int? AppointDN { get; set; }

    public DateOnly? ExperanceDate { get; set; }

    public int? ExperanceDN { get; set; }

    public int? Serial { get; set; }

    public int? SocialStateId { get; set; }

    public int? GovernrateId { get; set; }

    public string Address { get; set; } = string.Empty;

    public DateOnly? WorkDateFt { get; set; }

    public DateOnly? ReAppointDate { get; set; }

    public DateOnly? CombinationDate { get; set; }

    public string AppointDateTxt { get; set; } = string.Empty;

    public string WorkDateTxt { get; set; } = string.Empty;

    public string WorkDateFtTxt { get; set; } = string.Empty;

    public string ReAppointDateTxt { get; set; } = string.Empty;

    public string CombinationDateTxt { get; set; } = string.Empty;

    public string ExperanceDateTxt { get; set; } = string.Empty;

    public string BoundDegree { get; set; } = string.Empty;

    public string ExperanceDomain { get; set; } = string.Empty;

    public int? WorkEndDec { get; set; }

    public DateOnly? WorkEndDeDate { get; set; }

    public string YearBuy { get; set; } = string.Empty;

    public DateOnly FJobDate { get; set; }

    public string Center { get; set; } = string.Empty;

    public string Village { get; set; } = string.Empty;

    public string YearEmp { get; set; } = string.Empty;

    public decimal? LastBalance { get; set; }

    public decimal? CurrentBalance { get; set; }

    public decimal? LastYearBalance { get; set; }

    public decimal? SickBalance { get; set; }

    public int? ReservedDays { get; set; }

    public int? ReservedMonths { get; set; }

    public int? ReservedYears { get; set; }

    public string DisabilityType { get; set; } = string.Empty;

    public string DisabilityFamilyMember { get; set; } = string.Empty;

    public DateOnly? DegreeDate { get; set; }


    public int Code { get; set; }
    public bool CurrentDegree { get; set; }
    public DateOnly fincialDegreeDate { get; set; }
    public int FincialDegreeId { get; set; }



}
