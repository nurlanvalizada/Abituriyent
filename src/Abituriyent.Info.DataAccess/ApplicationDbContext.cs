using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.DataAccess.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Abituriyent.Info.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) {}

        public DbSet<City> Cities { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamTest> ExamTests { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<TestAnswer> TestAnswers { get; set; }
        public DbSet<CourseRating> CourseRatings { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<StudentLesson> StudentLessons { get; set; }
        public DbSet<StudentLessonTestAnswer> StudentLessonTestAnswers { get; set; }
        public DbSet<StudentExamTestAnswer> StudentExamTestAnswers { get; set; }
        public DbSet<StudentLessonOpenTestAnswer> StudentLessonOpenTestAnswers { get; set; }
        public DbSet<StudentExamOpenTestAnswer> StudentExamOpenTestAnswers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<News> Newses { get; set; }
 		public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; }
        public DbSet<CourseLessonTest> CourseLessonTests { get; set; }
        public DbSet<HappyStudent> HappyStudents { get; set; }
        public DbSet<QuizTest> QuizTests { get; set; }
        public DbSet<DataDictionary> DataDictionaries { get; set; }
        public DbSet<GroupExam> GroupExams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CityMapping(modelBuilder.Entity<City>());
            new SchoolMapping(modelBuilder.Entity<School>());
            new GroupMapping(modelBuilder.Entity<Group>());
            new CourseMapping(modelBuilder.Entity<Course>());
            new LessonMapping(modelBuilder.Entity<Lesson>());
            new ExamMapping(modelBuilder.Entity<Exam>());
            new ExamTestMapping(modelBuilder.Entity<ExamTest>());
            new TestMapping(modelBuilder.Entity<Test>());
            new AnswerMapping(modelBuilder.Entity<Answer>());
            new TestAnswerMapping(modelBuilder.Entity<TestAnswer>());
            new CourseRatingMapping(modelBuilder.Entity<CourseRating>());
            new StudentMapping(modelBuilder.Entity<Student>());
            new StudentExamMapping(modelBuilder.Entity<StudentExam>());
            new StudentLessonMapping(modelBuilder.Entity<StudentLesson>());
            new StudentLessonTestAnswerMapping(modelBuilder.Entity<StudentLessonTestAnswer>());
            new StudentLessonOpenTestAnswerMapping(modelBuilder.Entity<StudentLessonOpenTestAnswer>());
            new StudentExamTestAnswerMapping(modelBuilder.Entity<StudentExamTestAnswer>());
            new StudentExamOpenTestAnswerMapping(modelBuilder.Entity<StudentExamOpenTestAnswer>());
            new PersonMapping(modelBuilder.Entity<Person>());
            new AdminMapping(modelBuilder.Entity<Admin>());
            new TeacherMapping(modelBuilder.Entity<Teacher>());
            new NewsMapping(modelBuilder.Entity<News>());
			new ContactMessageMapping(modelBuilder.Entity<ContactMessage>());
            new SubjectMapping(modelBuilder.Entity<Subject>());
            new CourseLessonMapping(modelBuilder.Entity<CourseLesson>());
            new CourseLessonTestMapping(modelBuilder.Entity<CourseLessonTest>());
            new HappyStudentMapping(modelBuilder.Entity<HappyStudent>());
            new QuizTestMapping(modelBuilder.Entity<QuizTest>());
            new DataDictionaryMapping(modelBuilder.Entity<DataDictionary>());
            new GroupExamMapping(modelBuilder.Entity<GroupExam>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
