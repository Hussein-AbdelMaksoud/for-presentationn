namespace Server.Services;


// ServiceRegistration.cs (or DependencyInjection.cs)
using Microsoft.Extensions.DependencyInjection;
using Server.Data.Entities;
using Server.Services.Repositories.Implementations;
using Server.Services.Repositories.Interfaces;
using Server.Services.UnitOfWork.Interfaces;

public static class ServiceRegistration
{
    public static void AddRepositories(this IServiceCollection services)
    {


        // Register UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork.Implementations.UnitOfWork>();

        // Register repositories for all entities dynamically
        var entityTypes = new[] {
            typeof(AllowanceType),
            typeof(ExistaceCase),
            typeof(Faculty),
            typeof(FinicialZemaType),
            typeof(Governrate),
            typeof(Allowance),
            typeof(FinicialZema),
            typeof(Employee),
            typeof(YearReport),
            typeof(Vacation),
            typeof(Salary),
            typeof(ThanksLetter),




            typeof(HealthState),
            typeof(AdKind),
            typeof(JobType),
            typeof(MandateType),
            typeof(MilitaryStateType),
            typeof(NonExistanceType),
            typeof(MandateData),
            typeof(MilitaryState),
            typeof(Lagna),


            typeof(EducationalLevel),
            typeof(Certificate),
            typeof(Sector),
            typeof(GeneralAd),
            typeof(SubAd),
            typeof(Department),
            typeof(Qualification),
            typeof(Training),

            typeof(PenaltyType),
            typeof(PenaltyCase),
            typeof(VacationType),
            typeof(YearReportGrade),
            typeof(QualGrade),
            typeof(SocialState),
            
            typeof(Penalty),

            typeof(FincialDegree),
            typeof(FincialDegreeType),
            typeof(JobSubGroup),
            typeof(JobGroup),
            typeof(JobName),
            typeof(JobDegredationData)
        };

        foreach (var entityType in entityTypes)
        {
            var repositoryType = typeof(IGenericRepository<>).MakeGenericType(entityType);
            var concreteRepositoryType = typeof(GenericRepository<>).MakeGenericType(entityType);
            services.AddScoped(repositoryType, concreteRepositoryType);
        }
    }
}
