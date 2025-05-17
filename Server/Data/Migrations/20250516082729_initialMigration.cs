using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdKinds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdKinds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllowanceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowanceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationalLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SortId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExistanceCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExistanceCases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FincialDegreeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FincialDegreeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinicialZemaTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinicialZemaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Governrates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governrates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MandateTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandateTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryStateTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryStateTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonExistanceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonExistanceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PenaltyCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PenaltyCases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PenaltyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PenaltyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QualGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualGrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YearReportGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearReportGrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    educationalLevelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_EducationalLevels_educationalLevelID",
                        column: x => x.educationalLevelID,
                        principalTable: "EducationalLevels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FincialDegrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FincialDegreeTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FincialDegrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FincialDegrees_FincialDegreeTypes_FincialDegreeTypeId",
                        column: x => x.FincialDegreeTypeId,
                        principalTable: "FincialDegreeTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobSubGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JobGroupsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSubGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSubGroups_JobGroups_JobGroupsId",
                        column: x => x.JobGroupsId,
                        principalTable: "JobGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "generalAds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Level = table.Column<bool>(type: "bit", nullable: false),
                    SpecialLevel = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    SectorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generalAds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_generalAds_Sectors_SectorID",
                        column: x => x.SectorID,
                        principalTable: "Sectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JobMission = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    JobSubGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobNames_JobSubGroups_JobSubGroupId",
                        column: x => x.JobSubGroupId,
                        principalTable: "JobSubGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "subAds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Level = table.Column<bool>(type: "bit", nullable: false),
                    SpecialLevel = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    GeneralAdId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subAds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subAds_generalAds_GeneralAdId",
                        column: x => x.GeneralAdId,
                        principalTable: "generalAds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    SubAdID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_departments_subAds_SubAdID",
                        column: x => x.SubAdID,
                        principalTable: "subAds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    NationalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    JobGroupId = table.Column<int>(type: "int", nullable: false),
                    JobSubGroupId = table.Column<int>(type: "int", nullable: false),
                    JobNameId = table.Column<int>(type: "int", nullable: false),
                    SubAdId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    IsExist = table.Column<bool>(type: "bit", nullable: false),
                    ExistaceCaseId = table.Column<int>(type: "int", nullable: true),
                    NonExistanceTypeId = table.Column<int>(type: "int", nullable: true),
                    TaminNo = table.Column<int>(type: "int", nullable: true),
                    AppointDate = table.Column<DateOnly>(type: "date", nullable: true),
                    WorkEndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkDate = table.Column<DateOnly>(type: "date", nullable: true),
                    HealthStateId = table.Column<int>(type: "int", nullable: true),
                    AppointDN = table.Column<int>(type: "int", nullable: true),
                    ExperanceDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExperanceDN = table.Column<int>(type: "int", nullable: true),
                    Serial = table.Column<int>(type: "int", nullable: true),
                    SocialStateId = table.Column<int>(type: "int", nullable: true),
                    GovernrateId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WorkDateFt = table.Column<DateOnly>(type: "date", nullable: true),
                    ReAppointDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CombinationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AppointDateTxt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkDateTxt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkDateFtTxt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReAppointDateTxt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CombinationDateTxt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperanceDateTxt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoundDegree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperanceDomain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkEndDec = table.Column<int>(type: "int", nullable: true),
                    WorkEndDeDate = table.Column<DateOnly>(type: "date", nullable: true),
                    YearBuy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FJobDate = table.Column<DateOnly>(type: "date", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Village = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearEmp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    LastYearBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    SickBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ReservedDays = table.Column<int>(type: "int", nullable: true),
                    ReservedMonths = table.Column<int>(type: "int", nullable: true),
                    ReservedYears = table.Column<int>(type: "int", nullable: true),
                    DisabilityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisabilityFamilyMember = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DegreeDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.NationalId);
                    table.ForeignKey(
                        name: "FK_Employees_ExistanceCases_ExistaceCaseId",
                        column: x => x.ExistaceCaseId,
                        principalTable: "ExistanceCases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Governrates_GovernrateId",
                        column: x => x.GovernrateId,
                        principalTable: "Governrates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_HealthStates_HealthStateId",
                        column: x => x.HealthStateId,
                        principalTable: "HealthStates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_JobGroups_JobGroupId",
                        column: x => x.JobGroupId,
                        principalTable: "JobGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_JobNames_JobNameId",
                        column: x => x.JobNameId,
                        principalTable: "JobNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_JobSubGroups_JobSubGroupId",
                        column: x => x.JobSubGroupId,
                        principalTable: "JobSubGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_NonExistanceTypes_NonExistanceTypeId",
                        column: x => x.NonExistanceTypeId,
                        principalTable: "NonExistanceTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_SocialStates_SocialStateId",
                        column: x => x.SocialStateId,
                        principalTable: "SocialStates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_subAds_SubAdId",
                        column: x => x.SubAdId,
                        principalTable: "subAds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Allowances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AllowanceTypeId = table.Column<int>(type: "int", nullable: true),
                    DecisionNo = table.Column<int>(type: "int", nullable: true),
                    DecisionDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allowances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Allowances_AllowanceTypes_AllowanceTypeId",
                        column: x => x.AllowanceTypeId,
                        principalTable: "AllowanceTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Allowances_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "NationalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinicialZemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequiredZemaNo = table.Column<int>(type: "int", nullable: true),
                    FinicialZemaTypeId = table.Column<int>(type: "int", nullable: true),
                    LastDecisionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    NewSubmissionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Submitted = table.Column<bool>(type: "bit", nullable: false),
                    SubmissionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    GraftGoingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    GraftComingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinicialZemas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinicialZemas_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "NationalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinicialZemas_FinicialZemaTypes_FinicialZemaTypeId",
                        column: x => x.FinicialZemaTypeId,
                        principalTable: "FinicialZemaTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobDegredations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FincialDegreeId = table.Column<int>(type: "int", nullable: false),
                    JobTypeId = table.Column<int>(type: "int", nullable: true),
                    JobName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    JobStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    JobEndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DecisionNo = table.Column<int>(type: "int", nullable: false),
                    DecisionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CurrentDegree = table.Column<bool>(type: "bit", nullable: false),
                    FincialDegreeDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDegredations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobDegredations_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "NationalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobDegredations_FincialDegrees_FincialDegreeId",
                        column: x => x.FincialDegreeId,
                        principalTable: "FincialDegrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobDegredations_JobTypes_JobTypeId",
                        column: x => x.JobTypeId,
                        principalTable: "JobTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lagnas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MemberType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DecisionNo = table.Column<int>(type: "int", nullable: true),
                    DecisionDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lagnas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lagnas_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "NationalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MandateDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MandateTypeId = table.Column<int>(type: "int", nullable: true),
                    FacultyId = table.Column<int>(type: "int", nullable: true),
                    IsMandated = table.Column<bool>(type: "bit", nullable: false),
                    Geha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MandateJob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DecisionNo = table.Column<int>(type: "int", nullable: true),
                    DecisionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandateDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MandateDatas_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "NationalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MandateDatas_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MandateDatas_MandateTypes_MandateTypeId",
                        column: x => x.MandateTypeId,
                        principalTable: "MandateTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MilitaryStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MilitaryStateTypeId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentMilitaryState = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MilitaryStates_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "NationalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MilitaryStates_MilitaryStateTypes_MilitaryStateTypeId",
                        column: x => x.MilitaryStateTypeId,
                        principalTable: "MilitaryStateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Penalties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PenaltyTypeId = table.Column<int>(type: "int", nullable: true),
                    PenaltyCaseId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DecisionNo = table.Column<int>(type: "int", nullable: true),
                    DecisionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: true),
                    CancelationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CancelationDecisionNo = table.Column<int>(type: "int", nullable: true),
                    CancelationReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penalties_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "NationalId");
                    table.ForeignKey(
                        name: "FK_Penalties_PenaltyCases_PenaltyCaseId",
                        column: x => x.PenaltyCaseId,
                        principalTable: "PenaltyCases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Penalties_PenaltyTypes_PenaltyTypeId",
                        column: x => x.PenaltyTypeId,
                        principalTable: "PenaltyTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    QualPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DecisionNumber = table.Column<int>(type: "int", nullable: false),
                    DecisionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    GraduationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LastQual = table.Column<bool>(type: "bit", nullable: false),
                    CertificateID = table.Column<int>(type: "int", nullable: false),
                    QualGradeID = table.Column<int>(type: "int", nullable: false),
                    NationalID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qualifications_Certificates_CertificateID",
                        column: x => x.CertificateID,
                        principalTable: "Certificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Qualifications_Employees_NationalID",
                        column: x => x.NationalID,
                        principalTable: "Employees",
                        principalColumn: "NationalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Qualifications_QualGrades_QualGradeID",
                        column: x => x.QualGradeID,
                        principalTable: "QualGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Agr_Wasefy = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Agr_Mokamel = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Hafez_Taawedy = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    M_asasy30 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Hafez_Thabet = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MoKafat_Emt7anat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    All_Badalt = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Badalat_Okhra = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    All_Mok = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salaries_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "NationalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanksLetters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LetterName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Geha = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DecisionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DecisionNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanksLetters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThanksLetters_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "NationalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Training_Employees_NationalId",
                        column: x => x.NationalId,
                        principalTable: "Employees",
                        principalColumn: "NationalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Ended = table.Column<bool>(type: "bit", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VacationTypeId = table.Column<int>(type: "int", nullable: true),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DecisionNo = table.Column<int>(type: "int", nullable: true),
                    DecisionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacations_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "NationalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacations_VacationTypes_VacationTypeId",
                        column: x => x.VacationTypeId,
                        principalTable: "VacationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "YearReportLaws",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Geha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearReportLaws", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YearReportLaws_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "NationalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YearReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Degree = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YearReports_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "NationalId");
                    table.ForeignKey(
                        name: "FK_YearReports_YearReportGrades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "YearReportGrades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allowances_AllowanceTypeId",
                table: "Allowances",
                column: "AllowanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Allowances_EmpId",
                table: "Allowances",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_educationalLevelID",
                table: "Certificates",
                column: "educationalLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_departments_SubAdID",
                table: "departments",
                column: "SubAdID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ExistaceCaseId",
                table: "Employees",
                column: "ExistaceCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FacultyId",
                table: "Employees",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GovernrateId",
                table: "Employees",
                column: "GovernrateId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_HealthStateId",
                table: "Employees",
                column: "HealthStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobGroupId",
                table: "Employees",
                column: "JobGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobNameId",
                table: "Employees",
                column: "JobNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobSubGroupId",
                table: "Employees",
                column: "JobSubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NonExistanceTypeId",
                table: "Employees",
                column: "NonExistanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SocialStateId",
                table: "Employees",
                column: "SocialStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SubAdId",
                table: "Employees",
                column: "SubAdId");

            migrationBuilder.CreateIndex(
                name: "IX_FincialDegrees_FincialDegreeTypeId",
                table: "FincialDegrees",
                column: "FincialDegreeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinicialZemas_EmpId",
                table: "FinicialZemas",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_FinicialZemas_FinicialZemaTypeId",
                table: "FinicialZemas",
                column: "FinicialZemaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_generalAds_SectorID",
                table: "generalAds",
                column: "SectorID");

            migrationBuilder.CreateIndex(
                name: "IX_JobDegredations_EmpId",
                table: "JobDegredations",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDegredations_FincialDegreeId",
                table: "JobDegredations",
                column: "FincialDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDegredations_JobTypeId",
                table: "JobDegredations",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobNames_JobSubGroupId",
                table: "JobNames",
                column: "JobSubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSubGroups_JobGroupsId",
                table: "JobSubGroups",
                column: "JobGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_Lagnas_EmpId",
                table: "Lagnas",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_MandateDatas_EmpId",
                table: "MandateDatas",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_MandateDatas_FacultyId",
                table: "MandateDatas",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_MandateDatas_MandateTypeId",
                table: "MandateDatas",
                column: "MandateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryStates_EmpId",
                table: "MilitaryStates",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryStates_MilitaryStateTypeId",
                table: "MilitaryStates",
                column: "MilitaryStateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_EmpId",
                table: "Penalties",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_PenaltyCaseId",
                table: "Penalties",
                column: "PenaltyCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_PenaltyTypeId",
                table: "Penalties",
                column: "PenaltyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_CertificateID",
                table: "Qualifications",
                column: "CertificateID");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_NationalID",
                table: "Qualifications",
                column: "NationalID");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_QualGradeID",
                table: "Qualifications",
                column: "QualGradeID");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmpId",
                table: "Salaries",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_subAds_GeneralAdId",
                table: "subAds",
                column: "GeneralAdId");

            migrationBuilder.CreateIndex(
                name: "IX_ThanksLetters_EmpId",
                table: "ThanksLetters",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Training_NationalId",
                table: "Training",
                column: "NationalId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_EmpId",
                table: "Vacations",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_VacationTypeId",
                table: "Vacations",
                column: "VacationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_YearReportLaws_EmpId",
                table: "YearReportLaws",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_YearReports_EmpId",
                table: "YearReports",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_YearReports_GradeId",
                table: "YearReports",
                column: "GradeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdKinds");

            migrationBuilder.DropTable(
                name: "Allowances");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FinicialZemas");

            migrationBuilder.DropTable(
                name: "JobDegredations");

            migrationBuilder.DropTable(
                name: "Lagnas");

            migrationBuilder.DropTable(
                name: "MandateDatas");

            migrationBuilder.DropTable(
                name: "MilitaryStates");

            migrationBuilder.DropTable(
                name: "Penalties");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "ThanksLetters");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "YearReportLaws");

            migrationBuilder.DropTable(
                name: "YearReports");

            migrationBuilder.DropTable(
                name: "AllowanceTypes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FinicialZemaTypes");

            migrationBuilder.DropTable(
                name: "FincialDegrees");

            migrationBuilder.DropTable(
                name: "JobTypes");

            migrationBuilder.DropTable(
                name: "MandateTypes");

            migrationBuilder.DropTable(
                name: "MilitaryStateTypes");

            migrationBuilder.DropTable(
                name: "PenaltyCases");

            migrationBuilder.DropTable(
                name: "PenaltyTypes");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "QualGrades");

            migrationBuilder.DropTable(
                name: "VacationTypes");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "YearReportGrades");

            migrationBuilder.DropTable(
                name: "FincialDegreeTypes");

            migrationBuilder.DropTable(
                name: "EducationalLevels");

            migrationBuilder.DropTable(
                name: "ExistanceCases");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Governrates");

            migrationBuilder.DropTable(
                name: "HealthStates");

            migrationBuilder.DropTable(
                name: "JobNames");

            migrationBuilder.DropTable(
                name: "NonExistanceTypes");

            migrationBuilder.DropTable(
                name: "SocialStates");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "JobSubGroups");

            migrationBuilder.DropTable(
                name: "subAds");

            migrationBuilder.DropTable(
                name: "JobGroups");

            migrationBuilder.DropTable(
                name: "generalAds");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
