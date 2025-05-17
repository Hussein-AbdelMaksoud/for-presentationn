using Server.Mapping.Profile;
using Server.Models.Profile;

public static class ProfileServiceExtensions
{
    public static IServiceCollection AddProfileServices(this IServiceCollection services)
    {
        #region MEga

        services.AddScoped<AllowanceTypeProfile>();
        services.AddScoped<AllowanceProfile>();
        services.AddScoped<ExistaceCaseProfile>();
        services.AddScoped<FinicialZemaTypeProfile>();
        services.AddScoped<FacultyProfile>();
        services.AddScoped<GovernrateProfile>();
        services.AddScoped<FinicialZemaProfile>();
        services.AddScoped<YearReportLawProfile>();
        services.AddScoped<EmployeeProfile>();



        #endregion

        #region Marwa

        services.AddScoped<AdKindProfile>();
        services.AddScoped<HealthStateProfile>();
        services.AddScoped<JobTypeProfile>();
        services.AddScoped<MandateTypeProfile>();
        services.AddScoped<MilitaryStateTypeProfile>();
        services.AddScoped<NonExistanceTypeProfile>();
        services.AddScoped<MilitaryStateProfile>();
        services.AddScoped<MandateDataProfile>();
        services.AddScoped<LagnaProfile>();


        #endregion

        #region Omar

        services.AddScoped<PenaltyCaseProfile>();
        services.AddScoped<PenaltyTypeProfile>();
        services.AddScoped<YearReportGradeProfile>();
        services.AddScoped<QualGradeProfile>();
        services.AddScoped<VacationTypeProfile>();
        services.AddScoped<SocialStateProfile>();

        services.AddScoped<YearReportProfile>();
        services.AddScoped<VacationProfile>();
        services.AddScoped<SalaryProfile>();
        services.AddScoped<PenaltyProfile>();
        services.AddScoped<ThanksLetterProfile>();




        #endregion

        #region amr
        services.AddScoped<FincialDegreeProfile>();
        services.AddScoped<FincialDegreeTypeProfile>();
        services.AddScoped<JobGroupProfile>();
        services.AddScoped<JobSubGroupProfile>();
        services.AddScoped<JobNameProfile>();
        services.AddScoped<JobDegredationDataProfile>();
        #endregion

        #region Hussein
        services.AddScoped<SectorProfile>();
        services.AddScoped<GeneralAdProfile>();
        services.AddScoped<SubadProfile>();
        services.AddScoped<DepartmentProfile>();
        services.AddScoped<QualificationProfile>();
        services.AddScoped<EducationLevelProfile>();
        services.AddScoped<CertificateProfile>();
        services.AddScoped<TrainingProfile>();
        #endregion

        return services;
    }
}