using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace v1jobportal.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllJobListings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(nullable: false),
                    DutyStation = table.Column<string>(nullable: false),
                    Duties = table.Column<string>(nullable: false),
                    Doctorate = table.Column<bool>(nullable: false),
                    MastersDegree = table.Column<bool>(nullable: false),
                    bachelorsDegree = table.Column<bool>(nullable: false),
                    Diploma = table.Column<bool>(nullable: false),
                    Certificate = table.Column<bool>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllJobListings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantBioData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpId = table.Column<int>(nullable: false),
                    JobId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    Residence = table.Column<string>(nullable: false),
                    PrimaryPhoneNumber = table.Column<string>(nullable: false),
                    AlternativePhoneNumber = table.Column<string>(nullable: true),
                    LanguagesSpoken = table.Column<int>(nullable: false),
                    WorkedWithIDRC = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantBioData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jd_JobId = table.Column<int>(nullable: false),
                    Emp_Id = table.Column<int>(nullable: false),
                    UploadCv = table.Column<byte>(nullable: false),
                    UploadCoverLetter = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantSummary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantSummaryData = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantSummary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CertificationsAndAwards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificationName = table.Column<string>(nullable: false),
                    CertifyingInstitutionName = table.Column<string>(nullable: false),
                    CertificationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationsAndAwards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EditRoleViewModels",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RoleName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditRoleViewModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducaitonDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfInstitution = table.Column<string>(nullable: true),
                    QualificationAttained = table.Column<string>(nullable: true),
                    DateQualificationWasAttained = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducaitonDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentHistoryModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployerName = table.Column<string>(nullable: false),
                    EmploymentStartDate = table.Column<DateTime>(nullable: false),
                    EmploymentEndDate = table.Column<DateTime>(nullable: false),
                    JobTitle = table.Column<string>(nullable: false),
                    SummaryOfDuties = table.Column<string>(nullable: false),
                    SummaryOfAchievments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentHistoryModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PendingApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jd_JobId = table.Column<int>(nullable: true),
                    Emp_Id = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: true),
                    Bd_FirstName = table.Column<string>(nullable: true),
                    Bd_MiddleName = table.Column<string>(nullable: true),
                    Bd_LastName = table.Column<string>(nullable: true),
                    Bd_Residence = table.Column<string>(nullable: true),
                    Bd_PrimaryPhoneNumber = table.Column<string>(maxLength: 13, nullable: true),
                    Bd_AlternativePhoneNumber = table.Column<string>(nullable: true),
                    Bd_LanguagesSpoken = table.Column<string>(nullable: true),
                    Bd_WorkedWithIDRC = table.Column<bool>(nullable: true),
                    Ed_NameOfInstitution = table.Column<string>(nullable: true),
                    Ed_QualificationAttained = table.Column<string>(nullable: true),
                    Ed_DateQualificationWasAttained = table.Column<DateTime>(nullable: true),
                    Eh_EmployerName = table.Column<string>(nullable: true),
                    Eh_EmploymentStartDate = table.Column<DateTime>(nullable: true),
                    Eh_EmploymentEndDate = table.Column<DateTime>(nullable: true),
                    Eh_JobTitle = table.Column<string>(nullable: true),
                    Eh_SummaryOfDuties = table.Column<string>(nullable: true),
                    Eh_SummaryOfAchievments = table.Column<string>(nullable: true),
                    Ca_CertificationName = table.Column<string>(nullable: true),
                    Ca_CertifyingInstitutionName = table.Column<string>(nullable: true),
                    Ca_CertificationDate = table.Column<DateTime>(nullable: true),
                    Ud_CertificationAwardName = table.Column<string>(nullable: true),
                    Ud_InstitutionName = table.Column<string>(nullable: true),
                    Ud_DateOfCertification = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleCv = table.Column<long>(nullable: false),
                    ScheduleCoverLeter = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubmittedApplication",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jd_JobId = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Jd_JobTitle = table.Column<string>(nullable: false),
                    Bd_FirstName = table.Column<string>(nullable: false),
                    Bd_MiddleName = table.Column<string>(nullable: true),
                    Bd_LastName = table.Column<string>(nullable: false),
                    Bd_Residence = table.Column<string>(nullable: false),
                    Bd_PrimaryPhoneNumber = table.Column<string>(maxLength: 13, nullable: false),
                    Bd_AlternativePhoneNumber = table.Column<string>(nullable: true),
                    Bd_LanguagesSpoken = table.Column<string>(nullable: false),
                    Bd_WorkedWithIDRC = table.Column<bool>(nullable: false),
                    Ed_NameOfInstitution = table.Column<string>(nullable: true),
                    Ed_QualificationAttained = table.Column<string>(nullable: true),
                    Ed_DateQualificationWasAttained = table.Column<DateTime>(nullable: false),
                    Eh_EmployerName = table.Column<string>(nullable: true),
                    Eh_EmploymentStartDate = table.Column<DateTime>(nullable: false),
                    Eh_EmploymentEndDate = table.Column<DateTime>(nullable: false),
                    Eh_JobTitle = table.Column<string>(nullable: false),
                    Eh_SummaryOfDuties = table.Column<string>(nullable: true),
                    Eh_SummaryOfAchievments = table.Column<string>(nullable: true),
                    Ca_CertificationName = table.Column<string>(nullable: true),
                    Ca_CertifyingInstitutionName = table.Column<string>(nullable: true),
                    Ca_CertificationDate = table.Column<DateTime>(nullable: false),
                    Ud_CertificationAwardName = table.Column<string>(nullable: true),
                    Ud_InstitutionName = table.Column<string>(nullable: true),
                    Ud_DateOfCertification = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmittedApplication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "62b934f5-afe3-4eb9-b7e7-05777637a5f8", "f4769726-bee8-4b93-93cf-70b36db78544", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "67615ce7-518c-4202-a548-b9485e892000", "e7dab9ac-8ec6-4957-8d34-5216b682aabb", "Admin", "ADMIN" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllJobListings");

            migrationBuilder.DropTable(
                name: "ApplicantBioData");

            migrationBuilder.DropTable(
                name: "ApplicantDocuments");

            migrationBuilder.DropTable(
                name: "ApplicantSummary");

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
                name: "CertificationsAndAwards");

            migrationBuilder.DropTable(
                name: "EditRoleViewModels");

            migrationBuilder.DropTable(
                name: "EducaitonDetails");

            migrationBuilder.DropTable(
                name: "EmploymentHistoryModel");

            migrationBuilder.DropTable(
                name: "PendingApplications");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "SubmittedApplication");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
