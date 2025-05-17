using Server.Data;
using Server.Data.Entities;
using Server.Data.Enums;


public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider, ApplicationDBContext context)
    {





        #region Upper




        // Seed Faculties
        if (!context.Faculties.Any())
        {
            context.Faculties.AddRange(
                new Faculty { Code = 1, Name = "ادارة الجامعة" },
                new Faculty { Code = 2, Name = "كلية الهندسة بشبين الكوم" },
                new Faculty { Code = 3, Name = "كلية الهندسة الإلكترونية بمنوف" },
                new Faculty { Code = 4, Name = "كلية التربية بشبين الكوم" },
                new Faculty { Code = 5, Name = "كلية العلوم" },
                new Faculty { Code = 6, Name = "كلية التجارة" },
                new Faculty { Code = 7, Name = "كلية الطب" },
                new Faculty { Code = 8, Name = "كلية الحقوق" },
                new Faculty { Code = 9, Name = "كلية الآداب" },
                new Faculty { Code = 10, Name = "كلية الاقتصاد المنزلى" },
                new Faculty { Code = 13, Name = "كلية التربية النوعية بأشمون" },
                new Faculty { Code = 14, Name = "كلية الحاسبات والمعلومات" },
                new Faculty { Code = 18, Name = "كلية الزراعة" },
                new Faculty { Code = 19, Name = "المعهد الفني للتمريض" },
                new Faculty { Code = 20, Name = "معهد الكبد القومى" },
                new Faculty { Code = 27, Name = "مركز الخدمة العامة" },
                new Faculty { Code = 30, Name = "مطبعة الجامعة" },
                new Faculty { Code = 37, Name = "المدن الجامعية" },
                new Faculty { Code = 38, Name = "كلية التمريض" },
                new Faculty { Code = 47, Name = "كلية التربية الرياضية" },
                new Faculty { Code = 48, Name = "كلية تكنولوجيا العلوم الصحية التطبيقية" },
                new Faculty { Code = 49, Name = "مستشفى معهد الكبد" },
                new Faculty { Code = 50, Name = "وحدة طوخ طنبشا بالادارة العامة" },
                new Faculty { Code = 51, Name = "ادارة المشروع" },
                new Faculty { Code = 52, Name = "كلية الطب البيطرى" },
                new Faculty { Code = 53, Name = "كلية الصيدلة" },
                new Faculty { Code = 54, Name = "ادارة الوافدين" },
                new Faculty { Code = 55, Name = "الإدارة العامة للشئون الطبية" },
                new Faculty { Code = 56, Name = "الإدارة العامة لرعاية الشباب" },
                new Faculty { Code = 57, Name = "كلية التربية للطفولة المبكرة" },
                new Faculty { Code = 58, Name = "كلية طب الاسنان" },
                new Faculty { Code = 59, Name = "كلية الاعلام" },
                new Faculty { Code = 60, Name = "كلية الذكاء الاصطناعى" }
            );
            context.SaveChanges();
        }



        // Seed ExistenceCase
        if (!context.ExistanceCases.Any())
        {
            context.ExistanceCases.AddRange(
                new ExistaceCase { Code = 1, Name = "قائم بالعمل" },
                new ExistaceCase { Code = 2, Name = "أجازة" },
                new ExistaceCase { Code = 3, Name = "منتدب للخارج" },
                new ExistaceCase { Code = 4, Name = "منتدب من الخارج" }
            );
            context.SaveChanges();

        }


        // Seed Governrates
        if (!context.Governrates.Any())
        {
            context.Governrates.AddRange(
                new Governrate { Code = 1, Name = "أسوان" },
                new Governrate { Code = 2, Name = "أسيوط" },
                new Governrate { Code = 3, Name = "الأسكندرية" },
                new Governrate { Code = 4, Name = "المنوفية" },
                new Governrate { Code = 5, Name = "البحر الأحمر" },
                new Governrate { Code = 6, Name = "البحيرة" },
                new Governrate { Code = 7, Name = "الجيزة" },
                new Governrate { Code = 8, Name = "الدقهلية" },
                new Governrate { Code = 9, Name = "السويس" },
                new Governrate { Code = 10, Name = "الشرقية" },
                new Governrate { Code = 11, Name = "الغربية" },
                new Governrate { Code = 12, Name = "الفيوم" },
                new Governrate { Code = 13, Name = "القاهرة" },
                new Governrate { Code = 14, Name = "القليوبية" },
                new Governrate { Code = 15, Name = "المنيا" },
                new Governrate { Code = 16, Name = "الوادى الجديد" },
                new Governrate { Code = 17, Name = "بنى سويف" },
                new Governrate { Code = 18, Name = "بور سعيد" },
                new Governrate { Code = 19, Name = "جنوب سيناء" },
                new Governrate { Code = 20, Name = "دمياط" },
                new Governrate { Code = 21, Name = "سوهاج" },
                new Governrate { Code = 22, Name = "قنا" },
                new Governrate { Code = 23, Name = "كفر الشيخ" },
                new Governrate { Code = 24, Name = "مرسى مطروح" },
                new Governrate { Code = 25, Name = "شمال سيناء" },
                new Governrate { Code = 26, Name = "الاقصر" },
                new Governrate { Code = 27, Name = "الاسماعلية" }
            );
            context.SaveChanges();

        }





        if (!context.Sectors.Any())
        {
            context.Sectors.AddRange(
                new Sector { Code = 1, Name = "رئيس الجامعة", Status = 1 },
                new Sector { Code = 2, Name = "نائب رئيس الجامعة لشئون التعليم والطلاب" },
                new Sector { Code = 3, Name = "نائب رئيس الجامعة للدراسات العليا والبحوث" },
                new Sector { Code = 4, Name = "نائب رئيس الجامعة لشئون خدمة المجتمع وتنمية البيئة" },
                new Sector { Code = 5, Name = "أمين عام الجامعة" }
            );
            context.SaveChanges();


        }

        if (!context.generalAds.Any())
        {
            context.generalAds.AddRange(
                new GeneralAd
                {
                    SectorID = 1,
                    Code = 1,
                    Name = "رئيس الادارة المركزية لمكتب رئيس الجامعة",
                    Level = true,
                    SpecialLevel = false
                },
                new GeneralAd
                {
                    SectorID = 1,
                    Code = 2,
                    Name = "الإدارة العامة لنظم المعلومات والتحول الرقمى بالجا",
                    Level = true,
                    SpecialLevel = false
                },
                new GeneralAd
                {
                    SectorID = 1,
                    Code = 3,
                    Name = "الإدارة العامة للشئون القانونية",
                    Level = true,
                    SpecialLevel = false
                },
                new GeneralAd
                {
                    SectorID = 2,
                    Code = 1,
                    Name = "الإدارة العامة لشئون التعليم",
                    Level = true,
                    SpecialLevel = false
                },
                new GeneralAd
                {
                    SectorID = 2,
                    Code = 2,
                    Name = "الإدارة العامة للشئون الطبية",
                    Level = true,
                    SpecialLevel = false
                },
                new GeneralAd
                {
                    SectorID = 3,
                    Code = 1,
                    Name = "مكتب النائب للدراسات العليا والبحوث",
                    Level = false,
                    SpecialLevel = false
                },
                new GeneralAd
                {
                    SectorID = 3,
                    Code = 2,
                    Name = "الإدارة العامة للدراسات العليا والبحوث",
                    Level = true,
                    SpecialLevel = false
                },
                new GeneralAd
                {
                    SectorID = 4,
                    Code = 1,
                    Name = "مكتب النائب لشئون خدمه المجتمع وتنمية البيئه",
                    Level = false,
                    SpecialLevel = false
                },
                new GeneralAd
                {
                    SectorID = 4,
                    Code = 2,
                    Name = "الإدارة العامة للمشروعات البيئة",
                    Level = true,
                    SpecialLevel = false
                },
                new GeneralAd
                {
                    SectorID = 4,
                    Code = 10,
                    Name = "مطبعه الجامعه",
                    Level = false,
                    SpecialLevel = false,
                    Status = 0
                },
                new GeneralAd
                { SectorID = 5, Code = 1, Name = "الأمناء المساعدون", Level = false, SpecialLevel = false },
                new GeneralAd
                {
                    SectorID = 5,
                    Code = 8,
                    Name = "الإدارة العامة لشئون أعضاء هيئة التدريس",
                    Level = true,
                    SpecialLevel = false
                },
                new GeneralAd
                {
                    SectorID = 5,
                    Code = 9,
                    Name = "الإدارة العامة لشئون العاملين  كادر عام",
                    Level = true,
                    SpecialLevel = false
                },
                new GeneralAd
                {
                    SectorID = 5,
                    Code = 10,
                    Name = "الإدارة العامة للشئون الإدارية",
                    Level = true,
                    SpecialLevel = false
                }
            );
            context.SaveChanges();

        }











        if (!context.subAds.Any())
        {
            context.subAds.AddRange(
                new SubAd
                {
                    GeneralAdId = 1,
                    Code = 1,
                    Name = "ادارة خدمة المواطنين",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd { GeneralAdId = 1, Code = 2, Name = "ادارة  المتابعة", Level = true, SpecialLevel = false },
                new SubAd
                {
                    GeneralAdId = 1,
                    Code = 3,
                    Name = "ادارة  سكرتارية رئيس الجامعة",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 1,
                    Code = 4,
                    Name = "ادارة المكتب الفنى",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 1,
                    Code = 5,
                    Name = "ادارة تطوير وتقويم الاداء لضمان الجوده",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd { GeneralAdId = 1, Code = 6, Name = "اداره الترجمه", Level = true, SpecialLevel = false },
                new SubAd
                {
                    GeneralAdId = 1,
                    Code = 7,
                    Name = "مكتب الاتصال بالقاهره",
                    Level = false,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 1,
                    Code = 8,
                    Name = "مكتب مستشارى رئيس الجامعة",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 1,
                    Code = 9,
                    Name = "وحدة إدارة المشروعات",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 1,
                    Code = 10,
                    Name = "مكتب العلاقات الدولية",
                    Level = false,
                    SpecialLevel = false
                },
                new SubAd { GeneralAdId = 2, Code = 1, Name = "إدارة الحاسبات", Level = true, SpecialLevel = false },
                new SubAd
                {
                    GeneralAdId = 2,
                    Code = 2,
                    Name = "إدارة دعم اتخاذ القرار",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 2,
                    Code = 3,
                    Name = "إدارة الإحصاء والمعلومات والإحصاءات المركزية",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 2,
                    Code = 4,
                    Name = "إدارة التوثيق والمكتبه",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd { GeneralAdId = 2, Code = 5, Name = "إدارة النشر", Level = true, SpecialLevel = false },
                new SubAd { GeneralAdId = 2, Code = 6, Name = "إدارة البرمجة", Level = true, SpecialLevel = false },
                new SubAd
                {
                    GeneralAdId = 2,
                    Code = 7,
                    Name = "إدارة تكنولوجيا التعليم",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd { GeneralAdId = 2, Code = 8, Name = "شبكة الربط", Level = true, SpecialLevel = false },
                new SubAd { GeneralAdId = 2, Code = 9, Name = "ادارة التدريب", Level = true, SpecialLevel = false },
                new SubAd
                {
                    GeneralAdId = 2,
                    Code = 10,
                    Name = "مشروعات البوابه الالكترونيه",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd { GeneralAdId = 2, Code = 11, Name = "وحدة MIS", Level = false, SpecialLevel = false },
                new SubAd
                {
                    GeneralAdId = 3,
                    Code = 1,
                    Name = "إدارة القضايا والتنفيذ والحجز الإداري",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 3,
                    Code = 2,
                    Name = "إدارة التحقيقات (تكراري)",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd { GeneralAdId = 3, Code = 3, Name = "إدارة التحقيقات", Level = true, SpecialLevel = false },
                new SubAd
                {
                    GeneralAdId = 3,
                    Code = 4,
                    Name = "إدارة الشكاوي والتظلمات",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd { GeneralAdId = 3, Code = 5, Name = "إدارة الفتاوى", Level = true, SpecialLevel = false },
                new SubAd
                {
                    GeneralAdId = 3,
                    Code = 6,
                    Name = "إدارة العقود واللوائح",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 3,
                    Code = 7,
                    Name = "مدير ادارة قانونية ( تكراراى )",
                    Level = false,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 1,
                    Code = 8,
                    Name = "إدارة شئون التسجيل",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 1,
                    Code = 9,
                    Name = "إدارة شئون الدراسة",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 1,
                    Code = 10,
                    Name = "إدارة شئون الامتحانات",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 1,
                    Code = 11,
                    Name = "إدارة شئون الخريجين",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd { GeneralAdId = 2, Code = 12, Name = "مستشفى الطلبة", Level = true, SpecialLevel = false },
                new SubAd
                {
                    GeneralAdId = 2,
                    Code = 13,
                    Name = "إدارة شئون الوحدات العلاجية",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 2,
                    Code = 14,
                    Name = "إدارة شئون الطب الوقائي",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 2,
                    Code = 15,
                    Name = "إدارة التموين الطبي والصيدليات",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 2,
                    Code = 16,
                    Name = "إدارة الشئون المالية والادارية",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 2,
                    Code = 17,
                    Name = "الادارة الطبية بفرع الجامعة بمدينه السادات",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd { GeneralAdId = 8, Code = 18, Name = "إدارة التعيينات", Level = true, SpecialLevel = false },
                new SubAd
                {
                    GeneralAdId = 8,
                    Code = 19,
                    Name = "إدارة الانتدابات والإعارات والأجازات",
                    Level = true,
                    SpecialLevel = false
                },
                new SubAd
                {
                    GeneralAdId = 8,
                    Code = 20,
                    Name = "إدارة المعاشات ووثائق الخدمة",
                    Level = true,
                    SpecialLevel = false
                }
            );


            context.SaveChanges();
        }

        if (!context.departments.Any())
        {
            context.departments.AddRange(
                new Department { Code = 1, SubAdID = 2, Name = "قسم ترتيب الوظائف" },
                new Department { Code = 2, SubAdID = 2, Name = "قسم تخطيط القوى العاملة" },
                new Department { Code = 1, SubAdID = 2, Name = "بالسادات", Status = 1 },
                new Department { Code = 1, SubAdID = 1, Name = "مركز استشارات وتكنولوجيا البيئية", Status = 1 },
                new Department { Code = 1, SubAdID = 2, Name = "المعامل" },
                new Department { Code = 2, SubAdID = 2, Name = "الورش" },
                new Department { Code = 1, SubAdID = 4, Name = "قسم الشئون العلاجية" },
                new Department { Code = 2, SubAdID = 4, Name = "قسم الشئون المالية" },
                new Department { Code = 3, SubAdID = 4, Name = "قسم الشئون الإدارية" },
                new Department { Code = 4, SubAdID = 4, Name = "قسم الصيدلة" },
                new Department { Code = 1, SubAdID = 5, Name = "قسم الشئون العلاجية" },
                new Department { Code = 2, SubAdID = 5, Name = "قسم الشئون المالية" },
                new Department { Code = 3, SubAdID = 5, Name = "قسم الشئون الإدارية" },
                new Department { Code = 4, SubAdID = 5, Name = "قسم الصيدلة" },
                new Department { Code = 1, SubAdID = 6, Name = "قسم الشئون العلاجية" },
                new Department { Code = 2, SubAdID = 6, Name = "قسم الشئون المالية" },
                new Department { Code = 3, SubAdID = 6, Name = "قسم الشئون الإدارية" },
                new Department { Code = 4, SubAdID = 6, Name = "قسم الصيدلة" },
                new Department { Code = 1, SubAdID = 7, Name = "قسم الشئون العلاجية" },
                new Department { Code = 2, SubAdID = 7, Name = "قسم الشئون المالية" },
                new Department { Code = 3, SubAdID = 7, Name = "قسم الشئون الإدارية" },
                new Department { Code = 4, SubAdID = 7, Name = "قسم الصيدلة" },
                new Department { Code = 1, SubAdID = 8, Name = "قسم شئون الأطباء" },
                new Department { Code = 2, SubAdID = 8, Name = "قسم شئون التمريض" },
                new Department { Code = 1, SubAdID = 12, Name = "قسم الشئون المالية" },
                new Department { Code = 2, SubAdID = 12, Name = "قسم الشئون الإدارية" }
            );
            context.SaveChanges();


        }


        // Seed HealthState
        if (!context.HealthStates.Any())
        {
            context.HealthStates.AddRange(
                new HealthState { Code = 1, Name = "سليم" },
                new HealthState { Code = 2, Name = "معاق" },
                new HealthState { Code = 3, Name = "يعول معاق" }
            );
            context.SaveChanges();

        }


        if (!context.NonExistanceTypes.Any())
        {
            context.NonExistanceTypes.AddRange(
                new NonExistanceType { Code = 1, Name = "الوفاة" },
                new NonExistanceType { Code = 2, Name = "الإستقالة" },
                new NonExistanceType { Code = 3, Name = "المعاش" },
                new NonExistanceType { Code = 4, Name = "إنهاء الخدمة" },
                new NonExistanceType { Code = 5, Name = "معاش مبكر" },
                new NonExistanceType { Code = 6, Name = "الانقطاع" },
                new NonExistanceType { Code = 7, Name = "نقل خارج الجامعة" },
                new NonExistanceType { Code = 8, Name = "عجز جزئى مرضى مستديم" }
            );
            context.SaveChanges();

        }




        if (!context.JobGroups.Any())
        {
            context.JobGroups.AddRange(
                new JobGroup { Code = 1, Name = "إدارة" },
                new JobGroup { Code = 2, Name = "فنية" },
                new JobGroup { Code = 3, Name = "مالية" }
            );
            context.SaveChanges();

        }

        //context.SaveChanges();

        if (!context.JobSubGroups.Any())
        {
            context.JobSubGroups.AddRange(
                new JobSubGroup { Code = 1, Name = "إدارة عليا" },
                new JobSubGroup { Code = 2, Name = "التنمية الإدارية" },
                new JobSubGroup { Code = 3, Name = "الاستشارية" },
                new JobSubGroup { Code = 4, Name = "الخدمات المعاونة" },
                new JobSubGroup { Code = 5, Name = "إدارة وسطى" },
                new JobSubGroup { Code = 6, Name = "هندسة" }
            );
            context.SaveChanges();

        }

        ;

        //context.SaveChanges();

        if (!context.JobNames.Any())
        {
            context.JobNames.AddRange(
                new JobName
                {
                    Code = 1,
                    Name = "مدير عام",
                    JobMission = "الإشراف العام على المؤسسة"
                },
                new JobName
                {
                    Code = 2,
                    Name = "مهندس",
                    JobMission = "تنفيذ المشاريع الهندسية"
                }
            );
            context.SaveChanges();

        }









        // SocialState
        if (!context.SocialStates.Any())
        {
            context.SocialStates.AddRange(
                new SocialState { Code = 1, Name = "أعزب" },
                new SocialState { Code = 2, Name = "متزوج" },
                new SocialState { Code = 3, Name = "متزوج ويعول" },
                new SocialState { Code = 4, Name = "مطلق" },
                new SocialState { Code = 5, Name = "أرمل" }
            );
            context.SaveChanges();

        }





        #endregion











        // Seed Employees
        if (!context.Employees.Any())
        {
            context.Employees.AddRange(
                new Employee
                {
                    NationalId = "26411201700123",
                    FileId = "EMP001",
                    Name = "Ahmed Ali",
                    FacultyId = 2, // كلية الهندسة بشبين الكوم
                    JobGroupId = 1,
                    JobSubGroupId = 1,
                    JobNameId = 1,
                    SubAdId = 1,
                    DepartmentId = 1,
                    IsExist = true,
                    ExistaceCaseId = 1, // قائم بالعمل
                    NonExistanceTypeId = null,
                    TaminNo = 123456,
                    AppointDate = new DateOnly(2010, 5, 15),
                    WorkEndDate = new DateOnly(2030, 5, 15),
                    Gender = Gender.Male,
                    BirthDate = new DateOnly(1985, 6, 20),
                    BirthPlace = "Cairo",
                    Tel = "02-12345678",
                    Mobile = "01012345678",
                    WorkDate = new DateOnly(2010, 6, 1),
                    HealthStateId = 1, // سليم
                    AppointDN = 101,
                    ExperanceDate = new DateOnly(2010, 6, 1),
                    ExperanceDN = 5,
                    Serial = 1001,
                    SocialStateId = 1,
                    GovernrateId = 13, // القاهرة
                    Address = "123 Street, Cairo",
                    WorkDateFt = new DateOnly(2010, 7, 1),
                    ReAppointDate = null,
                    CombinationDate = null,
                    FJobDate = new DateOnly(2010, 6, 1),
                    City = "Nasr City",
                    Village = "N/A",
                    YearEmp = "2010",
                    LastBalance = 10000m,
                    CurrentBalance = 15000m,
                    LastYearBalance = 5000m,
                    SickBalance = 20,
                    ReservedDays = 5,
                    ReservedMonths = 1,
                    ReservedYears = 0,
                    DisabilityType = "",
                    DisabilityFamilyMember = "",
                    DegreeDate = new DateOnly(2009, 7, 1)
                },
                new Employee
                {
                    NationalId = "26411281702333",
                    FileId = "EMP002",
                    Name = "Sara Mohamed",
                    FacultyId = 5, // كلية العلوم
                    JobGroupId = 2,
                    JobSubGroupId = 1,
                    JobNameId = 2,
                    SubAdId = 2,
                    DepartmentId = 2,
                    IsExist = true,
                    ExistaceCaseId = 2, // أجازة
                    NonExistanceTypeId = null,
                    TaminNo = 654321,
                    AppointDate = new DateOnly(2012, 8, 10),
                    WorkEndDate = new DateOnly(2032, 8, 10),
                    Gender = Gender.Female,
                    BirthDate = new DateOnly(1990, 4, 12),
                    BirthPlace = "Alexandria",
                    Tel = "03-98765432",
                    Mobile = "01198765432",
                    WorkDate = new DateOnly(2012, 9, 1),
                    HealthStateId = 2, // معاق
                    AppointDN = 102,
                    ExperanceDate = new DateOnly(2012, 9, 1),
                    ExperanceDN = 7,
                    Serial = 1002,
                    SocialStateId = 2,
                    GovernrateId = 3, // الإسكندرية
                    Address = "45 Main St, Alexandria",
                    WorkDateFt = new DateOnly(2012, 10, 1),
                    ReAppointDate = null,
                    CombinationDate = null,
                    FJobDate = new DateOnly(2012, 9, 1),
                    City = "Smouha",
                    Village = "N/A",
                    YearEmp = "2012",
                    LastBalance = 12000m,
                    CurrentBalance = 18000m,
                    LastYearBalance = 6000m,
                    SickBalance = 15,
                    ReservedDays = 3,
                    ReservedMonths = 2,
                    ReservedYears = 0,
                    DisabilityType = "Hearing Impairment",
                    DisabilityFamilyMember = "Brother",
                    DegreeDate = new DateOnly(2011, 7, 1)
                }
            );

            context.SaveChanges();
        }









        #region Lower







        // Seed AllowanceTypes
        if (!context.AllowanceTypes.Any())
        {
            context.AllowanceTypes.AddRange(
                new AllowanceType { Code = 1, Name = "الأقدمية" },
                new AllowanceType { Code = 2, Name = "علمية" },
                new AllowanceType { Code = 4, Name = "حافز ترقيه درجة أولى" },
                new AllowanceType { Code = 5, Name = "علاوة دورية" }
            );

            context.SaveChanges();

        }

        // Seed FinancialZemaTypes
        if (!context.FinicialZemaTypes.Any())
        {
            context.FinicialZemaTypes.AddRange(
                new FinicialZemaType { Code = 1, Name = "خضوع" },
                new FinicialZemaType { Code = 2, Name = "دورى" },
                new FinicialZemaType { Code = 3, Name = "زوال الصفة" }
            );

            context.SaveChanges();

        }




        // Seed dummy data for YearReportLaw
        if (!context.YearReportLaws.Any())
        {
            context.YearReportLaws.AddRange(
                new YearReportLaw
                {
                    Code = 100,
                    EmpId = "26411201700123",
                    Period = "2024",
                    Grade = "A",
                    Geha = "Department 1"
                },
                new YearReportLaw
                {
                    Code = 101,
                    EmpId = "26411201700123",
                    Period = "2024",
                    Grade = "B",
                    Geha = "Department 2"
                }
            );

            context.SaveChanges();

        }

        // Seed dummy data for FinicialZema
        if (!context.FinicialZemas.Any())
        {
            context.FinicialZemas.AddRange(
                new FinicialZema
                {
                    Code = 200,
                    EmpId = "26411201700123",
                    RequiredZemaNo = 3,
                    FinicialZemaTypeId = 1,
                    LastDecisionDate = new DateOnly(2024, 1, 1),
                    NewSubmissionDate = new DateOnly(2024, 1, 1),
                    Submitted = true,
                    SubmissionDate = new DateOnly(2024, 1, 1),
                    GraftGoingDate = new DateOnly(2024, 1, 1),
                    GraftComingDate = new DateOnly(2024, 1, 1),
                    Notes = "Dummy note 1"
                },
                new FinicialZema
                {
                    Code = 201,
                    EmpId = "26411201700123",
                    RequiredZemaNo = 2,
                    FinicialZemaTypeId = 2,
                    LastDecisionDate = new DateOnly(2024, 1, 1),
                    NewSubmissionDate = new DateOnly(2024, 1, 1),
                    Submitted = false,
                    SubmissionDate = null,
                    GraftGoingDate = new DateOnly(2024, 1, 1),
                    GraftComingDate = new DateOnly(2024, 1, 1),
                    Notes = "Dummy note 2"
                }
            );
            context.SaveChanges();

        }

        // Seed dummy data for Allowance
        if (!context.Allowances.Any())
        {
            context.Allowances.AddRange(
                new Allowance
                {
                    Code = 300,
                    EmpId = "26411201700123",
                    AllowanceTypeId = 1,
                    DecisionNo = 101,
                    DecisionDate = new DateOnly(2024, 1, 1)
                },
                new Allowance
                {
                    Code = 301,
                    EmpId = "26411201700123",
                    AllowanceTypeId = 2,
                    DecisionNo = 102,
                    DecisionDate = new DateOnly(2024, 1, 1)
                }
            );
            context.SaveChanges();
        }




        if (!context.EducationalLevels.Any())
        {
            context.EducationalLevels.AddRange(
                new EducationalLevel { Name = "دون المتوسط", SortId = 8 },
                new EducationalLevel { Name = "مؤهل متوسط", SortId = 7 },
                new EducationalLevel { Name = "فوق المتوسط", SortId = 6 },
                new EducationalLevel { Name = "ليسانس", SortId = 5 },
                new EducationalLevel { Name = "بكالوريوس", SortId = 5 },
                new EducationalLevel { Name = "دبلوم عالي", SortId = 4 },
                new EducationalLevel { Name = "ماجستير", SortId = 2 },
                new EducationalLevel { Name = "دكتوراه", SortId = 1 },
                new EducationalLevel { Name = "شهادة معادلة القوات المسلحة", SortId = 7 },
                new EducationalLevel { Name = "تمهيدى ماجستير", SortId = 3 },
                new EducationalLevel { Name = "راسب الاعدادية", SortId = 9 },
                new EducationalLevel { Name = "راسب الابتدائية", SortId = 10 },
                new EducationalLevel { Name = "دبلوم العلوم الاقتصادية والمالية", SortId = 4 },
                new EducationalLevel { Name = "بدون مؤهل", SortId = 11 },
                new EducationalLevel { Name = "زميل" }, // No SortId provided
                new EducationalLevel { Name = "استشارى" }, // No SortId provided
                new EducationalLevel { Name = "استشارى مساعد" }, // No SortId provided
                new EducationalLevel { Name = "مؤهل عالى", SortId = 5 },
                new EducationalLevel { Name = "الزمالة المصرية" }, // No SortId provided
                new EducationalLevel { Name = "دبلومتين تعادل درجة الماجستير" }, // No SortId provided
                new EducationalLevel { Name = "امى يقرا ويكتب" } // No SortId provided
            );
            context.SaveChanges();

        }


        if (!context.Certificates.Any())
        {
            context.Certificates.AddRange(
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس الخدمة الاجتماعية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس التربية الرياضية" },
                new Certificate { educationalLevelID = 4, Name = "ليسانس أداب" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس التربية النوعية" },
                new Certificate { educationalLevelID = 4, Name = "ليسانس لغة عربية" },
                new Certificate { educationalLevelID = 4, Name = "الاجازة العليا(ليسانس دراسات انسانية)" },
                new Certificate { educationalLevelID = 3, Name = "معهد فنى صناعى" },
                new Certificate { educationalLevelID = 4, Name = "ليسانس حقوق " },
                new Certificate { educationalLevelID = 3, Name = "معهد فنى تجارى" },
                new Certificate { educationalLevelID = 3, Name = "معهد معلمين ازهرى" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم المعاهد الفنيه الصناعية" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم  المدارس الثانوية  الفنية التجارية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس التجارة" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم المدارس الثانوية الفنية الصناعية" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم المدارس الثانوية الفنية الزراعية" },
                new Certificate { educationalLevelID = 2, Name = "شهادة  الثانوية ازهرية" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم معلمين ازهرى" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم المدارس الثانوية الفنية للادارة والخدمات" },
                new Certificate { educationalLevelID = 2, Name = "شهادة متوسطة عسكرية" },
                new Certificate { educationalLevelID = 2, Name = "ثانوية ازهرية " },
                new Certificate { educationalLevelID = 2, Name = "دبلوم تجارة خمس سنوات" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم خدمة اجتماعية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس الاقتصاد المنزلى" },
                new Certificate { educationalLevelID = 4, Name = "ليسانس اداب وتربية " },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس العلوم" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس الهندسة" },
                new Certificate
                { educationalLevelID = 5, Name = "بكالوريوس المعهد العالى للدراسات التعاونيه والادارية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس الزراعة" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس التربية الفكرية" },
                new Certificate { educationalLevelID = 4, Name = "ليسانس دراسات اسلامية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس السياحة والفنادق" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس الدراسات التعاونية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس التعاون الزراعى" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم الدرسات العليا" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى العلوم" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراة حاسب" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس الحاسبات والمعلومات" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم المعاهد الفنيه الصحيه" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم المعهد الفنى للتمريض" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس الطب والجراحة" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس العلاج الطبيعي" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس التمريض" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس طب وجراحة الفم والأسنان" },
                new Certificate { educationalLevelID = 4, Name = "لبسانس التربية" },
                new Certificate { educationalLevelID = 7, Name = "الماجستير فى التربية" },
                new Certificate { educationalLevelID = 7, Name = "ماجستيرفى طب وجراحة العين" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير طب المناطق الحارة وصحتها" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس معهد الكفاية الانتاجية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس التجارة بنظام التعليم المفتوح  شعبة" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم المدارس الثانويه للتمريض" },
                new Certificate { educationalLevelID = 7, Name = "الماجستير فى علوم التمريض" },
                new Certificate
                {
                    educationalLevelID = 5,
                    Name = "الاجازة العالية (بكالوريوس فى الخدمة الاجتماعية وتنميه المجتمع)"
                },
                new Certificate { educationalLevelID = 2, Name = "شهادة اتمام الدراسة الثانوية العامة" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فى العلوم الزراعية" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى الاقتصاد المنزلى" },
                new Certificate { educationalLevelID = 8, Name = "درجة الدكتوراه فى الاقتصاد المنزلى" },
                new Certificate { educationalLevelID = 1, Name = "راسب ابتدائية" },
                new Certificate { educationalLevelID = 1, Name = "محو الامية" },
                new Certificate { educationalLevelID = 1, Name = "الابتدائية" },
                new Certificate { educationalLevelID = 1, Name = "اتمام المرحلة الاولى من التعليم الاساسى" },
                new Certificate { educationalLevelID = 1, Name = "راسب اعدادية" },
                new Certificate { educationalLevelID = 1, Name = "الشهادة الاعدادية الازهرية" },
                new Certificate { educationalLevelID = 1, Name = "راسب ثانوية" },
                new Certificate { educationalLevelID = 1, Name = "مصدقة باتمام مدة الالزام" },
                new Certificate { educationalLevelID = 1, Name = "نهاية الحلقة الاولى من التعليم الاساسى" },
                new Certificate { educationalLevelID = 1, Name = "شهادة الاعدادية العامة" },
                new Certificate { educationalLevelID = 5, Name = "الاجازة العالية (بكالوريوس التجارة)" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم اسعاف" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس علوم الحاسب الالى" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس الهندسة الالكترونية" },
                new Certificate { educationalLevelID = 4, Name = "الاجازة العليا (الليسانس)" },
                new Certificate { educationalLevelID = 6, Name = "الدبلوم العام فى التربية" },
                new Certificate { educationalLevelID = 6, Name = "الدبلوم الخاص فى التربية" },
                new Certificate { educationalLevelID = 4, Name = "الاجازة العالية ( الليسانس ) اللغة العربية" },
                new Certificate { educationalLevelID = 1, Name = "مقيد بالصف السادس الابتدائى" },
                new Certificate { educationalLevelID = 1, Name = "اتمام الدراسة بمرحلة التعليم الاساسى" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس علوم و تربية" },
                new Certificate { educationalLevelID = 4, Name = "ليسانس الالسن" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير الالسن" },
                new Certificate { educationalLevelID = 5, Name = "الاجازة العالية ( بكالوريوس فى العلوم الزراعية )" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس المعهد العالى الصناعى" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى الطب الشرعى والسموم" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى العلوم الطبية الاساسية ( الباثولوجيا )" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير الامراض الباطنه" },
                new Certificate { educationalLevelID = 5, Name = "شهادة إتمام التأهيل التربوى فى المجال التجارى" },
                new Certificate { educationalLevelID = 7, Name = "ماجستيرمكتبات" },
                new Certificate { educationalLevelID = 1, Name = "مقيد بالصف الخامس" },
                new Certificate { educationalLevelID = 2, Name = "شهادة الثانوية العامة" },
                new Certificate { educationalLevelID = 6, Name = "الدبلومة المهنية شعبة حضانه ورياض اطفال" },
                new Certificate { educationalLevelID = 7, Name = "الماجستير فى الاشعة التشخيصية" },
                new Certificate { educationalLevelID = 7, Name = "الماجستير فى الهندسة" },
                new Certificate { educationalLevelID = 7, Name = "الماجستير فى الاداب" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى العلوم التجارية" },
                new Certificate { educationalLevelID = 7, Name = "الماجستير الامراض الصدرية والتدرن" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم الدراسات العليا فى الجراحة العامة" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى التربية الرياضية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فى الفنون الجميلة" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى طب الاطفال" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فى العلوم الصيدلية" },
                new Certificate { educationalLevelID = 1, Name = "راسب الاعدادية الازهرية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فى الاعلام" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى طب المناطق الحارة وصحتها" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس تعاون تجارى" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فى العلوم الطبية البيطرية" },
                new Certificate { educationalLevelID = 5, Name = "الاجازة العالية ( بكالوريوس الهندسة)" },
                new Certificate { educationalLevelID = 2, Name = "شهادة اتمام الدراسة للصيارفة والمحصلين" },
                new Certificate { educationalLevelID = 8, Name = "درجة الدكتوراه فى العلوم الطبية الاساسيه" },
                new Certificate { educationalLevelID = 8, Name = "درجة الدكتوراه فى الاشعه التشخيصيه" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فى المعاملات المالية والتجارية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس الاقتصاد والعلوم السياسية" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم الدراسات الاسلامية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس اللغة العربية" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم معهد الاداره والسكرتارية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فنون تطبيقية" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم مساعده مولودات" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى التخدير" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم الدراسات العليا فى طب المناطق الحارة وصحتها" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم الامراض الباطنه" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم القانون العام" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم القانون الخاص" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم الادارة المحليه" },
                new Certificate { educationalLevelID = 5, Name = "بكالويوس العلوم العسكرية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس العلوم التعاونيه الزراعية" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم تمريض" },
                new Certificate { educationalLevelID = 6, Name = "شهادة الدبلوم المهنى فى التربية" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم طب الفم وامراض اللثة وطرق التشخيص والاشعة" },
                new Certificate { educationalLevelID = 7, Name = "الماجستير فى دراسات الطفوله" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراه الفلسفه" },
                new Certificate
                {
                    educationalLevelID = 6,
                    Name = "دبلوم طب الفم والاسنان فرع العلاج التحفظى للاسنان وعلاج الجذور والتيجان والجسور"
                },
                new Certificate { educationalLevelID = 8, Name = "درجة دكتور فى الطب" },
                new Certificate
                { educationalLevelID = 7, Name = "دبلوم الترجمة التحريرية والفورية (المعادل للماجستير)" },
                new Certificate { educationalLevelID = 7, Name = "الماجستير فى الجراحة" },
                new Certificate { educationalLevelID = 5, Name = "الاجازة العالية بكالوريوس العلوم" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم الدراسات العليا فى العلوم الجنائية" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم العلوم الاقتصادية المالية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس تكنولوجيا البصريات" },
                new Certificate { educationalLevelID = 2, Name = "شهادة دبلوم التلمذة الصناعية" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم المدارس الثانوية الفنية للبنات" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم الدراسات التعاونيه والادارية" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى العلوم الزراعية" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم المعهد الفنى التجارى" },
                new Certificate { educationalLevelID = 1, Name = "مساعدات الممرضات" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراه الفلسفه فى التربية الرياضية" },
                new Certificate { educationalLevelID = 2, Name = "المعهد الفنى الصحى" },
                new Certificate { educationalLevelID = 2, Name = "الشهادة المعادلة للقوات المسلحة" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى طب الكبد في الاطفال" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم العلاقات العامة" },
                new Certificate { educationalLevelID = 5, Name = "الاجازة العالية ( بكالوريوس الطب والجراحة )" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس العلوم المالية و التجارية" },
                new Certificate
                { educationalLevelID = 5, Name = "الاجازة العالية الليسانس الدراسات الاسلامية والعربية" },
                new Certificate { educationalLevelID = 4, Name = "الاجازة العالية ( الليسانس ) فى الدراسات العربية" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير الامراض الجلدية والتناسلية والذكورة" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير الامراض الجلدية والتناسلية" },
                new Certificate { educationalLevelID = 7, Name = "انهى الدراسة التمهيدية للماجستير" },
                new Certificate
                {
                    educationalLevelID = 5,
                    Name = "بكالوريوس الهندسة نظام الدراسات التكميلية لخريجى الشعبة الهندسية بمعهد الكفاية الانتاجية"
                },
                new Certificate { educationalLevelID = 6, Name = "دبلوم التأهيلى للمكتبات" },
                new Certificate { educationalLevelID = 7, Name = "الماجستير فى العلاج الطبيعى" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى التوليد وامراض النساء" },
                new Certificate { educationalLevelID = 7, Name = "الماجستير فى جراحة العظام" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم اداره الاعمال" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى الباثولوجيا الاكلينيكية" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراه فى البا ثولوجيا الاكلينيكيه والكميائيه" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى التحاليل الطبيه" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى العلوم الطبيه الاساسيه" },
                new Certificate { educationalLevelID = 5, Name = "البكالوريوس فى التربية" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم  معاهد اعداد الفنيين" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى العلوم الصيدليه" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم غذائيات المستشفيات" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراة فى دراسات الطفولة" },
                new Certificate { educationalLevelID = 7, Name = "الماجستير تخصص جراحة الانف والاذن والحنجرة" },
                new Certificate { educationalLevelID = 4, Name = "الاجازة العالية ( الليسانس ) الشريعه والقانون" },
                new Certificate { educationalLevelID = 5, Name = "البكالوريوس فى الهندسة الكهربائية" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى التربية البدنيه والرياضية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس التصوير ( معهد ليوناردو دافنيشى )" },
                new Certificate { educationalLevelID = 8, Name = "درجة دكتور الفلسفة فى الاقتصاد المنزلى" },
                new Certificate { educationalLevelID = 10, Name = "تمهيد ماجستير" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم العلوم الادارية" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم القانون العام" },
                new Certificate { educationalLevelID = 8, Name = "درجة الدكتوراه فى الاداب" },
                new Certificate { educationalLevelID = 5, Name = "الاجازة العالية ( بكالوريوس فى العلوم الصيدلية )" },
                new Certificate
                { educationalLevelID = 5, Name = "الاجازة العالية ( البكالوريوس فى الطب وجراجه الفم والاسنان )" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى العلوم الطبية البيطرية" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى الهندسة الالكترونية" },
                new Certificate { educationalLevelID = 8, Name = "درجه دكتوراه الفلسفه فى الهندسة" },
                new Certificate { educationalLevelID = 2, Name = "شهادة دبلوم  تخصص ادارة الخدمات التمريض" },
                new Certificate { educationalLevelID = 2, Name = "شهادة دبلوم  تخصص تمريض خاص فرع غرف عمليات" },
                new Certificate
                { educationalLevelID = 5, Name = "بكالوريوس فى تكنولوجيا استصلاح واستزراع الاراضى الصحراوية" },
                new Certificate { educationalLevelID = 4, Name = "ليسانس فى الدراسات القانونية العملية" },
                new Certificate { educationalLevelID = 6, Name = "الدبلوم التخصصى فى العلاج الطبيعى" },
                new Certificate { educationalLevelID = 2, Name = "شهادة الاهلية فى التلغراف اللاسلكى" },
                new Certificate { educationalLevelID = 7, Name = "الماحستير فى الدراسات الادبية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس المعاهد العالية الصناعية" },
                new Certificate { educationalLevelID = 5, Name = "شهادة اتمام التأهيل التربوى فى المجال الزراعى" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير الباثولوجيا الاكلينكية والكيميائية" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى العلوم الزراعية" },
                new Certificate { educationalLevelID = 8, Name = "درجة دكتور الفلسفة فى العلوم الزراعية" },
                new Certificate
                { educationalLevelID = 6, Name = "دبلوم التخصصية الاكلينيكية في طب وجراحة الفم والاسنان فرع" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم الاستعاضة فى طب الاسنان" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم علوم الحاسب" },
                new Certificate { educationalLevelID = 5, Name = "المعهد العالى للخدمة الاجتماعية" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى امراض القلب والاوعية الدموية" },
                new Certificate { educationalLevelID = 6, Name = "الدبلوم العام" },
                new Certificate { educationalLevelID = 7, Name = "أنهى الدراسة التمهيدية لماجستير ادارة الاعمال" },
                new Certificate { educationalLevelID = 5, Name = "شهادة أتمام التأهيل التربوى فى المجال الصناعى" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس التربية الرياضية للمعلمات" },
                new Certificate { educationalLevelID = 5, Name = "البكالوريوس فى المكتبات والمعلومات" },
                new Certificate
                {
                    educationalLevelID = 6,
                    Name = "دبلوم طب الفم وامراض اللثه وطرق التشخيص والاشعه ( نظام السنه الواحده )"
                },
                new Certificate { educationalLevelID = 8, Name = "درجة دكتوراة الالسن" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم المعاهد الفنيه للسكرتاريه" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير الامراض العصبية والطب النفسى" },
                new Certificate { educationalLevelID = 7, Name = "ماجسيتر الانف والاذن والحنجرة" },
                new Certificate
                { educationalLevelID = 5, Name = "الاجازة العالية ( البكالوريوس فى التربية الرياضية )" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم المدارس الثانويه الصناعيه خمس سنوات" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فى نظم وتكنولوجيا المعلومات" },
                new Certificate { educationalLevelID = 5, Name = "المعهد العالى للتعاون الزراعى" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم الدراسات المهنية فى الخدمه الاجتماعية" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس العلوم الزراعيه البيئيه" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى اصول الدين" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراة الفلسفة فى المحاسبة" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى العلوم البيئيه" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم الخدمه الاجتماعية المتوسط" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم الخط العربى" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى علوم طب الاسنان الاكلينكية" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم المدارس الثانوية الفنية للمسعفين" },
                new Certificate { educationalLevelID = 8, Name = "دكتواره فى الحقوق" },
                new Certificate { educationalLevelID = 8, Name = "درجة دكتور الفلسفة فى التربية" },
                new Certificate { educationalLevelID = 8, Name = "درجة الدكتوراه فى الفلسفه فى العلوم" },
                new Certificate { educationalLevelID = 6, Name = "الدبلوم فى العلوم الصيدلية" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم كهرباء ( التنمية التكنولوجية )" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس رقابة جودة - جامعة عمالية" },
                new Certificate { educationalLevelID = 4, Name = "ليسانس فى اللغه العربية والعلوم الاسلامية" },
                new Certificate { educationalLevelID = 4, Name = "الاجازة العالية ( الليسانس فى الاداب والتربية )" },
                new Certificate { educationalLevelID = 8, Name = "درجة الدكتواره فى علوم التمريض" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم المعلمين والمعلمات" },
                new Certificate { educationalLevelID = 4, Name = "الاجازة العالية ( الليسانس فى الاداب  )" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم الادارة والحاسب الالى" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم المدارس الثانويه الصناعيه (اعداد مهنى)" },
                new Certificate { educationalLevelID = 4, Name = "الاجازة العالية (الليسانس فى اصول الدين )" },
                new Certificate { educationalLevelID = 6, Name = "الدبلوم العام فى التربيه الرياضيه" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس المعهد العالى للدراسات النوعية" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم معهد العلاقات الصناعيه" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى جراحه الكبد والقنوات المراريه" },
                new Certificate { educationalLevelID = 4, Name = "الليسانس فى الاجتماع" },
                new Certificate { educationalLevelID = 3, Name = "المعهد الفنى للدراسات النوعيه" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم الحاسب الالى" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فى الحاسب الالى" },
                new Certificate
                { educationalLevelID = 5, Name = "الاجازه العاليه ( البكالوريوس ) فى الاقتصاد المنزلى" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس العلاقات الصناعيه" },
                new Certificate { educationalLevelID = 1, Name = "تربيه فكريه" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم تجارة خمس سنوات" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس الهندسة المدنية" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم الدراسات العليا فى الاداب" },
                new Certificate { educationalLevelID = 5, Name = "الاجازة العالية ( بكالوريوس زراعه )" },
                new Certificate { educationalLevelID = 1, Name = "الابتدائيه الازهريه" },
                new Certificate
                {
                    educationalLevelID = 3,
                    Name = "دبلوم الدراسه الفنيه المتقدمه للشئون الفندقيه والخدمات السياحيه نظام الخمس سنوات"
                },
                new Certificate { educationalLevelID = 5, Name = "البكالوريوس فى العلوم التمريضيه" },
                new Certificate { educationalLevelID = 8, Name = "درجة دكتوراه الفلسفة فى التربية النوعية" },
                new Certificate { educationalLevelID = 1, Name = "مقيد بالصف الثالث الابتدائى" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس التربيه الفنيه" },
                new Certificate { educationalLevelID = 5, Name = "البكالوريوس العامة فى العلوم" },
                new Certificate { educationalLevelID = 5, Name = "البكالوريوس فى تكنولوجيا المعلومات" },
                new Certificate { educationalLevelID = 5, Name = "الاجازة العالية ( الليسانس )" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فى التعليم الصناعى" },
                new Certificate { educationalLevelID = 5, Name = "الاجازه العاليه ( البكالوريوس فى العلوم والتربيه )" },
                new Certificate
                { educationalLevelID = 5, Name = "الاجازه العاليه ( البكالوريوس فى الدراسات الانسانيه)" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم صنايع خمس سنوات" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس الهندسه والتكنولوجيا فى الهندسه الكهربيه" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى طب وجراحة الفم والاسنان" },
                new Certificate
                { educationalLevelID = 8, Name = "دكتوراه الفلسفه فى الهندسه الوراثيه والتكنولوجيا الحيويه" },
                new Certificate
                {
                    educationalLevelID = 8,
                    Name = "دكتوارة فى طب الامراض التناسلية العقم وجراحتها ( امراض الذكورة)"
                },
                new Certificate { educationalLevelID = 7, Name = "ماجستير تخصص تمريض صحه الام حديثى الولادة" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير تخصص تمريض صحه المجتمع والبيئه" },
                new Certificate { educationalLevelID = 8, Name = "درجة الدكتوراه فى الدراسات الفندقية" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى الدراسات الفندقية" },
                new Certificate { educationalLevelID = 4, Name = "الليسانس الاثار المصريه" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم المعهد الفنى للتمريض" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فى الصيدلية" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى الكيمياء الطبيه التطبيقيه" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فى رياض الاطفال" },
                new Certificate { educationalLevelID = 6, Name = "الدبلوم المهنى للترجمة التحريرية والتتبعيةوالفورية" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراة الفلسفة فى العلوم الطبية البيطرية" },
                new Certificate { educationalLevelID = 6, Name = "الدبلوم العام التربوى نظام العام الوحد" },
                new Certificate { educationalLevelID = 6, Name = "الدبلوم التطبيقية فى ادارة الجودة الشاملة" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى علوم الحاسب" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس فى الاقتصاد الدولى" },
                new Certificate { educationalLevelID = 8, Name = "دكتور في الجراحة" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم الصيدلة الاكلينيكية" },
                new Certificate { educationalLevelID = 8, Name = "دكتواره في العلاج الطبيعى" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير في العلوم الهندسية" },
                new Certificate { educationalLevelID = 2, Name = "دبلوم معهد الفنادق" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم ادارة اعمال تخصص( مستشفيات)" },
                new Certificate { educationalLevelID = 8, Name = "دكتواره في طب العين وجراحتها" },
                new Certificate { educationalLevelID = 3, Name = "دبلوم فوق المتوسط سنتان دراسيتان -تحاليل بيلوجيه" },
                new Certificate { educationalLevelID = 8, Name = "دكتواره الفلسفة في العلوم الصيدله" },
                new Certificate { educationalLevelID = 4, Name = "ليسانس الدراسات الاسلامية والعربية" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراه في الاعلام" },
                new Certificate { educationalLevelID = 7, Name = "الماجستير في الاعلام" },
                new Certificate { educationalLevelID = 4, Name = "ليسانس الاثار الاسلامية" },
                new Certificate { educationalLevelID = 6, Name = "دبلوم الدراسات العليا في الامراض المتوطنه" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراه في طب الكبد في الاطفال" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير في التربيه النوعيه" },
                new Certificate
                { educationalLevelID = 6, Name = "الدبلوم التدريبى في ادارة المستشفيات والمنشئات الصحية" },
                new Certificate { educationalLevelID = 6, Name = "الدبلوم التدريبى المهنى في مكافحة العدوى" },
                new Certificate { educationalLevelID = 3, Name = "المعهد الفنى الكيماوى" },
                new Certificate
                { educationalLevelID = 2, Name = "دبلوم المدارس الثانويه الفنيه للتمريض  نظام خمس سنوات" },
                new Certificate
                {
                    educationalLevelID = 8,
                    Name = "دكتواره الفلسفة  في العلوم الاساسية في طب الاسنان فرع خواص المواد الحيوية"
                },
                new Certificate { educationalLevelID = 1, Name = "شهادة الاعدادية المهنية" },
                new Certificate { educationalLevelID = 1, Name = "مقيد بالصف الاول بالاعدادية المهنية" },
                new Certificate { educationalLevelID = 5, Name = "البكالوريوس التقنى ( التكنولوجى ) في التمريض" },
                new Certificate { educationalLevelID = 19, Name = "الزمالة المصرية في طب اسنان أسرة" },
                new Certificate { educationalLevelID = 2, Name = "شهادة معهد القراءات" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس في علوم التمريض" },
                new Certificate { educationalLevelID = 5, Name = "البكالوريوس التقنى ( التكنولوجى )" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير التمريض" },
                new Certificate { educationalLevelID = 4, Name = "ليسانس الاداب بنظام التعليم المفتوح" },
                new Certificate { educationalLevelID = 4, Name = "بكالوريوس الاعلام بنظام التعليم المفتوح" },
                new Certificate { educationalLevelID = 4, Name = "ليسانس الحقوق بنظام التعليم المفتوح" },
                new Certificate { educationalLevelID = 4, Name = "بكالوريوس التجارة بنظام التعليم المفتوح" },
                new Certificate { educationalLevelID = 8, Name = "درجة الدكتوراة في السياحة والفنادق" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراة الفلسفة في الدراسات الافريقية" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير في ادارة الجودة" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير فى الروماتيزم والتأهيل والطب الطبيعى" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراه الفلسفه في الحاسبات والمعلومات" },
                new Certificate { educationalLevelID = 7, Name = " درجة الماجستير المهنى" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراه في الامراض العصبيه" },
                new Certificate { educationalLevelID = 7, Name = "ماجستير في الدراسات السياحيه" },
                new Certificate { educationalLevelID = 7, Name = "دبلومتان تعادلان درجة الماجستير" },
                new Certificate { educationalLevelID = 5, Name = "بكالوريوس الاعلام بنظام التعليم المفتوح" },
                new Certificate { educationalLevelID = 8, Name = "دكتوراه في امراض القلب والاوعيه الدمويه" },
                new Certificate
                { educationalLevelID = 19, Name = "الرمالة المصرية اليكتروفسيولوجيا القلب ومنظمات القلب" },
                new Certificate
                { educationalLevelID = 6, Name = "دبلوم الدراسات العاليا دبلوم خاص في التربيه النوعيه ( عامان)" }
            );

            context.SaveChanges();

        }










        // Seed JobGroupKind
        if (!context.AdKinds.Any())
        {
            context.AdKinds.AddRange(
                new AdKind { Code = 1, Name = "معتمد" },
                new AdKind { Code = 2, Name = "مستحدث" }
            );
            context.SaveChanges();

        }

        // Seed JobType
        if (!context.JobTypes.Any())
        {
            context.JobTypes.AddRange(
                new JobType { Code = 1, Name = "عامل" },
                new JobType { Code = 2, Name = "موظف" },
                new JobType { Code = 3, Name = "مدير إدارة" },
                new JobType { Code = 4, Name = "رئيس قسم" },
                new JobType { Code = 5, Name = "مدير عام" },
                new JobType { Code = 6, Name = "أمين عام مساعد" },
                new JobType { Code = 7, Name = "أمين عام" },
                new JobType { Code = 8, Name = "كبير باحثين من شاغلي الوظائف الإدارة العليا" },
                new JobType { Code = 9, Name = "كبير باحثين من غير شاغلي الوظائف الإدارة العليا" },
                new JobType { Code = 10, Name = "امين الكلية" },
                new JobType { Code = 11, Name = "مدير إدارة بالإنابة" },
                new JobType { Code = 12, Name = "قائم بأعمال مدير إدارة" },
                new JobType { Code = 13, Name = "استشارى إدارة عامة" },
                new JobType { Code = 14, Name = "استشارى إدارة مركزية" }
            );
            context.SaveChanges();

        }


        // Seed MandateType
        if (!context.MandateTypes.Any())
        {
            context.MandateTypes.AddRange(
                new MandateType { Code = 1, Name = "إلى وحدات الجامعة المختلفة" },
                new MandateType { Code = 2, Name = "من الخارج" },
                new MandateType { Code = 3, Name = "لخارج الجامعة" },
                new MandateType { Code = 4, Name = "إنتداب على وظيفة" },
                new MandateType { Code = 5, Name = "إنتداب جزئي" },
                new MandateType { Code = 6, Name = "من وحدات الجامعة المختلفة" },
                new MandateType { Code = 7, Name = "إعارة" }
            );
            context.SaveChanges();

        }


        // Seed MilitaryStateType
        if (!context.MilitaryStateTypes.Any())
        {
            context.MilitaryStateTypes.AddRange(
                new MilitaryStateType { Code = 1, Name = "في الخدمة" },
                new MilitaryStateType { Code = 2, Name = "معافى نهائى" },
                new MilitaryStateType { Code = 3, Name = "معافى مؤقت" },
                new MilitaryStateType { Code = 4, Name = "مؤجل" },
                new MilitaryStateType { Code = 5, Name = "أدى الخدمة" },
                new MilitaryStateType { Code = 6, Name = "مؤجل ثلاث سنوات" },
                new MilitaryStateType { Code = 7, Name = "تحدد تجنيده" },
                new MilitaryStateType { Code = 8, Name = "لم يصبه الدور" },
                new MilitaryStateType { Code = 9, Name = "حفظ وظيفة" },
                new MilitaryStateType { Code = 10, Name = "تجاوز سن التجنيد" },
                new MilitaryStateType { Code = 11, Name = "لا يوجد" },
                new MilitaryStateType { Code = 12, Name = "معاف نهائى غير لائق طبيا" },
                new MilitaryStateType { Code = 13, Name = "معاف كونه وحيد ابيه الحى" },
                new MilitaryStateType { Code = 14, Name = "موقف سن 29 سنه" },
                new MilitaryStateType { Code = 15, Name = "موقف سن 30 سنه" },
                new MilitaryStateType { Code = 16, Name = "تحت الطلب" },
                new MilitaryStateType { Code = 17, Name = "(ادى الخدمة (طبقا للمادة 115 من القانون 123 /1981)" },
                new MilitaryStateType { Code = 18, Name = "المذكور تخلف عن التجنيد طبقا لاحكام الماده 49" },
                new MilitaryStateType { Code = 19, Name = "مجند" },
                new MilitaryStateType { Code = 20, Name = "الرفت من الخدمة العسكرية بالقوات المسلحة" },
                new MilitaryStateType { Code = 21, Name = "المذكور وضع تحت الطلب لمدة ثلاث سنوات" },
                new MilitaryStateType { Code = 22, Name = "ادى الخدمة العسكرية مادة 6 من ق 127 لسنة 1980" },
                new MilitaryStateType { Code = 23, Name = "اعفاء مؤقت حتى بلوغ اكبر الاخوة القصر سن 21 سنه" },
                new MilitaryStateType { Code = 24, Name = "اجل الشهادة" },
                new MilitaryStateType { Code = 25, Name = "بلوغ سن الوالد 60 سنه" },
                new MilitaryStateType { Code = 26, Name = "غير مطلوب للتجنيد نهائيا" }
            );
            context.SaveChanges();

        }




        // Seed MilitaryState
        if (!context.MilitaryStates.Any())
        {
            context.MilitaryStates.AddRange(
                new MilitaryState
                {
                    EmpId = "26411201700123",
                    MilitaryStateTypeId = 2,
                    Date = null,
                    Notes = "",
                    Code = 421,
                    CurrentMilitaryState = false
                },
                new MilitaryState
                {
                    EmpId = "26411281702333",
                    MilitaryStateTypeId = 4,
                    Date = null,
                    Notes = "",
                    Code = 1378,
                    CurrentMilitaryState = false
                },
                new MilitaryState
                {
                    EmpId = "26411281702333",
                    MilitaryStateTypeId = 5,
                    Date = null,
                    Notes = "",
                    Code = 2046,
                    CurrentMilitaryState = false
                },
                new MilitaryState
                {
                    EmpId = "26411281702333",
                    MilitaryStateTypeId = 5,
                    Date = null,
                    Notes = "",
                    Code = 2310,
                    CurrentMilitaryState = false
                },
                new MilitaryState
                {
                    EmpId = "26411201700123",
                    MilitaryStateTypeId = 5,
                    Date = null,
                    Notes = "",
                    Code = 3544,
                    CurrentMilitaryState = false
                }
            );
            context.SaveChanges();

        }

        // Seed Lagna
        if (!context.Lagnas.Any())
        {
            context.Lagnas.AddRange(
                new Lagna
                {
                    Code = 2567,
                    EmpId = "26411281702333",
                    Name =
                        "طرح ممارسة محدودة لاصلاح وابرام عقد صيانة لعدد ( 1) مصعد مرضى وعدد (2) مصعد ركاب هيوانداى غير شامل قطع الغيار 0",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = new DateOnly(2024, 1, 1)
                },
                new Lagna
                {
                    Code = 7560,
                    EmpId = "26411201700123",
                    Name = "لجنة شراء احبار لطابعات الكمبيوتر وماكينات التصوير",
                    MemberType = "رئيسا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7561,
                    EmpId = "26411201700123",
                    Name = "لجنة شراء اثاث مكتبى لمعهد الكبد القومى بالامر المباشر",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7562,
                    EmpId = "26411281702333",
                    Name = "لجنة اصلاح جهاز الRedicai 7 الماسيمو الموجود بقسم التخدير بالمعهد",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7563,
                    EmpId = "26411281702333",
                    Name =
                        "لجنة شراء المستلزمات المطلوبة لمشروع دراسة متعددة المراكز عشوائية التوزيع مزدوجة التعمية  مضبطة بعقار مموة وامان عقار يوياداسيتينيت (عقار ABT -494)",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7564,
                    EmpId = "26411281702333",
                    Name =
                        "لجنة تركيب عدد قطوع الومنتيال بجناح وحدة زراعة الكبد بالدور الثالث لعمل مخزن للادوية والمستهلكات الخاصة بمرضى زراعة الكبد",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7565,
                    EmpId = "26411281702333",
                    Name =
                        "لجنة شراء كيماويات لقسم الميكروبيولوجى والمناعة بالمعهد لزوم الابحاث العلمية لطلبة الماجيستر والدكتوراة",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7566,
                    EmpId = "26411201700123",
                    Name = "لجنة الجرد السنوى للمخازن الرئيسية بالمعهد عن العام المالى 2022/2021",
                    MemberType = "رئيسا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7567,
                    EmpId = "26411281702333",
                    Name = "لجنة جرد العهد الفرعية والشخصية للعاملين بالمعهد عن العام المالى 2022",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7568,
                    EmpId = "26411201700123",
                    Name = "لجنة طرح ممارسة محدودة لشراء وصيانة 2 جهاز ارجون + 2جهاز دياثرمى لقسم الجراحة بالمعهد",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7569,
                    EmpId = "26411201700123",
                    Name =
                        "لجنة طرح مناقصة عامة لشراء اجهزة لقسم التخدير بالمعهد وصيانتها بعد انتهاء فترة الضمان المجانية",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7570,
                    EmpId = "26411281702333",
                    Name = "لجنة طرح ممارسة محدودة لشراء وصيانة جهاز موجات فوق الصوتية لقسم الجراحة بالمعهد",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7571,
                    EmpId = "26411281702333",
                    Name =
                        "لجنة طرح مناقصة عامة لشراء الكيماويات والمستلزمات المطلوبة لقسم التحاليل الطبية بمستشفى المعهد 0",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7572,
                    EmpId = "26411201700123",
                    Name = "لجنة طرح ممارسة عامة لشراء مستلزمات تنقية الدم cell saver لقسم التخدير بالمعهد",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7573,
                    EmpId = "26411281702333",
                    Name =
                        "لجنة طرح ممارسة عامة لشراء اثاث طبى واثاث مكتبى ا0د عميد المعهد والسادة الوكلاء والمكاتب الاداية بالمبنى الجديد لمستشفى المعهد",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7574,
                    EmpId = "26411201700123",
                    Name = "لجنة شراء كتب ومراجع علمية لمكتبة المعهد",
                    MemberType = "عضوا",
                    DecisionNo = null,
                    DecisionDate = null
                },
                new Lagna
                {
                    Code = 7575,
                    EmpId = "26411281702333",
                    Name =
                        "تشكيل لجنة وذلك لتكهين وارتجاع خاتم شعار الجمهورية الخاص بالمعهد (  جامعه المنوفية - معهد الكبد القومى ) ولتحريز خاتم شعار الجمهورية",
                    MemberType = "رئيسا",
                    DecisionNo = null,
                    DecisionDate = null
                }
            );
            context.SaveChanges();

        }


        // Seed MandateData
        if (!context.MandateDatas.Any())
        {
            context.MandateDatas.AddRange(
                new MandateData
                {
                    Code = 1,
                    EmpId = "26411201700123",
                    MandateTypeId = 1,
                    FacultyId = 2,
                    IsMandated = true,
                    Geha = "",
                    MandateJob = "وظيفة مؤقتة",
                    DecisionNo = 1234,
                    DecisionDate = new DateOnly(2024, 1, 1),
                    StartDate = new DateOnly(2024, 1, 1),
                    EndDate = new DateOnly(2024, 1, 1)
                },
                new MandateData
                {
                    Code = 2,
                    EmpId = "26411201700123",
                    MandateTypeId = 3,
                    FacultyId = 20,
                    IsMandated = false,
                    Geha = "وزارة الصحة",
                    MandateJob = "",
                    DecisionNo = 5678,
                    DecisionDate = new DateOnly(2024, 1, 1),
                    StartDate = new DateOnly(2024, 1, 1),
                    EndDate = new DateOnly(2024, 1, 1)
                },
                new MandateData
                {
                    Code = 3,
                    EmpId = "26411281702333",
                    MandateTypeId = 2,
                    FacultyId = 6,
                    IsMandated = true,
                    Geha = "",
                    MandateJob = "وظيفة إدارية",
                    DecisionNo = 91011,
                    DecisionDate = new DateOnly(2024, 1, 1),
                    StartDate = new DateOnly(2024, 1, 1),
                    EndDate = null
                },
                new MandateData
                {
                    Code = 4,
                    EmpId = "26411281702333",
                    MandateTypeId = null,
                    FacultyId = null,
                    IsMandated = false,
                    Geha = "",
                    MandateJob = "",
                    DecisionNo = 0,
                    DecisionDate = new DateOnly(2024, 1, 1),
                    StartDate = null,
                    EndDate = null
                }
            );
            context.SaveChanges();

        }






        // PenaltyCase
        if (!context.PenaltyCases.Any())
        {
            context.PenaltyCases.AddRange(
                new PenaltyCase { Code = 1, Name = "سارى" },
                new PenaltyCase { Code = 2, Name = "تم المحو" },
                new PenaltyCase { Code = 3, Name = "براءه" },
                new PenaltyCase { Code = 5, Name = "سحب القرار والاكتفاء بلفت نظر" },
                new PenaltyCase { Code = 7, Name = "تم سحب القرار" }
            );
            context.SaveChanges();
        }

        // PenaltyType
        if (!context.PenaltyTypes.Any())
        {
            context.PenaltyTypes.AddRange(
                new PenaltyType { Code = 1, Name = "لفت نظر" },
                new PenaltyType { Code = 2, Name = "إنذار" },
                new PenaltyType { Code = 3, Name = "خصم يوم" },
                new PenaltyType { Code = 4, Name = "خصم خمسة أيام" },
                new PenaltyType { Code = 5, Name = "اللوم" }
            );
            context.SaveChanges();

        }

        // QualGrade
        if (!context.QualGrades.Any())
        {
            context.QualGrades.AddRange(
                new QualGrade { Code = 1, Grade = 5, Name = "بتقدير ممتاز مع مرتبة الشرف" },
                new QualGrade { Code = 3, Grade = 7, Name = "بتقدير ممتاز" },
                new QualGrade { Code = 2, Grade = 5, Name = "بتقدير جيد جدا مع مرتبة الشرف" },
                new QualGrade { Code = 4, Grade = 5, Name = "بتقدير جيدجداً" },
                new QualGrade { Code = 5, Grade = 2, Name = "بتقدير جيد" },
                new QualGrade { Code = 6, Grade = 7, Name = "بتقدير بنجاح" },
                new QualGrade { Code = 7, Grade = 9, Name = "بتقدير مقبول" }
            );
            context.SaveChanges();

        }


        // VacationType
        if (!context.VacationTypes.Any())
        {
            context.VacationTypes.AddRange(
                new VacationType { Code = 2, Name = "اجازة للعمل بالخارج" },
                new VacationType { Code = 3, Name = "أجازة للعمل بالداخل" },
                new VacationType { Code = 4, Name = "وضع" },
                new VacationType { Code = 5, Name = "رعاية طفل" },
                new VacationType { Code = 1, Name = "مرافق للزوج / الزوجة" }
            );
            context.SaveChanges();

        }

        // YearReportGrade
        if (!context.YearReportGrades.Any())
        {
            context.YearReportGrades.AddRange(
                new YearReportGrade { Code = 1, Name = "ممتاز" },
                new YearReportGrade { Code = 10, Name = "كفء" },
                new YearReportGrade { Code = 11, Name = "فوق متوسط" },
                new YearReportGrade { Code = 12, Name = "متوسط" },
                new YearReportGrade { Code = 13, Name = "ضعيف" },
                new YearReportGrade { Code = 14, Name = "جيد جداً" },
                new YearReportGrade { Code = 15, Name = "جيد" },
                new YearReportGrade { Code = 16, Name = "مقبول" },
                new YearReportGrade { Code = 17, Name = "أجازة" },
                new YearReportGrade { Code = 18, Name = "حكما" }
            );
            context.SaveChanges();

        }

        // YearReports
        if (!context.YearReports.Any())
        {
            context.YearReports.AddRange(
                new YearReport { Code = 19834, Year = 2013, Degree = 96, EmpId = "26411201700123", GradeId = 1, Notes = "" },
                new YearReport { Code = 22342, Year = 2013, Degree = 94, EmpId = "26411201700123", GradeId = 1, Notes = "" },
                new YearReport { Code = 22582, Year = 2011, Degree = 90, EmpId = "26411201700123", GradeId = 1, Notes = "" },
                new YearReport { Code = 22588, Year = 2012, Degree = 91, EmpId = "26411201700123", GradeId = 1, Notes = "" },
                new YearReport { Code = 22589, Year = 2013, Degree = 92, EmpId = "26411201700123", GradeId = 1, Notes = "" },
                new YearReport { Code = 23320, Year = 2013, Degree = 95, EmpId = "26411201700123", GradeId = 1, Notes = "" },
                new YearReport { Code = 25195, Year = 2013, Degree = 96, EmpId = "26411201700123", GradeId = 1, Notes = "" },
                new YearReport { Code = 28624, Year = 2013, Degree = 95, EmpId = "26411201700123", GradeId = 1, Notes = "" },
                new YearReport { Code = 28625, Year = 2014, Degree = 96, EmpId = "26411201700123", GradeId = 1, Notes = "" }

            );

            context.SaveChanges();
        }


        // Penalties
        if (!context.Penalties.Any())
        {
            context.Penalties.AddRange(
                new Penalty { EmpId = "26411201700123", Code = 12284, PenaltyTypeId = 1, PenaltyCaseId = 1 },
                new Penalty { EmpId = "26411201700123", Code = 11271, PenaltyTypeId = 1, PenaltyCaseId = 1 },
                new Penalty { EmpId = "26411201700123", Code = 12701, PenaltyTypeId = 1, PenaltyCaseId = 1 },
                new Penalty { EmpId = "26411201700123", Code = 6167, PenaltyTypeId = 1, PenaltyCaseId = 1 },
                new Penalty { EmpId = "26411201700123", Code = 4422, PenaltyTypeId = 1, PenaltyCaseId = 1 },
                new Penalty { EmpId = "26411201700123", Code = 3699, PenaltyTypeId = 1, PenaltyCaseId = 1 },
                new Penalty { EmpId = "26411201700123", Code = 3699, PenaltyTypeId = 1, PenaltyCaseId = 1 },
                new Penalty { EmpId = "26411201700123", Code = 12000, PenaltyTypeId = 1, PenaltyCaseId = 1 },
                new Penalty { EmpId = "26411201700123", Code = 8985, PenaltyTypeId = 1, PenaltyCaseId = 1 },
                new Penalty { EmpId = "26411201700123", Code = 8397, PenaltyTypeId = 1, PenaltyCaseId = 1 },
                new Penalty { EmpId = "26411201700123", Code = 4229, PenaltyTypeId = 1, PenaltyCaseId = 1 },
                new Penalty { EmpId = "26411201700123", Code = 2431, PenaltyTypeId = 1, PenaltyCaseId = 1 },
                new Penalty { EmpId = "26411201700123", Code = 503, PenaltyTypeId = 1, PenaltyCaseId = 1 }
            );
            context.SaveChanges();

        }



        //Vacations
        if (!context.Vacations.Any())
        {
            context.Vacations.AddRange(
                new Vacation
                {
                    EmpId = "26411281702333",
                    Code = 15760,
                    Ended = true,
                    VacationTypeId = null,
                    StartDate = new DateOnly(1998, 6, 1),
                    EndDate = new DateOnly(1999, 5, 1),
                    ReturnDate = new DateOnly(1998, 11, 25),
                    DecisionNo =591,
                    DecisionDate = new DateOnly(1998, 3, 2),
                    Notes = "اعادة"
                },
                new Vacation
                {
                    EmpId = "26411201700123",
                    Code = 10708,
                    Ended = true,
                    VacationTypeId = null,
                    StartDate = new DateOnly(1994, 5, 5),
                    EndDate = new DateOnly(1995, 3, 31),
                    ReturnDate = new DateOnly(1995, 2, 1),
                    DecisionNo = 820,
                    DecisionDate = new DateOnly(1994, 5, 6),
                    Notes = null
                },
                new Vacation
                {
                    EmpId = "26411201700123",
                    Code = 10707,
                    Ended = true,
                    VacationTypeId = null,
                    StartDate = new DateOnly(1992, 4, 1),
                    EndDate = new DateOnly(1994, 3, 31),
                    ReturnDate = null,
                    DecisionNo = 604,
                    DecisionDate = new DateOnly(1992, 3, 25),
                    Notes = null
                },
                new Vacation
                {
                    EmpId = "26411201700123",
                    Code = 8938,
                    Ended = true,
                    VacationTypeId = null,
                    StartDate = new DateOnly(1991, 4, 1),
                    EndDate = new DateOnly(1992, 3, 31),
                    ReturnDate = null,
                    DecisionNo = 193,
                    DecisionDate = new DateOnly(1991, 4, 3),
                    Notes = null
                }
            );

            context.SaveChanges();
        }


        // Seeding 
        if (!context.Salaries.Any())
        {
            context.Salaries.AddRange(
                new Salary
                {
                    EmpId = "26411281702333",
                    Code = 1001,
                    Agr_Wasefy = 1500.75m,
                    Agr_Mokamel = 500.25m,
                    Hafez_Taawedy = 300.00m,
                    M_asasy30 = 200.00m,
                    Hafez_Thabet = 100.00m,
                    MoKafat_Emt7anat = 400.50m,
                    All_Badalt = 250.00m,
                    Badalat_Okhra = 150.00m,
                    All_Mok = 500.00m,
                    Notes = "First salary record"
                },
                new Salary
                {
                    EmpId = "26411281702333",
                    Code = 1002,
                    Agr_Wasefy = 1800.00m,
                    Agr_Mokamel = 600.00m,
                    Hafez_Taawedy = 350.00m,
                    M_asasy30 = 250.00m,
                    Hafez_Thabet = 120.00m,
                    MoKafat_Emt7anat = 450.00m,
                    All_Badalt = 280.00m,
                    Badalat_Okhra = 170.00m,
                    All_Mok = 550.00m,
                    Notes = "Second salary record"
                }
            );

            context.SaveChanges();
        }


        if (!context.ThanksLetters.Any())
        {
            context.ThanksLetters.AddRange(
                new ThanksLetter
                {
                    EmpId = "26411281702333",
                    Code = 730,
                    LetterName = "لا يوجد",
                    Geha = null,
                    DecisionDate = null,
                    DecisionNo = 730
                },
                new ThanksLetter
                {
                    EmpId = "26411281702333",
                    Code = 1,
                    LetterName = "شكر وتقدير على ما بذلة من جهد وافر من تطوير ادارة المشتريات والمخازن بالمعهد",
                    Geha = "معهد الكبد القومى",
                    DecisionDate = new DateOnly(2021, 12, 15),
                    DecisionNo = 2910
                },
                new ThanksLetter
                {
                    EmpId = "26411281702333",
                    Code = 3,
                    LetterName = "لا يوجد",
                    Geha = null,
                    DecisionDate = null,
                    DecisionNo = 4161
                },
                new ThanksLetter
                {
                    EmpId = "26411281702333",
                    Code = 4,
                    LetterName = "لا يوجد",
                    Geha = null,
                    DecisionDate = null,
                    DecisionNo = 4349
                },
                new ThanksLetter
                {
                    EmpId = "26411281702333",
                    Code = 5,
                    LetterName = "شكر وتقدير على حسن المشاركة والتعاون المتميز مع الرؤساء والزملاء",
                    Geha = "معهد الكبد القومى",
                    DecisionDate = new DateOnly(2020, 8, 7),
                    DecisionNo = 8394
                },
                new ThanksLetter
                {
                    EmpId = "26411281702333",
                    Code = 6,
                    LetterName =
                        "شكر وتقدير على المساهمة الفعالة والأداء المتميز في العمل في ادارة المشتريات والمخازن بالمعهد",
                    Geha = "معهد الكبد القومى",
                    DecisionDate = new DateOnly(2022, 3, 4),
                    DecisionNo = 8395
                },
                new ThanksLetter
                {
                    EmpId = "26411281702333",
                    Code = 7,
                    LetterName = "شكر وتقدير على روح التعاون والتفاني والأداء المتميز في خدمة المعهد",
                    Geha = "معهد الكبد القومى",
                    DecisionDate = new DateOnly(2022, 2, 15),
                    DecisionNo = 8396
                },
                new ThanksLetter
                {
                    EmpId = "26411281702333",
                    Code = 8,
                    LetterName = "شكر وتقدير على الجهد المبذول والتفاني في العمل الدؤوب بمستشفى معهد الكبد القومى",
                    Geha = "معهد الكبد القومى",
                    DecisionDate = new DateOnly(2021, 6, 15),
                    DecisionNo = 8397
                },
                new ThanksLetter
                {
                    EmpId = "26411281702333",
                    Code = 9,
                    LetterName = "شكر وتقدير من المؤتمر الأول لأمراض الكبد والجهاز الهضمي والتغذية للأطفال",
                    Geha = "معهد الكبد القومى",
                    DecisionDate = new DateOnly(2019, 10, 24),
                    DecisionNo = 8450
                }
            );

            context.SaveChanges();
        }





        if (!context.FincialDegreeTypes.Any())
        {
            context.FincialDegreeTypes.AddRange(
                new FincialDegreeType { Code = 1, Name = "ممولة" },
                new FincialDegreeType { Code = 2, Name = "غير ممولة" }
            );
            context.SaveChanges();

        }


        // Seed FincialDegrees
        if (!context.FincialDegrees.Any())
        {
            context.FincialDegrees.AddRange(
                new FincialDegree { Code = 1, Name = "الأولى" },
                new FincialDegree { Code = 2, Name = "الثانية" },
                new FincialDegree { Code = 3, Name = "الثالثة" },
                new FincialDegree { Code = 4, Name = "الرابعة" },
                new FincialDegree { Code = 5, Name = "الخامسة" },
                new FincialDegree { Code = 6, Name = "السادسة" },
                new FincialDegree { Code = 100, Name = "الممتازة" },
                new FincialDegree { Code = 200, Name = "العالية" },
                new FincialDegree { Code = 300, Name = "مدير عام" },
                new FincialDegree { Code = 400, Name = "كبير ـ بدرجة مدير عام" },
                new FincialDegree { Code = 500, Name = "كبير" },
                new FincialDegree { Code = 600, Name = "الأولى (أ)" },
                new FincialDegree { Code = 650, Name = "الأولى (ب)" },
                new FincialDegree { Code = 700, Name = "الثانية (أ)" },
                new FincialDegree { Code = 750, Name = "الثانية (ب)" },
                new FincialDegree { Code = 800, Name = "الثالثة (أ)" },
                new FincialDegree { Code = 850, Name = "الثالثة (ب)" },
                new FincialDegree { Code = 875, Name = "الثالثة (ج)" },
                new FincialDegree { Code = 900, Name = "الرابعة (أ)" },
                new FincialDegree { Code = 950, Name = "الرابعة (ب)" },
                new FincialDegree { Code = 1000, Name = "الخامسة (أ)" },
                new FincialDegree { Code = 1500, Name = "الخامسة (ب)" },
                new FincialDegree { Code = 2000, Name = "السادسة (أ)" },
                new FincialDegree { Code = 2500, Name = "السادسة (ب)" },
                new FincialDegree { Code = 3000, Name = "استشارى" },
                new FincialDegree { Code = 4000, Name = "استشارى مساعد" },
                new FincialDegree { Code = 5000, Name = "زميل" }
                // لا يوجد اسم لهذه الدرجة
            );
            context.SaveChanges();

        }



        if (!context.JobDegredations.Any())
        {
            context.JobDegredations.AddRange(

                new JobDegredationData
                {
                    EmpId = "26411201700123",
                    FincialDegreeId = 2,
                    JobName = "نقل للعمل بجامعه المنوفية كلية التربية النوعية باشمون بوظيفه فنى معمل ثان",
                    JobStartDate = new DateOnly(2021, 6, 15),
                    DecisionNo = 265,
                    FincialDegreeDate=new DateOnly(2021,6,16),
                    DecisionDate = new DateOnly(2021, 6, 15),
                    CurrentDegree = false,
                    Code = 63904,
                }
                ,

            new JobDegredationData
            {
                EmpId = "26411281702333",
                FincialDegreeId = 1,
                JobName = "نقل للعمل بجامعه المنوفية كلية التربية النوعية باشمون بوظيفه فنى معمل ثان",
                JobStartDate = new DateOnly(2021, 6, 15),
                DecisionNo = 266,
                DecisionDate = new DateOnly(2021, 6, 16),
                CurrentDegree = true,
                Code = 6304,
                FincialDegreeDate = new DateOnly(2025, 5, 2)
            }
            );
            context.SaveChanges();

        }





        #endregion

    }
}

