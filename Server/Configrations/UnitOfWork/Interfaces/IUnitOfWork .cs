using Server.Data.Entities;
using Server.Services.Repositories.Interfaces;

namespace Server.Services.UnitOfWork.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<AllowanceType> AllowanceTypes { get; }
    IGenericRepository<ExistaceCase> ExistanceCases { get; }
    IGenericRepository<Faculty> Faculties { get; }
    IGenericRepository<FinicialZemaType> FinicialZemaTypes { get; }
    IGenericRepository<Governrate> Governrates { get; }
    IGenericRepository<FinicialZema> FinicialZemas { get; }
    IGenericRepository<Allowance> Allowances { get; }
    IGenericRepository<Employee> Employees { get; }
    IGenericRepository<YearReportLaw> YearReportLaws { get; }

    IGenericRepository<YearReport> YearReports { get; }
    IGenericRepository<Vacation> Vacations { get; }
    IGenericRepository<ThanksLetter> ThanksLetters { get; }
    IGenericRepository<Salary> Salaries { get; }







    IGenericRepository<EducationalLevel> EducationalLevels { get; }
    IGenericRepository<Certificate> Certificates { get; }
    IGenericRepository<Qualification> Qualifications { get; }
    IGenericRepository<Training> Trainings { get; }
    IGenericRepository<Sector> Sectors { get; }
    IGenericRepository<GeneralAd> GeneralAds { get; }
    IGenericRepository<SubAd> SubAds { get; }
    IGenericRepository<Department> Departments { get; }


    IGenericRepository<HealthState> HealthStates { get; }
    IGenericRepository<AdKind> AdKinds { get; }
    IGenericRepository<JobType> JobTypes { get; }
    IGenericRepository<MandateType> MandateTypes { get; }
    IGenericRepository<MilitaryStateType> MilitaryStateTypes { get; }
    IGenericRepository<NonExistanceType> NonExistanceTypes { get; }
    IGenericRepository<MandateData> MandateDatas { get; }
    IGenericRepository<MilitaryState> MilitaryStates { get; }
    IGenericRepository<Lagna> Lagnas { get; }


    IGenericRepository<PenaltyCase> PenaltyCases { get; }
    IGenericRepository<PenaltyType> PenaltyTypes { get; }
    IGenericRepository<QualGrade> QualGrades { get; }
    IGenericRepository<SocialState> SocialStates { get; }
    IGenericRepository<VacationType> VacationTypes { get; }
    IGenericRepository<YearReportGrade> YearReportGrades { get; }
    IGenericRepository<Penalty>Penalties { get; }
    



    IGenericRepository<FincialDegree> FincialDegrees { get; }
    IGenericRepository<FincialDegreeType> FincialDegreeTypes { get; }
    IGenericRepository<JobGroup> JobGroups { get; }
    IGenericRepository<JobSubGroup> JobSubGroups { get; }
    IGenericRepository<JobName> JobNames { get; }
    IGenericRepository<JobDegredationData> JobDegredationDatas { get; }
    Task SaveAsync();
}
