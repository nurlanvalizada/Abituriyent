using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Abituriyent.Info.DataAccess
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            await context.Database.EnsureCreatedAsync();

            // Look for any groups in table.
            if (context.Groups.Any()) return; // DB has already been seeded

            #region adding SuperAdmin User

            await roleManager.CreateAsync(new IdentityRole {Name = "SuperAdmin"});
            await roleManager.CreateAsync(new IdentityRole {Name = "Admin"});

            #endregion

            // Seed database
            var groups = new List<Group>
            {
                new Group {Name = "Qrup 1"},
                new Group {Name = "Qrup 2"},
                new Group {Name = "Qrup 3"},
                new Group {Name = "Qrup 4"}
            };
            context.Groups.AddRange(groups);
            await context.SaveChangesAsync();

            var subjects = new List<Subject>
            {
                new Subject {Name = "Ana dili", Status = true},
                new Subject {Name = "Riyaziyyat", Status = true},
                new Subject {Name = "Fizika", Status = true},
                new Subject {Name = "Kimya", Status = true},
                new Subject {Name = "Xarici dil", Status = true},
                new Subject {Name = "Coğrafiya", Status = true},
                new Subject {Name = "Azərbaycan tarixi", Status = true},
                new Subject {Name = "Ədəbiyyat", Status = true},
                new Subject {Name = "Biologiya", Status = true},
                new Subject {Name = "Tarix", Status = true}
            };
            context.Subjects.AddRange(subjects);
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 1, SubjectId = 1, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 1, SubjectId = 2, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course { GroupId = 1, SubjectId = 3, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course { GroupId = 1, SubjectId = 4, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 1, SubjectId = 5, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 2, SubjectId = 1, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 2, SubjectId = 2, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 2, SubjectId = 6, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 2, SubjectId = 7, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course { GroupId = 2, SubjectId = 5, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 3, SubjectId = 1, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 3, SubjectId = 2, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 3, SubjectId = 10, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course { GroupId = 3, SubjectId = 8, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 3, SubjectId = 5, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 4, SubjectId = 1, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 4, SubjectId = 2, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 4, SubjectId = 3, Status = true });
            await context.SaveChangesAsync();

            context.Courses.Add(new Course { GroupId = 4, SubjectId = 4, Status = true});
            await context.SaveChangesAsync();

            context.Courses.Add(new Course {  GroupId = 4, SubjectId = 9, Status = true });
            await context.SaveChangesAsync();

            var lessons = new List<Lesson>
            {
                new Lesson {Name = "Fonetika", SubjectId = 1, Status = true},
                new Lesson {Name = "Leksika", SubjectId = 1, Status = true},
                new Lesson {Name = "İsim", SubjectId = 1, Status = true},
                new Lesson {Name = "Frazeloji birləşmələr", SubjectId = 1, Status = true},
                new Lesson {Name = "Sifət", SubjectId = 1, Status = true},
                new Lesson {Name = "Natural Ədədlər", SubjectId = 2, Status = true},
                new Lesson {Name = "Kəsrlər", SubjectId = 2, Status = true},
                new Lesson {Name = "Triqonometriya", SubjectId = 2, Status = true},
                new Lesson {Name = "Törəmə", SubjectId = 2, Status = true},
                new Lesson {Name = "İnteqral", SubjectId = 2, Status = true},
                new Lesson {Name = "Mexanika", SubjectId = 3, Status = true},
                new Lesson {Name = "Termodinamika", SubjectId = 3, Status = true},
                new Lesson {Name = "Qüvvə", SubjectId = 3, Status = true},
                new Lesson {Name = "Təzyiq", SubjectId = 3, Status = true},
                new Lesson {Name = "Elektodinamika", SubjectId = 3, Status = true},
                new Lesson {Name = "Qeyri-üzvi Kimya", SubjectId = 4, Status = true},
                new Lesson {Name = "Mendeleyev Cədvəli", SubjectId = 4, Status = true},
                new Lesson {Name = "Reaksiyalar", SubjectId = 4, Status = true},
                new Lesson {Name = "Üzvi Kimya", SubjectId = 4, Status = true},
                new Lesson {Name = "Karbohidratlar", SubjectId = 4, Status = true},
                new Lesson {Name = "Noun", SubjectId = 5, Status = true},
                new Lesson {Name = "Adjective", SubjectId = 5, Status = true},
                new Lesson {Name = "Verb", SubjectId = 5, Status = true},
                new Lesson {Name = "Adverb", SubjectId = 5, Status = true},
                new Lesson {Name = "Prepositions", SubjectId = 5, Status = true},
                new Lesson {Name = "Coğrafiyanın tarixi", SubjectId = 6, Status = true},
                new Lesson {Name = "Yer quruluşu", SubjectId = 6, Status = true},
                new Lesson {Name = "Atmosfer", SubjectId = 6, Status = true},
                new Lesson {Name = "Azərbaycan Coğrafiyası", SubjectId = 6, Status = true},
                new Lesson {Name = "Qlobus", SubjectId = 6, Status = true},
                new Lesson {Name = "Manna dövləti", SubjectId = 7, Status = true},
                new Lesson {Name = "Atropatena", SubjectId = 7, Status = true},
                new Lesson {Name = "Səfəvilər dövləti", SubjectId = 7, Status = true},
                new Lesson {Name = "Xalq Cümhuriyyəti", SubjectId = 7, Status = true},
                new Lesson {Name = "Müasir Azərbaycan", SubjectId = 7, Status = true},
                new Lesson {Name = "Makedoniya", SubjectId = 10, Status = true},
                new Lesson {Name = "Sasanilər imperiyası", SubjectId = 10, Status = true},
                new Lesson {Name = "Roma imperiyası", SubjectId = 10, Status = true},
                new Lesson {Name = "Osmanlı imperiyası", SubjectId = 10, Status = true},
                new Lesson {Name = "Hindistan", SubjectId = 10, Status = true},
                new Lesson {Name = "Şeir", SubjectId = 8, Status = true},
                new Lesson {Name = "Qəzəllər", SubjectId = 8, Status = true},
                new Lesson {Name = "Nizami Gəncəvi", SubjectId = 8, Status = true},
                new Lesson {Name = "Məhəmməd Füzuli", SubjectId = 8, Status = true},
                new Lesson {Name = "Molla Pənah Vaqif", SubjectId = 8, Status = true},
                new Lesson {Name = "Botanika", SubjectId = 9, Status = true},
                new Lesson {Name = "Bitkilər", SubjectId = 9, Status = true},
                new Lesson {Name = "Heyvanlar aləmi", SubjectId = 9, Status = true},
                new Lesson {Name = "Amöb", SubjectId = 9, Status = true},
                new Lesson {Name = "İnsan anatomiyası", SubjectId = 9, Status = true}
            };
            context.Lessons.AddRange(lessons);
            await context.SaveChangesAsync();

            var courseLessons = new List<CourseLesson>
            {
                new CourseLesson {CourseId = 1, LessonId = 1, Status = true},
                new CourseLesson {CourseId = 4, LessonId = 1, Status = true},
                new CourseLesson {CourseId = 9, LessonId = 1, Status = true},
                new CourseLesson {CourseId = 14, LessonId = 1, Status = true},
                new CourseLesson {CourseId = 1, LessonId = 27, Status = true},
                new CourseLesson {CourseId = 4, LessonId = 27, Status = true},
                new CourseLesson {CourseId = 9, LessonId = 27, Status = true},
                new CourseLesson {CourseId = 14, LessonId = 27, Status = true},
                new CourseLesson {CourseId = 1, LessonId = 28, Status = true},
                new CourseLesson {CourseId = 4, LessonId = 28, Status = true},
                new CourseLesson {CourseId = 9, LessonId = 28, Status = true},
                new CourseLesson {CourseId = 14, LessonId = 28, Status = true},
                new CourseLesson {CourseId = 1, LessonId = 29, Status = true},
                new CourseLesson {CourseId = 4, LessonId = 29, Status = true},
                new CourseLesson {CourseId = 9, LessonId = 29, Status = true},
                new CourseLesson {CourseId = 14, LessonId = 29, Status = true},
                new CourseLesson {CourseId = 1, LessonId = 30, Status = true},
                new CourseLesson {CourseId = 4, LessonId = 30, Status = true},
                new CourseLesson {CourseId = 9, LessonId = 30, Status = true},
                new CourseLesson {CourseId = 14, LessonId = 30, Status = true},
                new CourseLesson {CourseId = 3, LessonId = 31, Status = true},
                new CourseLesson {CourseId = 8, LessonId = 31, Status = true},
                new CourseLesson {CourseId = 18, LessonId = 31, Status = true},
                new CourseLesson {CourseId = 13, LessonId = 31, Status = true},
                new CourseLesson {CourseId = 3, LessonId = 32, Status = true},
                new CourseLesson {CourseId = 8, LessonId = 32, Status = true},
                new CourseLesson {CourseId = 18, LessonId = 32, Status = true},
                new CourseLesson {CourseId = 13, LessonId = 32, Status = true},
                new CourseLesson {CourseId = 3, LessonId = 33, Status = true},
                new CourseLesson {CourseId = 8, LessonId = 33, Status = true},
                new CourseLesson {CourseId = 18, LessonId = 33, Status = true},
                new CourseLesson {CourseId = 13, LessonId = 33, Status = true},
                new CourseLesson {CourseId = 3, LessonId = 34, Status = true},
                new CourseLesson {CourseId = 8, LessonId = 34, Status = true},
                new CourseLesson {CourseId = 18, LessonId = 34, Status = true},
                new CourseLesson {CourseId = 13, LessonId = 34, Status = true},
                new CourseLesson {CourseId = 3, LessonId = 35, Status = true},
                new CourseLesson {CourseId = 8, LessonId = 35, Status = true},
                new CourseLesson {CourseId = 18, LessonId = 35, Status = true},
                new CourseLesson {CourseId = 13, LessonId = 35, Status = true},
                new CourseLesson {CourseId = 17, LessonId = 36, Status = true},
                new CourseLesson {CourseId = 20, LessonId = 38, Status = true},
                new CourseLesson {CourseId = 17, LessonId = 39, Status = true},
                new CourseLesson {CourseId = 20, LessonId = 40, Status = true},
                new CourseLesson {CourseId = 17, LessonId = 49, Status = true},
                new CourseLesson {CourseId = 20, LessonId = 49, Status = true},
                new CourseLesson {CourseId = 2, LessonId = 41, Status = true},
                new CourseLesson {CourseId = 16, LessonId = 41, Status = true},
                new CourseLesson {CourseId = 2, LessonId = 42, Status = true},
                new CourseLesson {CourseId = 16, LessonId = 42, Status = true},
                new CourseLesson {CourseId = 2, LessonId = 43, Status = true},
                new CourseLesson {CourseId = 16, LessonId = 43, Status = true},
                new CourseLesson {CourseId = 2, LessonId = 44, Status = true},
                new CourseLesson {CourseId = 16, LessonId = 44, Status = true},
                new CourseLesson {CourseId = 2, LessonId = 45, Status = true},
                new CourseLesson {CourseId = 16, LessonId = 45, Status = true},
                new CourseLesson {CourseId = 5, LessonId = 25, Status = true},
                new CourseLesson {CourseId = 10, LessonId = 25, Status = true},
                new CourseLesson {CourseId = 15, LessonId = 25, Status = true},
                new CourseLesson {CourseId = 5, LessonId = 26, Status = true},
                new CourseLesson {CourseId = 10, LessonId = 26, Status = true},
                new CourseLesson {CourseId = 15, LessonId = 26, Status = true},
                new CourseLesson {CourseId = 5, LessonId = 46, Status = true},
                new CourseLesson {CourseId = 10, LessonId = 46, Status = true},
                new CourseLesson {CourseId = 15, LessonId = 46, Status = true},
                new CourseLesson {CourseId = 5, LessonId = 47, Status = true},
                new CourseLesson {CourseId = 10, LessonId = 47, Status = true},
                new CourseLesson {CourseId = 15, LessonId = 47, Status = true},
                new CourseLesson {CourseId = 5, LessonId = 48, Status = true},
                new CourseLesson {CourseId = 10, LessonId = 48, Status = true},
                new CourseLesson {CourseId = 15, LessonId = 48, Status = true},
                new CourseLesson {CourseId = 12, LessonId = 2, Status = true},
                new CourseLesson {CourseId = 12, LessonId = 3, Status = true},
                new CourseLesson {CourseId = 12, LessonId = 4, Status = true},
                new CourseLesson {CourseId = 12, LessonId = 23, Status = true},
                new CourseLesson {CourseId = 12, LessonId = 24, Status = true},
                new CourseLesson {CourseId = 11, LessonId = 5, Status = true},
                new CourseLesson {CourseId = 11, LessonId = 6, Status = true},
                new CourseLesson {CourseId = 11, LessonId = 7, Status = true},
                new CourseLesson {CourseId = 11, LessonId = 8, Status = true},
                new CourseLesson {CourseId = 11, LessonId = 9, Status = true},
                new CourseLesson {CourseId = 6, LessonId = 15, Status = true},
                new CourseLesson {CourseId = 6, LessonId = 16, Status = true},
                new CourseLesson {CourseId = 6, LessonId = 17, Status = true},
                new CourseLesson {CourseId = 6, LessonId = 18, Status = true},
                new CourseLesson {CourseId = 6, LessonId = 19, Status = true},
                new CourseLesson {CourseId = 19, LessonId = 20, Status = true},
                new CourseLesson {CourseId = 19, LessonId = 21, Status = true},
                new CourseLesson {CourseId = 19, LessonId = 22, Status = true},
                new CourseLesson {CourseId = 19, LessonId = 37, Status = true},
                new CourseLesson {CourseId = 19, LessonId = 50, Status = true},
                new CourseLesson {CourseId = 7, LessonId = 10, Status = true},
                new CourseLesson {CourseId = 7, LessonId = 11, Status = true},
                new CourseLesson {CourseId = 7, LessonId = 12, Status = true},
                new CourseLesson {CourseId = 7, LessonId = 13, Status = true},
                new CourseLesson {CourseId = 7, LessonId = 14, Status = true},
            };
            context.CourseLessons.AddRange(courseLessons);
            await context.SaveChangesAsync();

            var tests = new List<Test>
            {
                new Test {Content = "Test1", Status = true},
                new Test {Content = "Test2", Status = true},
                new Test {Content = "Test3", Status = true},
                new Test {Content = "Test4", Status = true},
                new Test {Content = "Test5", Status = true}
            };
            context.Tests.AddRange(tests);
            await context.SaveChangesAsync();

            var courseLessonTests = new List<CourseLessonTest>
            {
                new CourseLessonTest {CourseLessonId = 1, TestId = 1, Status = true},
                new CourseLessonTest {CourseLessonId = 70, TestId = 1, Status = true},
                new CourseLessonTest {CourseLessonId = 2, TestId = 2, Status = true},
                new CourseLessonTest {CourseLessonId = 1, TestId = 3, Status = true},
                new CourseLessonTest {CourseLessonId = 70, TestId = 4, Status = true},
                new CourseLessonTest {CourseLessonId = 1, TestId = 5, Status = true}
            };
            context.CourseLessonTests.AddRange(courseLessonTests);
            await context.SaveChangesAsync();

            var lessonTests = new List<QuizTest>
            {
                new QuizTest {CourseLessonId = 1,CourseLessonTestId = 1},
                new QuizTest {CourseLessonId = 1,CourseLessonTestId = 6}
            };
            context.QuizTests.AddRange(lessonTests);
            await context.SaveChangesAsync();

            var answers = new List<Answer>
            {
                new Answer {TestId = 1, Text = "Cavab1", Status = true},
                new Answer {TestId = 1, Text = "Cavab2", Status = true},
                new Answer {TestId = 1, Text = "Cavab3", Status = true},
                new Answer {TestId = 1, Text = "Cavab4", Status = true},
                new Answer {TestId = 1, Text = "Cavab5", Status = true},
                new Answer {TestId = 2, Text = "Cavab1", Status = true},
                new Answer {TestId = 2, Text = "Cavab2", Status = true},
                new Answer {TestId = 2, Text = "Cavab3", Status = true},
                new Answer {TestId = 2, Text = "Cavab4", Status = true},
                new Answer {TestId = 2, Text = "Cavab5", Status = true},
                new Answer {TestId = 3, Text = "Cavab1", Status = true},
                new Answer {TestId = 3, Text = "Cavab2", Status = true},
                new Answer {TestId = 3, Text = "Cavab3", Status = true},
                new Answer {TestId = 3, Text = "Cavab4", Status = true},
                new Answer {TestId = 3, Text = "Cavab5", Status = true},
                new Answer {TestId = 4, Text = "Cavab1", Status = true},
                new Answer {TestId = 4, Text = "Cavab2", Status = true},
                new Answer {TestId = 4, Text = "Cavab3", Status = true},
                new Answer {TestId = 4, Text = "Cavab4", Status = true},
                new Answer {TestId = 4, Text = "Cavab5", Status = true},
                new Answer {TestId = 5, Text = "Cavab1", Status = true},
                new Answer {TestId = 5, Text = "Cavab2", Status = true},
                new Answer {TestId = 5, Text = "Cavab3", Status = true},
                new Answer {TestId = 5, Text = "Cavab4", Status = true},
                new Answer {TestId = 5, Text = "Cavab5", Status = true},
            };
            context.Answers.AddRange(answers);
            await context.SaveChangesAsync();

            var testAnswers = new List<TestAnswer>
            {
                new TestAnswer {TestId = 1, AnswerId = 1},
                new TestAnswer {TestId = 2, AnswerId = 15},
                new TestAnswer {TestId = 3, AnswerId = 24},
                new TestAnswer {TestId = 4, AnswerId = 6},
                new TestAnswer {TestId = 5, AnswerId = 12},
            };
            context.TestAnswers.AddRange(testAnswers);
            await context.SaveChangesAsync();

            var exams = new List<Exam>
            {
                new Exam
                {
                    Name = "Mövzu sınaq imtahanı 1",
                    ExamType = ExamType.Subject,
                    Date = DateTime.Today.AddDays(3),
                    StartTime = TimeSpan.Parse("11:00:00"),
                    EndTime = TimeSpan.Parse("14:00:00")
                },
                new Exam
                {
                    Name = "Mövzu sınaq imtahanı 2",
                    ExamType = ExamType.Subject,
                    Date = DateTime.Today.AddDays(33),
                    StartTime = TimeSpan.Parse("11:00:00"),
                    EndTime = TimeSpan.Parse("14:00:00")
                },
                new Exam
                {
                    Name = "Ümumi sınaq imtahanı 1",
                    ExamType = ExamType.General,
                    Date = DateTime.Today.AddDays(3),
                    StartTime = TimeSpan.Parse("11:00:00"),
                    EndTime = TimeSpan.Parse("14:00:00")
                }
            };
            context.Exams.AddRange(exams);
            await context.SaveChangesAsync();

            var groupExams = new List<GroupExam>
            {
                new GroupExam
                {
                    ExamId=1,
                    GroupId=1
                },
                new GroupExam
                {
                    ExamId=1,
                    GroupId=2
                },
                new GroupExam
                {
                    ExamId=1,
                    GroupId=3
                },
                new GroupExam
                {
                    ExamId=1,
                    GroupId=4
                },
                 new GroupExam
                {
                    ExamId=2,
                    GroupId=1
                },
                new GroupExam
                {
                    ExamId=2,
                    GroupId=2
                },
                new GroupExam
                {
                    ExamId=2,
                    GroupId=3
                },
                new GroupExam
                {
                    ExamId=2,
                    GroupId=4
                },
                 new GroupExam
                {
                    ExamId=3,
                    GroupId=1
                },
                new GroupExam
                {
                    ExamId=3,
                    GroupId=2
                },
                new GroupExam
                {
                    ExamId=3,
                    GroupId=3
                },
                new GroupExam
                {
                    ExamId=3,
                    GroupId=4
                }
            };
            context.GroupExams.AddRange(groupExams);
            await context.SaveChangesAsync();

            var examTests = new List<ExamTest>
            {
                new ExamTest
                {
                    GroupExamId = 1,
                    CourseLessonTestId = 1
                },
                new ExamTest
                {
                    GroupExamId = 1,
                    CourseLessonTestId = 3
                },
                new ExamTest
                {
                    GroupExamId = 1,
                    CourseLessonTestId = 4
                },
                new ExamTest
                {
                    GroupExamId = 1,
                    CourseLessonTestId = 6
                }
            };
            context.ExamTests.AddRange(examTests);
            await context.SaveChangesAsync();

            var teachers = new List<Teacher>
            {
                new Teacher
                {
                    FullName = "Anar Nurəliyev",
                    Profession = "Məntiq müəllimi",
                    About = "Anar müəllim təcrübəli məntiq müəllimidir"
                },
                new Teacher
                {
                    FullName = "Elçin Tanriverdiyev",
                    Profession = "Riyaziyyat müəllimi",
                    About = "Elçin müəllim təcrübəli riyaziyyat müəllimidir"
                },
                new Teacher
                {
                    FullName = "Məleykə Rzazadə",
                    Profession = "Fizika müəlliməsi",
                    About = "Məleykə müəllimə təcrübəli fizika müəlliməsidir"
                },
                new Teacher
                {
                    FullName = "Ceyhun Kərimov",
                    Profession = "İngilis dili müəllimi",
                    About = "Ceyhun müəllim təcrübəli ingilis dili müəllimidir"
                },
            };
            context.Teachers.AddRange(teachers);
            await context.SaveChangesAsync();
        }
    }
}