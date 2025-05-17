using Server.Data.Enums;

namespace Server.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Data.Entities;


public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var converter = new EnumToStringConverter<Gender>();
        modelBuilder.Entity<Employee>()
            .Property(e => e.Gender)
            .HasConversion(converter);


    }


    public DbSet<AllowanceType> AllowanceTypes { get; set; }
    public DbSet<ExistaceCase> ExistanceCases { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<FinicialZemaType> FinicialZemaTypes { get; set; }
    public DbSet<Governrate> Governrates { get; set; }
    public DbSet<Allowance> Allowances { get; set; }
    public DbSet<FinicialZema> FinicialZemas { get; set; }
    public DbSet<YearReportLaw> YearReportLaws { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Salary> Salaries { get; set; }
    public DbSet<ThanksLetter> ThanksLetters { get; set; }
    public DbSet<Vacation> Vacations { get; set; }
    public DbSet<YearReport> YearReports { get; set; }

    public DbSet<EducationalLevel> EducationalLevels { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }

    public DbSet<Training> Training { get; set; }

    public DbSet<Sector> Sectors { get; set; }
    public DbSet<GeneralAd> generalAds { get; set; }
    public DbSet<SubAd> subAds { get; set; }
    public DbSet<Department> departments { get; set; }



    public DbSet<HealthState> HealthStates { get; set; }
    public DbSet<AdKind> AdKinds { get; set; }
    public DbSet<JobType> JobTypes { get; set; }
    public DbSet<MandateType> MandateTypes { get; set; }
    public DbSet<MilitaryStateType> MilitaryStateTypes { get; set; }
    public DbSet<NonExistanceType> NonExistanceTypes { get; set; }
    public DbSet<Lagna> Lagnas { get; set; }
    public DbSet<MilitaryState> MilitaryStates { get; set; }
    public DbSet<MandateData> MandateDatas { get; set; }




    public DbSet<PenaltyCase> PenaltyCases { get; set; }
    public DbSet<PenaltyType> PenaltyTypes { get; set; }
    public DbSet<QualGrade> QualGrades { get; set; }
    public DbSet<SocialState> SocialStates { get; set; }
    public DbSet<VacationType> VacationTypes { get; set; }
    public DbSet<YearReportGrade> YearReportGrades { get; set; }

    public DbSet<Penalty>Penalties { get; set; }



    public DbSet<FincialDegree> FincialDegrees { get; set; }
    public DbSet<FincialDegreeType> FincialDegreeTypes { get; set; }
    public DbSet<JobGroup> JobGroups { get; set; }
    public DbSet<JobName> JobNames { get; set; }
    public DbSet<JobSubGroup> JobSubGroups { get; set; }
    public DbSet<JobDegredationData> JobDegredations { get; set; }




}
