using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Abituriyent.Info.DataAccess;
using Abituriyent.Info.Core.Models;

namespace Abituriyent.Info.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200213053052_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Status");

                    b.Property<int>("TestId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(3000);

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<int>("PersonId");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.ContactMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(3000);

                    b.Property<DateTime>("SendDate")
                        .HasAnnotation("SqlServer:ColumnType", "datetime2(1)");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("ContactMessages");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(3000);

                    b.Property<int>("GroupId");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(100);

                    b.Property<byte>("ScoreRate");

                    b.Property<bool>("Status");

                    b.Property<int>("SubjectId");

                    b.Property<int?>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.CourseLesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<int>("LessonId");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("LessonId");

                    b.HasIndex("CourseId", "LessonId")
                        .IsUnique();

                    b.ToTable("CourseLessons");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.CourseLessonTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseLessonId");

                    b.Property<bool>("Status");

                    b.Property<int>("TestId");

                    b.HasKey("Id");

                    b.HasIndex("CourseLessonId");

                    b.HasIndex("TestId");

                    b.HasIndex("CourseLessonId", "TestId")
                        .IsUnique();

                    b.ToTable("CourseLessonTests");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.CourseRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment")
                        .HasMaxLength(1000);

                    b.Property<int>("CourseId");

                    b.Property<byte>("Stars");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.HasIndex("StudentId", "CourseId")
                        .IsUnique();

                    b.ToTable("CourseRatings");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.DataDictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasAnnotation("SqlServer:ColumnType", "nvarchar(MAX)");

                    b.HasKey("Id");

                    b.ToTable("DataDictionary");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date")
                        .HasAnnotation("SqlServer:ColumnType", "date");

                    b.Property<TimeSpan>("EndTime")
                        .HasAnnotation("SqlServer:ColumnType", "time(1)");

                    b.Property<int>("ExamType");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<TimeSpan>("StartTime")
                        .HasAnnotation("SqlServer:ColumnType", "time(1)");

                    b.HasKey("Id");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.ExamTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseLessonTestId");

                    b.Property<int>("GroupExamId");

                    b.HasKey("Id");

                    b.HasIndex("CourseLessonTestId");

                    b.HasIndex("GroupExamId");

                    b.HasIndex("GroupExamId", "CourseLessonTestId")
                        .IsUnique();

                    b.ToTable("ExamTests");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.GroupExam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExamId");

                    b.Property<int>("GroupId");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("GroupId");

                    b.HasIndex("GroupId", "ExamId")
                        .IsUnique();

                    b.ToTable("GroupExams");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.HappyStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullName")
                        .HasMaxLength(60);

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(100);

                    b.Property<string>("Review")
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.ToTable("HappyStudents");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.QuizTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseLessonId");

                    b.Property<int>("CourseLessonTestId");

                    b.HasKey("Id");

                    b.HasIndex("CourseLessonId");

                    b.HasIndex("CourseLessonTestId");

                    b.HasIndex("CourseLessonId", "CourseLessonTestId")
                        .IsUnique();

                    b.ToTable("QuizTests");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PdfContentType")
                        .HasMaxLength(20);

                    b.Property<byte[]>("PdfFile");

                    b.Property<bool>("Status");

                    b.Property<int>("SubjectId");

                    b.Property<string>("VideoUrl")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdminId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasAnnotation("SqlServer:ColumnType", "nvarchar(MAX)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(100);

                    b.Property<DateTime>("PublishDate")
                        .HasAnnotation("SqlServer:ColumnType", "datetime2(1)");

                    b.Property<string>("ShortContent")
                        .HasMaxLength(1000);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("Gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<DateTime>("DateOfBirth")
                        .HasAnnotation("SqlServer:ColumnType", "date");

                    b.Property<string>("FatherName")
                        .HasMaxLength(30);

                    b.Property<int>("GroupId");

                    b.Property<string>("HomePhone")
                        .HasMaxLength(15);

                    b.Property<byte[]>("Image");

                    b.Property<string>("ImageContentType")
                        .HasMaxLength(20);

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(15);

                    b.Property<int>("PersonId");

                    b.Property<int>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.HasIndex("SchoolId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.StudentExam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan?>("EndTime")
                        .HasAnnotation("SqlServer:ColumnType", "time(1)");

                    b.Property<int>("ExamResult");

                    b.Property<int>("GroupExamId");

                    b.Property<TimeSpan>("StartTime")
                        .HasAnnotation("SqlServer:ColumnType", "time(1)");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("GroupExamId");

                    b.HasIndex("StudentId");

                    b.HasIndex("StudentId", "GroupExamId")
                        .IsUnique();

                    b.ToTable("StudentExams");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.StudentExamOpenTestAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerText")
                        .HasMaxLength(500);

                    b.Property<int>("ExamTestId");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("ExamTestId");

                    b.HasIndex("StudentId");

                    b.HasIndex("StudentId", "ExamTestId", "AnswerText");

                    b.ToTable("StudentExamOpenTestAnswers");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.StudentExamTestAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerId");

                    b.Property<int>("ExamTestId");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("ExamTestId");

                    b.HasIndex("StudentId");

                    b.HasIndex("StudentId", "ExamTestId", "AnswerId");

                    b.ToTable("StudentExamTestAnswers");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.StudentLesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseLessonId");

                    b.Property<DateTime?>("EndDate")
                        .HasAnnotation("SqlServer:ColumnType", "datetime2(1)");

                    b.Property<DateTime>("StartDate")
                        .HasAnnotation("SqlServer:ColumnType", "datetime2(1)");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("CourseLessonId");

                    b.HasIndex("StudentId");

                    b.HasIndex("StudentId", "CourseLessonId");

                    b.ToTable("StudentLessons");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.StudentLessonOpenTestAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerText")
                        .HasMaxLength(500);

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("StudentId", "AnswerText");

                    b.ToTable("StudentLessonOpenTestAnswers");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.StudentLessonTestAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerId");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("StudentId");

                    b.HasIndex("StudentId", "AnswerId");

                    b.ToTable("StudentLessonTestAnswers");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<string>("FacebookProfile")
                        .HasMaxLength(100);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("GooglePlusProfile")
                        .HasMaxLength(100);

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(100);

                    b.Property<string>("Profession")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("TwitterProfile")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasAnnotation("SqlServer:ColumnType", "nvarchar(MAX)");

                    b.Property<bool>("Status");

                    b.Property<int>("TestType");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.TestAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerId");

                    b.Property<int>("TestId");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId")
                        .IsUnique();

                    b.HasIndex("TestId")
                        .IsUnique();

                    b.HasIndex("TestId", "AnswerId");

                    b.ToTable("TestAnswers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Admin", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Person", "Person")
                        .WithOne("Admin")
                        .HasForeignKey("Abituriyent.Info.Core.Domain.Admin", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Answer", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Test", "Test")
                        .WithMany("Answers")
                        .HasForeignKey("TestId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.ApplicationUser", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Person", "Person")
                        .WithOne("ApplicationUser")
                        .HasForeignKey("Abituriyent.Info.Core.Domain.ApplicationUser", "PersonId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Course", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Group", "Group")
                        .WithMany("Courses")
                        .HasForeignKey("GroupId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Subject", "Subject")
                        .WithMany("Courses")
                        .HasForeignKey("SubjectId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.CourseLesson", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Course", "Course")
                        .WithMany("CourseLessons")
                        .HasForeignKey("CourseId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Lesson", "Lesson")
                        .WithMany("CourseLessons")
                        .HasForeignKey("LessonId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.CourseLessonTest", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.CourseLesson", "CourseLesson")
                        .WithMany("CourseLessonTests")
                        .HasForeignKey("CourseLessonId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Test", "Test")
                        .WithMany("CourseLessonTests")
                        .HasForeignKey("TestId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.CourseRating", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Course", "Course")
                        .WithMany("CourseRatings")
                        .HasForeignKey("CourseId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Student", "Student")
                        .WithMany("CourseRatings")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.ExamTest", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.CourseLessonTest", "CourseLessonTest")
                        .WithMany("ExamTests")
                        .HasForeignKey("CourseLessonTestId");

                    b.HasOne("Abituriyent.Info.Core.Domain.GroupExam", "GroupExam")
                        .WithMany("ExamTests")
                        .HasForeignKey("GroupExamId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.GroupExam", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Exam", "Exam")
                        .WithMany("GroupExams")
                        .HasForeignKey("ExamId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Group", "Group")
                        .WithMany("GroupExams")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.QuizTest", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.CourseLesson", "CourseLesson")
                        .WithMany("QuizLessons")
                        .HasForeignKey("CourseLessonId");

                    b.HasOne("Abituriyent.Info.Core.Domain.CourseLessonTest", "CourseLessonTest")
                        .WithMany("QuizTests")
                        .HasForeignKey("CourseLessonTestId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Lesson", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Subject", "Subject")
                        .WithMany("Lessons")
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.News", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Admin", "Admin")
                        .WithMany("Newses")
                        .HasForeignKey("AdminId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.School", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.City", "City")
                        .WithMany("Schools")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.Student", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Person", "Person")
                        .WithOne("Student")
                        .HasForeignKey("Abituriyent.Info.Core.Domain.Student", "PersonId");

                    b.HasOne("Abituriyent.Info.Core.Domain.School", "School")
                        .WithMany("Students")
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.StudentExam", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.GroupExam", "GroupExam")
                        .WithMany("StudentExams")
                        .HasForeignKey("GroupExamId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Student", "Student")
                        .WithMany("StudentExams")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.StudentExamOpenTestAnswer", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.ExamTest", "ExamTest")
                        .WithMany("StudentExamOpenTestAnswers")
                        .HasForeignKey("ExamTestId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Student", "Student")
                        .WithMany("StudentExamOpenTestAnswers")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.StudentExamTestAnswer", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Answer", "Answer")
                        .WithMany("StudentExamTestAnswers")
                        .HasForeignKey("AnswerId");

                    b.HasOne("Abituriyent.Info.Core.Domain.ExamTest", "ExamTest")
                        .WithMany("StudentExamTestAnswers")
                        .HasForeignKey("ExamTestId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Student", "Student")
                        .WithMany("StudentExamTestAnswers")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.StudentLesson", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.CourseLesson", "CourseLesson")
                        .WithMany("StudentLessons")
                        .HasForeignKey("CourseLessonId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Student", "Student")
                        .WithMany("StudentLessons")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.StudentLessonOpenTestAnswer", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Student", "Student")
                        .WithMany("StudentLessonOpenTestAnswers")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.StudentLessonTestAnswer", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Answer", "Answer")
                        .WithMany("StudentLessonTestAnswers")
                        .HasForeignKey("AnswerId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Student", "Student")
                        .WithMany("StudentLessonTestAnswers")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("Abituriyent.Info.Core.Domain.TestAnswer", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.Answer", "Answer")
                        .WithOne("TestAnswer")
                        .HasForeignKey("Abituriyent.Info.Core.Domain.TestAnswer", "AnswerId");

                    b.HasOne("Abituriyent.Info.Core.Domain.Test", "Test")
                        .WithOne("TestAnswer")
                        .HasForeignKey("Abituriyent.Info.Core.Domain.TestAnswer", "TestId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Abituriyent.Info.Core.Domain.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Abituriyent.Info.Core.Domain.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
