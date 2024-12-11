using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Abituriyent.Info.Repository.UOW;
using Abituriyent.Info.Web.Models.ViewModels;
using Abituriyent.Info.Web.MyExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Abituriyent.Info.Web.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseController(IUnitOfWorkManager unitOfWorkManager, IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        //
        // GET: Course/Index
        public ActionResult Index()
        {
            var student = this.GetStudent(_userManager, _unitOfWorkManager.StudentRepository);
            var courses = _unitOfWorkManager.CourseRepository.FindBy(c => c.GroupId == student.GroupId, c=>c.Subject).OrderBy(c=>c.Id).ToList();
            var courseViewModelsList = (from course in courses
                                        let rateStars =
                                        _unitOfWorkManager.Repository<CourseRating>().FindBy(cr => cr.CourseId == course.Id).Select(cr => cr.Stars).ToList()
                                        select new CourseViewModel
                                        {
                                            Id = course.Id,
                                            Name = course.Subject.Name,
                                            ImageUrl = course.ImageUrl,
                                            StudentCount = _unitOfWorkManager.CourseRepository.GetActiveStudentsCount(course.Id),
                                            AverageRating = rateStars.Count > 0 ? (int)rateStars.Average(rs => rs) : 0
                                        }).ToList();
            return View(courseViewModelsList);
        }

        //
        // GET: Course/Single/5
        public async Task<ActionResult> Single(int id)
        {
            var student = this.GetStudent(_userManager, _unitOfWorkManager.StudentRepository);

            var course = _unitOfWorkManager.CourseRepository.GetSingle(c => c.Id == id && c.GroupId == student.GroupId, c => c.Group, c => c.Teacher, c=>c.Subject);
            if (course == null) return RedirectToAction("Index");

            course.CourseLessons = await _unitOfWorkManager.Repository<CourseLesson>().FindBy(cl => cl.CourseId == course.Id, cl=>cl.Course).ToListAsync();

            var courseLessons = new List<CourseLessonViewModel>();
            foreach (var courseLesson in course.CourseLessons)
            {
                var lesson = _unitOfWorkManager.LessonRepository.GetSingleWithoutPdf(courseLesson.LessonId);
                var courseLessonViewModel = new CourseLessonViewModel
                {
                    Id = courseLesson.Id,
                    Name = lesson.Name,
                    CourseId = courseLesson.CourseId,
                    VideoUrl = lesson.VideoUrl
                };

                var studentLesson = _unitOfWorkManager.StudentLessonRepository.GetSingle(sl => (sl.StudentId == student.Id) && (sl.CourseLessonId == courseLesson.Id));
                if (studentLesson == null) courseLessonViewModel.Status = LessonStatus.NotStarted;
                else
                    courseLessonViewModel.Status = studentLesson.EndDate < DateTime.Now
                        ? LessonStatus.Complete
                        : LessonStatus.InProgress;

                courseLessons.Add(courseLessonViewModel);
            }

            var courseStatus = courseLessons.All(l => l.Status == LessonStatus.Complete)
                ? LessonStatus.Complete
                : courseLessons.All(l => l.Status == LessonStatus.NotStarted)
                    ? LessonStatus.NotStarted
                    : LessonStatus.InProgress;

            var courseViewModel = new CourseViewModel
            {
                Id = course.Id,
                Name = course.Subject.Name,
                Group = course.Group,
                Lessons = courseLessons,
                Student = student,
                Teacher = course.Teacher,
                Description = System.Net.WebUtility.HtmlDecode(course.Description),
                CourseRatings = _unitOfWorkManager.Repository<CourseRating>().FindBy(cr => cr.CourseId == course.Id, cr=>cr.Student.Person).ToList(),
                StudentCount = _unitOfWorkManager.CourseRepository.GetActiveStudentsCount(course.Id),
                CourseStatus = courseStatus
            };
            return View(courseViewModel);
        }

        //
        // GET: Course/TakeLesson/5
        public ActionResult TakeLesson(int id)
        {
            var student =  this.GetStudent(_userManager, _unitOfWorkManager.StudentRepository);
            var courseLesson = _unitOfWorkManager.Repository<CourseLesson>().GetSingle(cl => cl.Id == id && cl.Course.GroupId == student.GroupId);
            if (courseLesson == null) return RedirectToAction("Index");

            var lesson = _unitOfWorkManager.LessonRepository.GetSingleWithoutPdf(courseLesson.LessonId);
            if (!_unitOfWorkManager.Repository<StudentLesson>().Any(sl => (sl.StudentId == student.Id) && (sl.CourseLessonId == id)))
            {
                var studentLesson = new StudentLesson
                {
                    StudentId = student.Id,
                    CourseLessonId = courseLesson.Id,
                    StartDate = DateTime.Now,
                };
                _unitOfWorkManager.Repository<StudentLesson>().Add(studentLesson);
                _unitOfWorkManager.Commit();
            }

            var tests = _unitOfWorkManager.Repository<QuizTest>().FindBy(qt => qt.CourseLessonId == courseLesson.Id, qt => qt.CourseLessonTest.Test.Answers)
                .Select(qt => new TestViewModel
                {
                    Id = qt.CourseLessonTest.Test.Id,
                    Content = qt.CourseLessonTest.Test.Content,
                    TestType = qt.CourseLessonTest.Test.TestType,
                    Answers = qt.CourseLessonTest.Test.Answers.Select(a => new AnswerViewModel
                    {
                        Id = a.Id,
                        Text = a.Text
                    }).ToList()
                }).ToList();

            var courseLessonViewModel = new CourseLessonViewModel
            {
                Id = courseLesson.Id,
                Name = lesson.Name,
                CourseId = courseLesson.CourseId,
                VideoUrl = lesson.VideoUrl,
                Tests = tests
            };
            return View(courseLessonViewModel);
        }

        //
        // GET: Course/ReadPdf/5
        [HttpGet]
        public ActionResult ReadPdf(int id)
        {
            return this.ReadLessonPdf(id, _unitOfWorkManager.LessonRepository);
        }

        //
        // GET: Course/ViewStudentImage/5
        [HttpGet]
        public ActionResult ViewStudentImage(int id)
        {
            return this.ViewStudentImage(id, _unitOfWorkManager.StudentRepository, _hostingEnvironment.WebRootPath);
        }
    }
}