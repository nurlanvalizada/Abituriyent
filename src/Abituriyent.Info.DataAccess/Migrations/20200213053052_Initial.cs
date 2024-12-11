using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Abituriyent.Info.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: false),
                    Message = table.Column<string>(maxLength: 3000, nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2(1)", nullable: false),
                    Topic = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataDictionary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Header = table.Column<string>(maxLength: 500, nullable: false),
                    Key = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataDictionary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(1)", nullable: false),
                    ExamType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HappyStudents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(maxLength: 60, nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 100, nullable: true),
                    Review = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HappyStudents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    About = table.Column<string>(maxLength: 2000, nullable: false),
                    FacebookProfile = table.Column<string>(maxLength: 100, nullable: true),
                    FullName = table.Column<string>(maxLength: 40, nullable: false),
                    GooglePlusProfile = table.Column<string>(maxLength: 100, nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 100, nullable: true),
                    Profession = table.Column<string>(maxLength: 50, nullable: false),
                    TwitterProfile = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    TestType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
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
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schools_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupExams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExamId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupExams_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupExams_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PdfContentType = table.Column<string>(maxLength: 20, nullable: true),
                    PdfFile = table.Column<byte[]>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false),
                    VideoUrl = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    GroupId = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 100, nullable: true),
                    ScoreRate = table.Column<byte>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(nullable: false),
                    TestId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 3000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
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
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    FatherName = table.Column<string>(maxLength: 30, nullable: true),
                    GroupId = table.Column<int>(nullable: false),
                    HomePhone = table.Column<string>(maxLength: 15, nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImageContentType = table.Column<string>(maxLength: 20, nullable: true),
                    MobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    SchoolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdminId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 100, nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2(1)", nullable: false),
                    ShortContent = table.Column<string>(maxLength: 1000, nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                name: "CourseLessons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<int>(nullable: false),
                    LessonId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseLessons_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseLessons_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerId = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestAnswers_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseRatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true),
                    CourseId = table.Column<int>(nullable: false),
                    Stars = table.Column<byte>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRatings_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseRatings_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentExams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndTime = table.Column<TimeSpan>(type: "time(1)", nullable: true),
                    ExamResult = table.Column<int>(nullable: false),
                    GroupExamId = table.Column<int>(nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(1)", nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExams_GroupExams_GroupExamId",
                        column: x => x.GroupExamId,
                        principalTable: "GroupExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentExams_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentLessonOpenTestAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerText = table.Column<string>(maxLength: 500, nullable: true),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLessonOpenTestAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentLessonOpenTestAnswers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentLessonTestAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLessonTestAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentLessonTestAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentLessonTestAnswers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseLessonTests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseLessonId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    TestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLessonTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseLessonTests_CourseLessons_CourseLessonId",
                        column: x => x.CourseLessonId,
                        principalTable: "CourseLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseLessonTests_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentLessons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseLessonId = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2(1)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2(1)", nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentLessons_CourseLessons_CourseLessonId",
                        column: x => x.CourseLessonId,
                        principalTable: "CourseLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentLessons_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamTests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseLessonTestId = table.Column<int>(nullable: false),
                    GroupExamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamTests_CourseLessonTests_CourseLessonTestId",
                        column: x => x.CourseLessonTestId,
                        principalTable: "CourseLessonTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamTests_GroupExams_GroupExamId",
                        column: x => x.GroupExamId,
                        principalTable: "GroupExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuizTests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseLessonId = table.Column<int>(nullable: false),
                    CourseLessonTestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizTests_CourseLessons_CourseLessonId",
                        column: x => x.CourseLessonId,
                        principalTable: "CourseLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizTests_CourseLessonTests_CourseLessonTestId",
                        column: x => x.CourseLessonTestId,
                        principalTable: "CourseLessonTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentExamOpenTestAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerText = table.Column<string>(maxLength: 500, nullable: true),
                    ExamTestId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExamOpenTestAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExamOpenTestAnswers_ExamTests_ExamTestId",
                        column: x => x.ExamTestId,
                        principalTable: "ExamTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentExamOpenTestAnswers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentExamTestAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerId = table.Column<int>(nullable: false),
                    ExamTestId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExamTestAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExamTestAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentExamTestAnswers_ExamTests_ExamTestId",
                        column: x => x.ExamTestId,
                        principalTable: "ExamTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentExamTestAnswers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_PersonId",
                table: "Admins",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TestId",
                table: "Answers",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonId",
                table: "AspNetUsers",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_GroupId",
                table: "Courses",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubjectId",
                table: "Courses",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLessons_CourseId",
                table: "CourseLessons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLessons_LessonId",
                table: "CourseLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLessons_CourseId_LessonId",
                table: "CourseLessons",
                columns: new[] { "CourseId", "LessonId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseLessonTests_CourseLessonId",
                table: "CourseLessonTests",
                column: "CourseLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLessonTests_TestId",
                table: "CourseLessonTests",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLessonTests_CourseLessonId_TestId",
                table: "CourseLessonTests",
                columns: new[] { "CourseLessonId", "TestId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseRatings_CourseId",
                table: "CourseRatings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRatings_StudentId",
                table: "CourseRatings",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRatings_StudentId_CourseId",
                table: "CourseRatings",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamTests_CourseLessonTestId",
                table: "ExamTests",
                column: "CourseLessonTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTests_GroupExamId",
                table: "ExamTests",
                column: "GroupExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTests_GroupExamId_CourseLessonTestId",
                table: "ExamTests",
                columns: new[] { "GroupExamId", "CourseLessonTestId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupExams_ExamId",
                table: "GroupExams",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupExams_GroupId",
                table: "GroupExams",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupExams_GroupId_ExamId",
                table: "GroupExams",
                columns: new[] { "GroupId", "ExamId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizTests_CourseLessonId",
                table: "QuizTests",
                column: "CourseLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizTests_CourseLessonTestId",
                table: "QuizTests",
                column: "CourseLessonTestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizTests_CourseLessonId_CourseLessonTestId",
                table: "QuizTests",
                columns: new[] { "CourseLessonId", "CourseLessonTestId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_News_AdminId",
                table: "News",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_CityId",
                table: "Schools",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PersonId",
                table: "Students",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolId",
                table: "Students",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExams_GroupExamId",
                table: "StudentExams",
                column: "GroupExamId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExams_StudentId",
                table: "StudentExams",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExams_StudentId_GroupExamId",
                table: "StudentExams",
                columns: new[] { "StudentId", "GroupExamId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamOpenTestAnswers_ExamTestId",
                table: "StudentExamOpenTestAnswers",
                column: "ExamTestId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamOpenTestAnswers_StudentId",
                table: "StudentExamOpenTestAnswers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamOpenTestAnswers_StudentId_ExamTestId_AnswerText",
                table: "StudentExamOpenTestAnswers",
                columns: new[] { "StudentId", "ExamTestId", "AnswerText" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamTestAnswers_AnswerId",
                table: "StudentExamTestAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamTestAnswers_ExamTestId",
                table: "StudentExamTestAnswers",
                column: "ExamTestId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamTestAnswers_StudentId",
                table: "StudentExamTestAnswers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamTestAnswers_StudentId_ExamTestId_AnswerId",
                table: "StudentExamTestAnswers",
                columns: new[] { "StudentId", "ExamTestId", "AnswerId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessons_CourseLessonId",
                table: "StudentLessons",
                column: "CourseLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessons_StudentId",
                table: "StudentLessons",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessons_StudentId_CourseLessonId",
                table: "StudentLessons",
                columns: new[] { "StudentId", "CourseLessonId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonOpenTestAnswers_StudentId",
                table: "StudentLessonOpenTestAnswers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonOpenTestAnswers_StudentId_AnswerText",
                table: "StudentLessonOpenTestAnswers",
                columns: new[] { "StudentId", "AnswerText" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonTestAnswers_AnswerId",
                table: "StudentLessonTestAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonTestAnswers_StudentId",
                table: "StudentLessonTestAnswers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonTestAnswers_StudentId_AnswerId",
                table: "StudentLessonTestAnswers",
                columns: new[] { "StudentId", "AnswerId" });

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_AnswerId",
                table: "TestAnswers",
                column: "AnswerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_TestId",
                table: "TestAnswers",
                column: "TestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_TestId_AnswerId",
                table: "TestAnswers",
                columns: new[] { "TestId", "AnswerId" });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

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
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "CourseRatings");

            migrationBuilder.DropTable(
                name: "DataDictionary");

            migrationBuilder.DropTable(
                name: "HappyStudents");

            migrationBuilder.DropTable(
                name: "QuizTests");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "StudentExams");

            migrationBuilder.DropTable(
                name: "StudentExamOpenTestAnswers");

            migrationBuilder.DropTable(
                name: "StudentExamTestAnswers");

            migrationBuilder.DropTable(
                name: "StudentLessons");

            migrationBuilder.DropTable(
                name: "StudentLessonOpenTestAnswers");

            migrationBuilder.DropTable(
                name: "StudentLessonTestAnswers");

            migrationBuilder.DropTable(
                name: "TestAnswers");

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
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ExamTests");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CourseLessonTests");

            migrationBuilder.DropTable(
                name: "GroupExams");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "CourseLessons");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
