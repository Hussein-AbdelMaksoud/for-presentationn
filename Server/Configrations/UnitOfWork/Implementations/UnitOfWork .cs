using Server.Data;
using Server.Data.Entities;
using Server.Services.Repositories.Implementations;
using Server.Services.Repositories.Interfaces;
using Server.Services.UnitOfWork.Interfaces;

namespace Server.Services.UnitOfWork.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _context;

    public UnitOfWork(ApplicationDBContext context)
    {
        _context = context;
        AllowanceTypes = new GenericRepository<AllowanceType>(_context);
        ExistanceCases = new GenericRepository<ExistaceCase>(_context);
        Faculties = new GenericRepository<Faculty>(_context);
        FinicialZemaTypes = new GenericRepository<FinicialZemaType>(_context);
        Governrates = new GenericRepository<Governrate>(_context);
        FinicialZemas = new GenericRepository<FinicialZema>(_context);
        Allowances = new GenericRepository<Allowance>(_context);
        Employees = new GenericRepository<Employee>(_context);
        YearReportLaws = new GenericRepository<YearReportLaw>(_context);

        Salaries = new GenericRepository<Salary>(_context);
        ThanksLetters = new GenericRepository<ThanksLetter>(_context);
        Vacations = new GenericRepository<Vacation>(_context);
        YearReports = new GenericRepository<YearReport>(_context);


        EducationalLevels = new GenericRepository<EducationalLevel>(_context);
        Certificates = new GenericRepository<Certificate>(_context);
        Qualifications = new GenericRepository<Qualification>(_context);
        Trainings = new GenericRepository<Training>(_context);
        Sectors = new GenericRepository<Sector>(_context);
        GeneralAds = new GenericRepository<GeneralAd>(_context);
        SubAds = new GenericRepository<SubAd>(_context);
        Departments = new GenericRepository<Department>(_context);



        HealthStates = new GenericRepository<HealthState>(_context);
        AdKinds = new GenericRepository<AdKind>(_context);
        JobTypes = new GenericRepository<JobType>(_context);
        MandateTypes = new GenericRepository<MandateType>(_context);
        MilitaryStateTypes = new GenericRepository<MilitaryStateType>(_context);
        NonExistanceTypes = new GenericRepository<NonExistanceType>(_context);
        MandateDatas = new GenericRepository<MandateData>(_context);
        MilitaryStates = new GenericRepository<MilitaryState>(_context);
        Lagnas = new GenericRepository<Lagna>(_context);



        PenaltyCases = new GenericRepository<PenaltyCase>(_context);
        PenaltyTypes = new GenericRepository<PenaltyType>(_context);
        QualGrades = new GenericRepository<QualGrade>(_context);
        SocialStates = new GenericRepository<SocialState>(_context);
        VacationTypes = new GenericRepository<VacationType>(_context);
        YearReportGrades = new GenericRepository<YearReportGrade>(_context);
        Penalties = new GenericRepository<Penalty> (_context);
        



        FincialDegrees = new GenericRepository<FincialDegree>(_context);
        FincialDegreeTypes = new GenericRepository<FincialDegreeType>(_context);
        JobGroups = new GenericRepository<JobGroup>(_context);
        JobSubGroups = new GenericRepository<JobSubGroup>(_context);
        JobNames = new GenericRepository<JobName>(_context);
        JobDegredationDatas=new GenericRepository<JobDegredationData>(_context);




    }

    public IGenericRepository<AllowanceType> AllowanceTypes { get; }
    public IGenericRepository<ExistaceCase> ExistanceCases { get; }
    public IGenericRepository<Faculty> Faculties { get; }
    public IGenericRepository<FinicialZemaType> FinicialZemaTypes { get; }
    public IGenericRepository<Governrate> Governrates { get; }
    public IGenericRepository<FinicialZema> FinicialZemas { get; }
    public IGenericRepository<Allowance> Allowances { get; }
    public IGenericRepository<Employee> Employees { get; }
    public IGenericRepository<YearReportLaw> YearReportLaws { get; }


    public IGenericRepository<Salary> Salaries { get; }
    public IGenericRepository<Vacation> Vacations { get; }
    public IGenericRepository<YearReport> YearReports { get; }
    public IGenericRepository<ThanksLetter> ThanksLetters { get; }







    public IGenericRepository<EducationalLevel> EducationalLevels { get; }
    public IGenericRepository<Certificate> Certificates { get; }
    public IGenericRepository<Qualification> Qualifications { get; }
    public IGenericRepository<Training> Trainings { get; }
    public IGenericRepository<Sector> Sectors { get; }
    public IGenericRepository<GeneralAd> GeneralAds { get; }
    public IGenericRepository<SubAd> SubAds { get; }
    public IGenericRepository<Department> Departments { get; }


    public IGenericRepository<HealthState> HealthStates { get; }
    public IGenericRepository<AdKind> AdKinds { get; }
    public IGenericRepository<JobType> JobTypes { get; }
    public IGenericRepository<MandateType> MandateTypes { get; }
    public IGenericRepository<MilitaryStateType> MilitaryStateTypes { get; }
    public IGenericRepository<NonExistanceType> NonExistanceTypes { get; }
    public IGenericRepository<MandateData> MandateDatas { get; }
    public IGenericRepository<MilitaryState> MilitaryStates { get; }
    public IGenericRepository<Lagna> Lagnas { get; }

    public IGenericRepository<PenaltyCase> PenaltyCases { get; }
    public IGenericRepository<PenaltyType> PenaltyTypes { get; }
    public IGenericRepository<QualGrade> QualGrades { get; }
    public IGenericRepository<SocialState> SocialStates { get; }
    public IGenericRepository<VacationType> VacationTypes { get; }
    public IGenericRepository<YearReportGrade> YearReportGrades { get; }
    public IGenericRepository<Penalty> Penalties { get; }



    public IGenericRepository<FincialDegree> FincialDegrees { get; }
    public IGenericRepository<FincialDegreeType> FincialDegreeTypes { get; }
    public IGenericRepository<JobGroup> JobGroups { get; }
    public IGenericRepository<JobSubGroup> JobSubGroups { get; }
    public IGenericRepository<JobName> JobNames { get; }
    public IGenericRepository<JobDegredationData> JobDegredationDatas { get; }
    public async Task SaveAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
