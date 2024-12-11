using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Abituriyent.Info.Web.Models.AdminViewModels;
using System.Collections.Generic;
using System.IO;
using Abituriyent.Info.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Abituriyent.Info.Core.Models;
using Abituriyent.Info.Repository.UOW;
using Abituriyent.Info.Web.Models.ViewModels;
using Abituriyent.Info.Web.MyExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExamTestViewModel = Abituriyent.Info.Web.Models.AdminViewModels.ExamTestViewModel;
using TestViewModel = Abituriyent.Info.Web.Models.AdminViewModels.TestViewModel;

namespace Abituriyent.Info.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AdminController(UserManager<ApplicationUser> userManager, IUnitOfWorkManager unitOfWorkManager, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View("AdminIndex");
        }

        #region Group
        public async Task<IActionResult> GroupIndex()
        {
            return View("Group/GroupIndex", await _unitOfWorkManager.Repository<Group>().GetAll().ToListAsync());
        }

        public IActionResult GroupCreate()
        {
            return View("Group/GroupCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GroupCreate(Group group)
        {
            if (ModelState.IsValid)
            {
                _unitOfWorkManager.Repository<Group>().Add(group);
                _unitOfWorkManager.Commit();
                return RedirectToAction("GroupIndex");
            }
            return View("Group/GroupCreate", group);
        }

        public IActionResult GroupEdit(int? id)
        {
            if (id == null) return NotFound();
            var group = _unitOfWorkManager.Repository<Group>().GetSingle(g => g.Id == id.Value);
            if (group == null) return NotFound();
            return View("Group/GroupEdit", group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GroupEdit(int id, Group group)
        {
            if (id != group.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _unitOfWorkManager.Repository<Group>().Update(group);
                _unitOfWorkManager.Commit();
                return RedirectToAction("GroupIndex");
            }
            return View("Group/GroupEdit", group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GroupDelete(int id)
        {
            var group = _unitOfWorkManager.Repository<Group>().GetSingle(g => g.Id == id);
            _unitOfWorkManager.Repository<Group>().Delete(group);
            _unitOfWorkManager.Commit();
            return RedirectToAction("GroupIndex");
        }
        #endregion

        #region Course
        public async Task<IActionResult> CourseIndex(int groupId)
        {
            return View("Course/CourseIndex",
                await
                    _unitOfWorkManager.CourseRepository.FindBy(c => (c.GroupId == groupId) && c.Subject.Status, c => c.Subject)
                        .ToListAsync());
        }

        public IActionResult CourseDetails(int? id)
        {
            if (id == null) return NotFound();
            var course = _unitOfWorkManager.Repository<Course>().GetSingle(c => c.Id == id.Value, c => c.Subject, c => c.Teacher);
            if (course == null) return NotFound();
            return View("Course/CourseDetails", course);
        }

        public IActionResult CourseCreate(int groupId)
        {
            var cs = new CourseViewModel();
            var courses = _unitOfWorkManager.Repository<Course>().FindBy(c => c.GroupId == groupId, c => c.Subject).ToList();
            var subjects = _unitOfWorkManager.Repository<Subject>().GetAll().ToList();
            foreach (var course in courses)
            {
                subjects.Remove(course.Subject);
            }
            cs.Course = new Course { GroupId = groupId };
            cs.Subjects = new SelectList(subjects.Select(s => new { s.Id, s.Name }), "Id", "Name");
            cs.Teachers = new SelectList(_unitOfWorkManager.Repository<Teacher>().GetAll().Select(t => new { t.Id, t.FullName }), "Id", "FullName");

            return View("Course/CourseCreate", cs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CourseCreate(CourseViewModel courseViewModel, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile?.Length > 0)
                {
                    var imageUrl = "/images/courses/" + imageFile.FileName;
                    using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + imageUrl, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    courseViewModel.Course.ImageUrl = imageUrl;
                }
                courseViewModel.Course.Status = true;
                _unitOfWorkManager.Repository<Course>().Add(courseViewModel.Course);
                _unitOfWorkManager.Commit();
                return RedirectToAction("CourseIndex", new { groupId = courseViewModel.Course.GroupId });
            }
            return View("Course/CourseCreate", courseViewModel);
        }

        public IActionResult CourseEdit(int? id)
        {
            if (id == null) return NotFound();
            var cvm = new CourseViewModel
            {
                Course = _unitOfWorkManager.Repository<Course>().GetSingle(c => c.Id == id.Value, c => c.Subject),
                Teachers = new SelectList(_unitOfWorkManager.Repository<Teacher>().GetAll().ToList().Select(t => new { t.Id, t.FullName }), "Id", "FullName")
            };
            if (cvm.Course == null) return NotFound();
            return View("Course/CourseEdit", cvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CourseEdit(int id, CourseViewModel courseViewModel, IFormFile imageFile)
        {
            if (id != courseViewModel.Course.Id) return NotFound();
            if (ModelState.IsValid)
            {
                if (imageFile?.Length > 0)
                {
                    var imageUrl = "/images/courses/" + imageFile.FileName;
                    using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + imageUrl, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    courseViewModel.Course.ImageUrl = imageUrl;
                }
                courseViewModel.Course.Status = true;
                _unitOfWorkManager.Repository<Course>().Update(courseViewModel.Course);
                _unitOfWorkManager.Commit();
                return RedirectToAction("CourseIndex", new { groupId = courseViewModel.Course.GroupId });
            }
            return View("Course/CourseEdit", courseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CourseDelete(int id)
        {
            var course = _unitOfWorkManager.Repository<Course>().GetSingle(c => c.Id == id);
            _unitOfWorkManager.Repository<Course>().Delete(course);
            _unitOfWorkManager.Commit();
            return RedirectToAction("CourseIndex", new { groupId = course.GroupId });
        }
        #endregion

        #region Subject
        public async Task<IActionResult> SubjectIndex()
        {
            var subjects = await _unitOfWorkManager.Repository<Subject>().GetAll().Where(s => s.Status).ToListAsync();
            foreach (var subject in subjects)
            {
                subject.Courses =
                    await _unitOfWorkManager.CourseRepository.FindBy(c => (c.SubjectId == subject.Id) && c.Status, c => c.Group).ToListAsync();
            }
            return View("Subject/SubjectIndex", subjects);
        }

        public IActionResult SubjectCreate()
        {
            var cs = new CourseSubjectViewModel
            {
                Groups = _unitOfWorkManager.Repository<Group>().GetAll().ToList(),
                GroupIds = new List<int>()
            };
            return View("Subject/SubjectCreate", cs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubjectCreate(CourseSubjectViewModel courseSubjectViewModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWorkManager.Repository<Subject>().Add(courseSubjectViewModel.Subject);
                foreach (var id in courseSubjectViewModel.GroupIds)
                {
                    _unitOfWorkManager.Repository<Course>().Add(new Course { SubjectId = courseSubjectViewModel.Subject.Id, GroupId = id, Status = true });
                }
                _unitOfWorkManager.Commit();
                return RedirectToAction("SubjectIndex");
            }
            return View("Subject/SubjectCreate", courseSubjectViewModel);
        }

        public IActionResult SubjectEdit(int? id)
        {
            if (id == null) return NotFound();
            var cs = new CourseSubjectViewModel { Groups = _unitOfWorkManager.Repository<Group>().GetAll().ToList() };
            var courses = _unitOfWorkManager.Repository<Course>().FindBy(c => (c.SubjectId == id.Value) && c.Status).ToList();
            cs.GroupIds = courses.Select(c => c.GroupId).ToList();
            var subject = _unitOfWorkManager.Repository<Subject>().GetSingle(s => s.Id == id.Value);
            if (subject == null)
            {
                return NotFound();
            }
            cs.Subject = subject;
            return View("Subject/SubjectEdit", cs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubjectEdit(int id, CourseSubjectViewModel courseSubjectViewModel)
        {
            if (id != courseSubjectViewModel.Subject.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var courses = _unitOfWorkManager.Repository<Course>().FindBy(c => (c.SubjectId == id) && c.Status).ToList();
                var groupIds = courses.Select(c => c.GroupId).ToList();
                if (!groupIds.OrderBy(g => g).SequenceEqual(courseSubjectViewModel.GroupIds.OrderBy(g => g)))
                {
                    var deleteGroupIds = new List<int>(groupIds);
                    var addGroupIds = new List<int>(courseSubjectViewModel.GroupIds);
                    deleteGroupIds.RemoveAll(i => courseSubjectViewModel.GroupIds.Contains(i));
                    foreach (var did in deleteGroupIds)
                    {
                        var course =
                            _unitOfWorkManager.Repository<Course>().FindBy(
                                    c => (c.SubjectId == courseSubjectViewModel.Subject.Id) && (c.GroupId == did))
                                .FirstOrDefault();
                        course.Status = false;
                        _unitOfWorkManager.Repository<Course>().Update(course);
                    }
                    addGroupIds.RemoveAll(i => groupIds.Contains(i));
                    foreach (var aid in addGroupIds)
                    {
                        _unitOfWorkManager.Repository<Course>().Add(new Course
                        {
                            SubjectId = courseSubjectViewModel.Subject.Id,
                            GroupId = aid,
                            Status = true
                        });
                    }
                }
                _unitOfWorkManager.Repository<Subject>().Update(courseSubjectViewModel.Subject);
                _unitOfWorkManager.Commit();

                return RedirectToAction("SubjectIndex");
            }
            return View("Subject/SubjectEdit", courseSubjectViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubjectDelete(int id)
        {
            var courses = _unitOfWorkManager.Repository<Course>().FindBy(c => (c.SubjectId == id) && c.Status).ToList();
            var subject = _unitOfWorkManager.Repository<Subject>().GetSingle(s => s.Id == id);
            subject.Status = false;
            _unitOfWorkManager.Repository<Subject>().Update(subject);
            foreach (var course in courses)
            {
                course.Status = false;
                _unitOfWorkManager.Repository<Course>().Update(course);
            }
            _unitOfWorkManager.Repository<Lesson>().FindBy(l => l.SubjectId == id).ToList().Select(l => { l.Status = false; return l; }).ToList().ForEach(l => { _unitOfWorkManager.Repository<Lesson>().Update(l); });
            _unitOfWorkManager.Commit();
            return RedirectToAction("SubjectIndex");
        }
        #endregion

        #region Lesson
        public async Task<IActionResult> LessonIndex(int subjectId)
        {
            return View("Lesson/LessonIndex",
                await _unitOfWorkManager.LessonRepository.
                FindBy(l => (l.SubjectId == subjectId) && l.Status)
                .Select(l => new Lesson
                {
                    Id = l.Id,
                    Name = l.Name,
                    SubjectId = l.SubjectId,
                    Status = l.Status,
                    VideoUrl = l.VideoUrl
                })
                .ToListAsync());
        }

        public IActionResult ReadPdf(int lessonId)
        {
            return this.ReadLessonPdf(lessonId, _unitOfWorkManager.LessonRepository);
        }

        public IActionResult LessonCreate(int subjectId)
        {
            var ls = new LessonViewModel
            {
                Groups = _unitOfWorkManager.Repository<Course>().FindBy(c => (c.SubjectId == subjectId) && c.Status)
                        .Select(c => c.Group)
                        .ToList(),
                GroupIds = new List<int>()
            };
            ViewBag.SubjectId = subjectId;
            return View("Lesson/LessonCreate", ls);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LessonCreate(IFormFile pdfFile, LessonViewModel lessonViewModel)
        {
            if (ModelState.IsValid)
            {
                if (pdfFile?.Length > 0)
                {
                    byte[] buffer;
                    using (var stream = pdfFile.OpenReadStream())
                    {
                        buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, (int)stream.Length);
                    }
                    lessonViewModel.Lesson.PdfContentType = pdfFile.ContentType;
                    lessonViewModel.Lesson.PdfFile = buffer;
                }
                lessonViewModel.Lesson.Status = true;
                _unitOfWorkManager.Repository<Lesson>().Add(lessonViewModel.Lesson);
                foreach (var id in lessonViewModel.GroupIds)
                {
                    var course = _unitOfWorkManager.Repository<Course>().GetSingle(
                                    c => (c.SubjectId == lessonViewModel.Lesson.SubjectId) && (c.GroupId == id) && c.Status);
                    _unitOfWorkManager.Repository<CourseLesson>().Add(new CourseLesson
                    {
                        LessonId = lessonViewModel.Lesson.Id,
                        CourseId = course.Id,
                        Status = true
                    });
                }
                _unitOfWorkManager.Commit();
                return RedirectToAction("LessonIndex", new { subjectId = lessonViewModel.Lesson.SubjectId });
            }
            return View("Lesson/LessonCreate", lessonViewModel);
        }

        public IActionResult LessonEdit(int? id)
        {
            if (id == null) return NotFound();
            var lessonViewModel = new LessonViewModel();
            var lesson = _unitOfWorkManager.Repository<Lesson>().GetSingle(l => l.Id == id.Value);
            if (lesson == null)
            {
                return NotFound();
            }
            lessonViewModel.Lesson = lesson;
            lessonViewModel.Groups = _unitOfWorkManager.Repository<Course>().FindBy(
                            c => (c.SubjectId == lessonViewModel.Lesson.SubjectId) && c.Status)
                        .Select(g => g.Group)
                        .ToList();
            var courses =
                _unitOfWorkManager.Repository<CourseLesson>().FindBy(c => (c.LessonId == id.Value) && c.Status, c => c.Course)
                    .Select(c => c.Course)
                    .Where(c => c.Status)
                    .ToList();
            lessonViewModel.GroupIds = courses.Select(c => c.GroupId).ToList();
            return View("Lesson/LessonEdit", lessonViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LessonEdit(int id, IFormFile pdfFile, LessonViewModel lessonViewModel)
        {
            if (id != lessonViewModel.Lesson.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var courses =
                    _unitOfWorkManager.Repository<CourseLesson>().FindBy(c => (c.LessonId == id) && c.Status, c => c.Course)
                        .Select(c => c.Course)
                        .Where(c => c.Status)
                        .ToList();
                var groupIds = courses.Select(c => c.GroupId).ToList();
                if (!groupIds.OrderBy(g => g).SequenceEqual(lessonViewModel.GroupIds.OrderBy(g => g)))
                {
                    var deleteGroupIds = new List<int>(groupIds);
                    var addGroupIds = new List<int>(lessonViewModel.GroupIds);
                    deleteGroupIds.RemoveAll(i => lessonViewModel.GroupIds.Contains(i));
                    foreach (var did in deleteGroupIds)
                    {
                        var course =
                            _unitOfWorkManager.Repository<Course>().FindBy(
                                c =>
                                    (c.SubjectId == lessonViewModel.Lesson.SubjectId) && (c.GroupId == did) &&
                                    c.Status).FirstOrDefault();
                        if (course != null)
                        {
                            var courseLesson =
                                _unitOfWorkManager.Repository<CourseLesson>().FindBy(
                                    c => (c.LessonId == lessonViewModel.Lesson.Id) && (c.CourseId == course.Id) &&
                                        c.Status).FirstOrDefault();
                            if (courseLesson != null)
                            {
                                courseLesson.Status = false;
                                _unitOfWorkManager.Repository<CourseLesson>().Update(courseLesson);

                                var courseLessonTests =
                                    _unitOfWorkManager.Repository<CourseLessonTest>().FindBy(
                                        clt => clt.CourseLessonId == courseLesson.Id).ToList();
                                if (courseLessonTests != null)
                                {
                                    foreach (var clt in courseLessonTests)
                                    {
                                        clt.Status = false;
                                        _unitOfWorkManager.Repository<CourseLessonTest>().Update(clt);
                                    }
                                }
                            }
                        }
                    }
                    addGroupIds.RemoveAll(i => groupIds.Contains(i));
                    foreach (var aid in addGroupIds)
                    {
                        var course =
                            _unitOfWorkManager.Repository<Course>().FindBy(
                                c => (c.SubjectId == lessonViewModel.Lesson.SubjectId) && (c.GroupId == aid) &&
                                    c.Status).FirstOrDefault();
                        _unitOfWorkManager.Repository<CourseLesson>().Add(new CourseLesson
                        {
                            LessonId = lessonViewModel.Lesson.Id,
                            CourseId = course.Id,
                            Status = true
                        });
                    }
                }
                if (pdfFile?.Length > 0)
                {
                    byte[] buffer;
                    using (var stream = pdfFile.OpenReadStream())
                    {
                        buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, (int)stream.Length);
                    }
                    lessonViewModel.Lesson.PdfContentType = pdfFile.ContentType;
                    lessonViewModel.Lesson.PdfFile = buffer;
                    _unitOfWorkManager.Repository<Lesson>().Update(lessonViewModel.Lesson);
                }
                else
                {
                    _unitOfWorkManager.LessonRepository.UpdateWithoutPdfFile(lessonViewModel.Lesson);
                }
                _unitOfWorkManager.Commit();
                return RedirectToAction("LessonIndex", new { subjectId = lessonViewModel.Lesson.SubjectId });
            }
            return View("Lesson/LessonEdit", lessonViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LessonDelete(int id)
        {
            var courseLessons = _unitOfWorkManager.Repository<CourseLesson>().FindBy(c => (c.LessonId == id) && c.Status).ToList();
            var lesson = _unitOfWorkManager.Repository<Lesson>().GetSingle(s => s.Id == id);
            lesson.Status = false;
            _unitOfWorkManager.Repository<Lesson>().Update(lesson);
            foreach (var courseLesson in courseLessons)
            {
                courseLesson.Status = false;
                _unitOfWorkManager.Repository<CourseLesson>().Update(courseLesson);
                var courseLessonTests =
                    _unitOfWorkManager.Repository<CourseLessonTest>().FindBy(
                        clt => (clt.CourseLessonId == courseLesson.Id) && clt.Status).ToList();
                foreach (var courseLessonTest in courseLessonTests)
                {
                    courseLessonTest.Status = false;
                    _unitOfWorkManager.Repository<CourseLessonTest>().Update(courseLessonTest);
                }
            }
            _unitOfWorkManager.Commit();
            return RedirectToAction("LessonIndex", new { subjectId = lesson.SubjectId });
        }
        #endregion

        #region News
        public async Task<IActionResult> NewsIndex()
        {
            return View("News/NewsIndex", await _unitOfWorkManager.NewsRepository.FindNewsWithoutFullContent(n=>true).ToListAsync());
        }

        public IActionResult NewsDetails(int? id)
        {
            if (id == null) return NotFound();
            var news = _unitOfWorkManager.Repository<News>().GetSingle(n => n.Id == id.Value);
            if (news == null) return NotFound();
            return View("News/NewsDetails", news);
        }

        public IActionResult NewsCreate()
        {
            return View("News/NewsCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewsCreate(News news, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                news.AdminId = GetAdminInfo().Id;
                news.PublishDate = DateTime.Now;
                if (imageFile?.Length > 0)
                {
                    var imageUrl = "/images/news/" + imageFile.FileName;
                    using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + imageUrl, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    news.ImageUrl = imageUrl;
                }
                _unitOfWorkManager.Repository<News>().Add(news);
                _unitOfWorkManager.Commit();
                return RedirectToAction("NewsIndex");
            }
            return View("News/NewsCreate", news);
        }

        public IActionResult NewsEdit(int? id)
        {
            if (id == null) return NotFound();
            var news = _unitOfWorkManager.Repository<News>().GetSingle(n => n.Id == id.Value);
            if (news == null) return NotFound();
            return View("News/NewsEdit", news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewsEdit(int id, News news, IFormFile imageFile)
        {
            if (id != news.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var newsObj = _unitOfWorkManager.Repository<News>().GetSingle(n => n.Id == id);
                newsObj.Title = news.Title;
                newsObj.ShortContent = news.ShortContent;
                newsObj.Content = news.Content;
                if (imageFile?.Length > 0)
                {
                    var imageUrl = "/images/news/" + imageFile.FileName;
                    using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + imageUrl, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    newsObj.ImageUrl = imageUrl;
                }
                _unitOfWorkManager.Repository<News>().Update(newsObj);
                _unitOfWorkManager.Commit();
                return RedirectToAction("NewsIndex");
            }
            return View("News/NewsEdit", news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewsDelete(int id)
        {
            var news = _unitOfWorkManager.Repository<News>().GetSingle(n => n.Id == id);
            _unitOfWorkManager.Repository<News>().Delete(news);
            _unitOfWorkManager.Commit();
            return RedirectToAction("NewsIndex");
        }
        #endregion

        #region Teacher
        public async Task<IActionResult> TeacherIndex()
        {
            return View("Teacher/TeacherIndex", await _unitOfWorkManager.Repository<Teacher>().GetAll().ToListAsync());
        }

        public IActionResult TeacherDetails(int? id)
        {
            if (id == null) return NotFound();
            var teacher = _unitOfWorkManager.Repository<Teacher>().GetSingle(t => t.Id == id.Value);
            if (teacher == null) return NotFound();
            return View("Teacher/TeacherDetails", teacher);
        }

        public IActionResult TeacherCreate()
        {
            return View("Teacher/TeacherCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeacherCreate(IFormFile imageFile, Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                if (imageFile?.Length > 0)
                {
                    var imageUrl = "/images/teachers/" + imageFile.FileName;
                    using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + imageUrl, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    teacher.ImageUrl = imageUrl;
                }
                _unitOfWorkManager.Repository<Teacher>().Add(teacher);
                _unitOfWorkManager.Commit();
                return RedirectToAction("TeacherIndex");
            }
            return View("Teacher/TeacherCreate", teacher);
        }

        public IActionResult TeacherEdit(int? id)
        {
            if (id == null) return NotFound();
            var teacher = _unitOfWorkManager.Repository<Teacher>().GetSingle(t => t.Id == id.Value);
            if (teacher == null) return NotFound();
            return View("Teacher/TeacherEdit", teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeacherEdit(int id, IFormFile imageFile, Teacher teacher)
        {
            if (id != teacher.Id) return NotFound();
            if (ModelState.IsValid)
            {
                if (imageFile?.Length > 0)
                {
                    var imageUrl = "/images/teachers/" + imageFile.FileName;
                    using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + imageUrl, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    teacher.ImageUrl = imageUrl;
                }
                _unitOfWorkManager.Repository<Teacher>().Update(teacher);
                _unitOfWorkManager.Commit();
                return RedirectToAction("TeacherIndex");
            }
            return View("Teacher/TeacherEdit", teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TeacherDelete(int id)
        {
            var teacher = _unitOfWorkManager.Repository<Teacher>().GetSingle(t => t.Id == id);
            _unitOfWorkManager.Repository<Teacher>().Delete(teacher);
            _unitOfWorkManager.Commit();
            return RedirectToAction("TeacherIndex");
        }
        #endregion

        #region HappyStudent

        public async Task<IActionResult> HappyStudentIndex()
        {
            return View("HappyStudent/HappyStudentIndex", await _unitOfWorkManager.Repository<HappyStudent>().GetAll().ToListAsync());
        }

        public IActionResult HappyStudentCreate()
        {
            return View("HappyStudent/HappyStudentCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HappyStudentCreate(IFormFile imageFile, HappyStudent happyStudent)
        {
            if (ModelState.IsValid)
            {
                if (imageFile?.Length > 0)
                {
                    var imageUrl = "/images/happyStudents/" + imageFile.FileName;
                    using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + imageUrl, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    happyStudent.ImageUrl = imageUrl;
                }
                _unitOfWorkManager.Repository<HappyStudent>().Add(happyStudent);
                _unitOfWorkManager.Commit();
                return RedirectToAction("HappyStudentIndex");
            }
            return View("HappyStudent/HappyStudentCreate", happyStudent);
        }

        public IActionResult HappyStudentEdit(int? id)
        {
            if (id == null) return NotFound();
            var happyStudent = _unitOfWorkManager.Repository<HappyStudent>().GetSingle(t => t.Id == id.Value);
            if (happyStudent == null) return NotFound();
            return View("HappyStudent/HappyStudentEdit", happyStudent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HappyStudentEdit(int id, IFormFile imageFile, HappyStudent happyStudent)
        {
            if (id != happyStudent.Id) return NotFound();
            if (ModelState.IsValid)
            {
                if (imageFile?.Length > 0)
                {
                    var imageUrl = "/images/happyStudents/" + imageFile.FileName;
                    using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + imageUrl, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    happyStudent.ImageUrl = imageUrl;
                }
                _unitOfWorkManager.Repository<HappyStudent>().Update(happyStudent);
                _unitOfWorkManager.Commit();
                return RedirectToAction("HappyStudentIndex");
            }
            return View("HappyStudent/HappyStudentEdit", happyStudent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HappyStudentDelete(int id)
        {
            var happyStudent = _unitOfWorkManager.Repository<HappyStudent>().GetSingle(t => t.Id == id);
            _unitOfWorkManager.Repository<HappyStudent>().Delete(happyStudent);
            _unitOfWorkManager.Commit();
            return RedirectToAction("HappyStudentIndex");
        }

        #endregion

        #region Data

        public async Task<IActionResult> DataIndex()
        {
            return View("Data/DataIndex", await _unitOfWorkManager.Repository<DataDictionary>().GetAll().ToListAsync());
        }

        public IActionResult DataDetails(int? id)
        {
            if (id == null) return NotFound();
            var data = _unitOfWorkManager.Repository<DataDictionary>().GetSingle(dd => dd.Id == id.Value);
            if (data == null) return NotFound();
            return View("Data/DataDetails", data);
        }

        public IActionResult DataCreate()
        {
            return View("Data/DataCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DataCreate(DataDictionary data)
        {
            if (ModelState.IsValid)
            {
                _unitOfWorkManager.Repository<DataDictionary>().Add(data);
                _unitOfWorkManager.Commit();
                return RedirectToAction("DataIndex");
            }
            return View("Data/DataCreate", data);
        }

        public IActionResult DataEdit(int? id)
        {
            if (id == null) return NotFound();
            var data = _unitOfWorkManager.Repository<DataDictionary>().GetSingle(dd => dd.Id == id.Value);
            if (data == null) return NotFound();
            return View("Data/DataEdit", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DataEdit(int id, DataDictionary data)
        {
            if (id != data.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _unitOfWorkManager.Repository<DataDictionary>().Update(data);
                _unitOfWorkManager.Commit();
                return RedirectToAction("DataIndex");
            }
            return View("Data/DataEdit", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DataDelete(int id)
        {
            var data = _unitOfWorkManager.Repository<DataDictionary>().GetSingle(dd => dd.Id == id);
            _unitOfWorkManager.Repository<DataDictionary>().Delete(data);
            _unitOfWorkManager.Commit();
            return RedirectToAction("DataIndex");
        }

        #endregion

        #region Admin
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult AdminIndex()
        {
            var admins = _unitOfWorkManager.Repository<Admin>().GetAll(a => a.Person).ToList();
            var adminViewModels = (from admin in admins
                                   let user = _userManager.Users.SingleOrDefault(u => u.PersonId == admin.PersonId)
                                   select new AdminViewModel
                                   {
                                       Id = admin.Id,
                                       Email = user.Email,
                                       Username = user.UserName,
                                       FirstName = admin.Person.FirstName,
                                       LastName = admin.Person.LastName,
                                       Gender = (int)admin.Person.Gender
                                   }).ToList();
            return View("Admin/AdminIndex", adminViewModels);
        }

        private Admin GetAdminInfo()
        {
            var user = _userManager.GetUserAsync(User).Result;
            return _unitOfWorkManager.Repository<Admin>().GetSingle(a => a.PersonId == user.PersonId);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult AdminCreate()
        {
            return View("Admin/AdminCreate");
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminCreate(AdminCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var person = new Person
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = (Gender)model.Gender,
                    Admin = new Admin()
                };

                var user = new ApplicationUser { UserName = model.Username, Email = model.Email, Person = person };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("AdminIndex");
                }
                AddErrors(result);
            }
            return View("Admin/AdminCreate", model);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult AdminEdit(int? id)
        {
            if (id == null) return NotFound();
            var admin = _unitOfWorkManager.Repository<Admin>().GetSingle(a => a.Id == id, a => a.Person);
            if (admin == null) return NotFound();
            var user = _userManager.Users.SingleOrDefault(u => u.PersonId == admin.PersonId);
            var adminViewModel = new AdminViewModel
            {
                Id = admin.Id,
                Email = user.Email,
                Username = user.UserName,
                FirstName = admin.Person.FirstName,
                LastName = admin.Person.LastName,
                Gender = (int)admin.Person.Gender
            };
            return View("Admin/AdminEdit", adminViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminEdit(int id, AdminViewModel adminModel)
        {
            if (id != adminModel.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var admin = _unitOfWorkManager.Repository<Admin>().GetSingle(a => a.Id == adminModel.Id, a => a.Person);
                var user = _userManager.Users.SingleOrDefault(u => u.PersonId == admin.PersonId);
                user.UserName = adminModel.Username;
                user.Email = adminModel.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    admin.Person.FirstName = adminModel.FirstName;
                    admin.Person.LastName = adminModel.LastName;
                    admin.Person.Gender = (Gender)adminModel.Gender;

                    _unitOfWorkManager.Repository<Admin>().Update(admin);
                }
                _unitOfWorkManager.Commit();
                return RedirectToAction("AdminIndex");
            }
            return View("Admin/AdminEdit", adminModel);
        }

        [HttpPost]
        [ActionName("AdminDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDelete(int id)
        {
            var admin = _unitOfWorkManager.Repository<Admin>().GetSingle(a => a.Id == id);
            var person = _unitOfWorkManager.Repository<Person>().GetSingle(p => p.Id == admin.PersonId);

            var user = _userManager.Users.SingleOrDefault(u => u.PersonId == admin.PersonId);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _unitOfWorkManager.Repository<Person>().Delete(person);
                _unitOfWorkManager.Commit();
            }
            return RedirectToAction("AdminIndex");
        }

        #endregion

        #region Exam

        public async Task<IActionResult> ExamIndex()
        {
            return View("Exam/ExamIndex", await _unitOfWorkManager.Repository<Exam>().GetAll().OrderByDescending(e => e.Date).ToListAsync());
        }

        public IActionResult ExamCreate()
        {
            var examtypes = Enum.GetNames(typeof(ExamType));
            var examTypesSelects = (from examtype in examtypes
                let value = Enum.Parse(typeof(ExamType), examtype)
                select new SelectListItem
                {
                    Value = value.ToString(),
                    Text = examtype
                }).ToList();
            ViewBag.ExamTypes = examTypesSelects;
            return View("Exam/ExamCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExamCreate(Exam exam)
        {
            if (ModelState.IsValid)
            {
                exam.Date = exam.Date.Date;
                _unitOfWorkManager.Repository<Exam>().Add(exam);
                var groups = _unitOfWorkManager.Repository<Group>().GetAll().ToList();
                foreach (var group in groups)
                {
                    var ge = new GroupExam
                    {
                        ExamId = exam.Id,
                        GroupId = group.Id
                    };
                    _unitOfWorkManager.Repository<GroupExam>().Add(ge);
                }
                _unitOfWorkManager.Commit();
                return RedirectToAction("ExamIndex");
            }
            var examtypes = Enum.GetNames(typeof(ExamType));
            var examTypesSelects = (from examtype in examtypes
                                    let value = (int)Enum.Parse(typeof(ExamType), examtype)
                                    select new SelectListItem
                                    {
                                        Value = value.ToString(),
                                        Text = examtype,
                                        Selected = exam.ExamType == (ExamType)value
                                    }).ToList();
            ViewBag.ExamTypes = examTypesSelects;
            return View("Exam/ExamCreate", exam);
        }

        public IActionResult ExamEdit(int? id)
        {
            if (id == null) return NotFound();
            var exam = _unitOfWorkManager.Repository<Exam>().GetSingle(g => g.Id == id.Value);
            if (exam == null) return NotFound();
            var examtypes = Enum.GetNames(typeof(ExamType));
            var examTypesSelects = (from examtype in examtypes
                                    let value = (int)Enum.Parse(typeof(ExamType), examtype)
                                    select new SelectListItem
                                    {
                                        Value = value.ToString(),
                                        Text = examtype,
                                        Selected = exam.ExamType == (ExamType)value
                                    }).ToList();
            ViewBag.ExamTypes = examTypesSelects;
            return View("Exam/ExamEdit", exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExamEdit(int id, Exam exam)
        {
            if (id != exam.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _unitOfWorkManager.Repository<Exam>().Update(exam);
                _unitOfWorkManager.Commit();
                return RedirectToAction("ExamIndex");
            }
            var examtypes = Enum.GetNames(typeof(ExamType));
            var examTypesSelects = (from examtype in examtypes
                                    let value = (int)Enum.Parse(typeof(ExamType), examtype)
                                    select new SelectListItem
                                    {
                                        Value = value.ToString(),
                                        Text = examtype,
                                        Selected = exam.ExamType == (ExamType)value
                                    }).ToList();
            ViewBag.ExamTypes = examTypesSelects;
            return View("Exam/ExamEdit", exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExamDelete(int id)
        {
            var exam = _unitOfWorkManager.Repository<Exam>().GetSingle(g => g.Id == id, g => g.GroupExams);
            _unitOfWorkManager.Repository<ExamTest>().FindBy(et => et.GroupExamId == exam.Id).ToList().ForEach(et => { _unitOfWorkManager.Repository<ExamTest>().Delete(et); });
            foreach (var groupExam in exam.GroupExams.ToList())
            {
                _unitOfWorkManager.Repository<GroupExam>().Delete(groupExam);
            }
            _unitOfWorkManager.Repository<Exam>().Delete(exam);
            _unitOfWorkManager.Commit();
            return RedirectToAction("ExamIndex");
        }

        public async Task<IActionResult> ExamTestIndex(int courseId, int groupExamId)
        {
            return View("ExamTest/TestIndex",
                await
                    _unitOfWorkManager.Repository<ExamTest>().FindBy(et => et.GroupExamId == groupExamId && et.CourseLessonTest.CourseLesson.CourseId == courseId
                    , et => et.CourseLessonTest.Test.Answers, et => et.CourseLessonTest.Test.TestAnswer).ToListAsync());
        }

        public async Task<IActionResult> ExamGroups(int examId)
        {
            var examGroups = await _unitOfWorkManager.Repository<GroupExam>().FindBy(eg => eg.ExamId == examId, eg => eg.Group).ToListAsync();
            return View("Exam/ExamGroups", examGroups);
        }

        public async Task<IActionResult> ExamCourses(int groupExamId)
        {
            var exam = _unitOfWorkManager.Repository<GroupExam>().GetSingle(ge => ge.Id == groupExamId);
            var courses = await _unitOfWorkManager.Repository<Course>().FindBy(c => c.GroupId == exam.GroupId, c => c.Subject).ToListAsync();
            ViewBag.GroupExamId = groupExamId;
            ViewBag.ExamId = exam.ExamId;
            return View("Exam/ExamCourses", courses);
        }

        public IActionResult ExamTestAdd(int courseId, int examId)
        {
            var cs = new ExamTestViewModel();
            var tests = _unitOfWorkManager.Repository<CourseLessonTest>().FindBy(clt => clt.CourseLesson.CourseId == courseId && clt.CourseLesson.Status && clt.Status && clt.Test.Status, clt => clt.Test.Answers, clt => clt.Test.TestAnswer).ToList();
            var examTests = _unitOfWorkManager.Repository<ExamTest>().FindBy(et => et.GroupExamId == examId, qt => qt.CourseLessonTest.Test.Answers, qt => qt.CourseLessonTest.Test.TestAnswer).ToList();
            foreach (var test in examTests)
            {
                tests.Remove(test.CourseLessonTest);
            }
            cs.CourseLessonTests = new List<CourseLessonTest>(tests);
            cs.TestIds = new List<int>();
            cs.CourseLesson = new CourseLesson { CourseId = courseId };
            cs.ExamTest = new ExamTest { GroupExamId = examId };
            return View("ExamTest/TestAdd", cs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExamTestAdd(ExamTestViewModel examTestViewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var id in examTestViewModel.TestIds)
                {
                    var et = new ExamTest
                    {
                        GroupExamId = examTestViewModel.ExamTest.GroupExamId,
                        CourseLessonTestId = id
                    };
                    _unitOfWorkManager.Repository<ExamTest>().Add(et);
                    _unitOfWorkManager.Commit();
                }
                return RedirectToAction("ExamTestIndex", new { courseId = examTestViewModel.CourseLesson.CourseId, groupExamId = examTestViewModel.ExamTest.GroupExamId });
            }
            return View("ExamTest/TestAdd", examTestViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExamTestDelete(int? id, int courseId)
        {
            if (id == null) return NotFound();
            var test = _unitOfWorkManager.Repository<ExamTest>().GetSingle(et => et.Id == id.Value);
            if (test == null) return NotFound();
            _unitOfWorkManager.Repository<ExamTest>().Delete(test);
            _unitOfWorkManager.Commit();
            return RedirectToAction("ExamTestIndex", new { groupExamId = test.GroupExamId, courseId });
        }

        #endregion

        #region CourseLesson

        public async Task<IActionResult> CourseLessonIndex(int courseId)
        {
            return View("CourseLesson/LessonIndex",
                await _unitOfWorkManager.Repository<CourseLesson>().FindBy(cl => cl.CourseId == courseId, cl => cl.Lesson).ToListAsync());
        }

        public IActionResult LessonAdd(int courseId)
        {
            ViewBag.CourseId = courseId;
            var cs = new CourseSubjectViewModel();
            var course = _unitOfWorkManager.Repository<Course>().GetSingle(c => c.Id == courseId);
            var lessons = _unitOfWorkManager.Repository<Lesson>().GetAll().Where(l => l.SubjectId == course.SubjectId).ToList();
            var courseLessons = _unitOfWorkManager.Repository<CourseLesson>().FindBy(cl => cl.CourseId == courseId, cl => cl.Lesson).ToList();
            foreach (var courseLesson in courseLessons)
            {
                lessons.Remove(courseLesson.Lesson);
            }
            cs.Course = course;
            cs.Lessons = new SelectList(lessons.Select(s => new { s.Id, s.Name }), "Id", "Name");
            return View("CourseLesson/LessonAdd", cs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LessonAdd(CourseSubjectViewModel courseSubjectViewModel)
        {
            if (ModelState.IsValid)
            {
                courseSubjectViewModel.CourseLesson.Status = true;
                _unitOfWorkManager.Repository<CourseLesson>().Add(courseSubjectViewModel.CourseLesson);
                _unitOfWorkManager.Commit();
                return RedirectToAction("CourseLessonIndex", new { courseId = courseSubjectViewModel.CourseLesson.CourseId });
            }
            ViewBag.CourseId = courseSubjectViewModel.CourseLesson.CourseId;
            return View("CourseLesson/LessonAdd", courseSubjectViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CourseLessonDelete(int id)
        {
            var courseLesson = _unitOfWorkManager.Repository<CourseLesson>().GetSingle(cl => cl.Id == id);
            _unitOfWorkManager.Repository<CourseLesson>().Delete(courseLesson);
            _unitOfWorkManager.Commit();
            return RedirectToAction("CourseLessonIndex", new { courseId = courseLesson.CourseId });
        }

        #endregion

        #region QuizTest

        public async Task<IActionResult> QuizTestIndex(int lessonId)
        {
            return View("QuizTest/TestIndex",
                await _unitOfWorkManager.Repository<QuizTest>().FindBy(t => t.CourseLessonId == lessonId,
                        t => t.CourseLessonTest.Test.Answers,
                        t => t.CourseLessonTest.Test.TestAnswer)
                    .ToListAsync());
        }

        public IActionResult TestAdd(int lessonId)
        {
            var cs = new CourseSubjectViewModel();
            var tests = _unitOfWorkManager.Repository<CourseLessonTest>().FindBy(clt => clt.CourseLessonId == lessonId && clt.CourseLesson.Status && clt.Status, clt => clt.Test.Answers, clt => clt.Test.TestAnswer).ToList();
            var quizTests = _unitOfWorkManager.Repository<QuizTest>().FindBy(qt => qt.CourseLessonId == lessonId, qt => qt.CourseLessonTest.Test.Answers, qt => qt.CourseLessonTest.Test.TestAnswer).ToList();
            foreach (var test in quizTests)
            {
                tests.Remove(test.CourseLessonTest);
            }
            cs.CourseLessonTests = new List<CourseLessonTest>(tests);
            cs.TestIds = new List<int>();
            cs.CourseLesson = new CourseLesson { Id = lessonId };
            return View("QuizTest/TestAdd", cs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TestAdd(CourseSubjectViewModel courseSubjectViewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var id in courseSubjectViewModel.TestIds)
                {
                    var qt = new QuizTest
                    {
                        CourseLessonId = courseSubjectViewModel.CourseLesson.Id,
                        CourseLessonTestId = id
                    };
                    _unitOfWorkManager.Repository<QuizTest>().Add(qt);
                    _unitOfWorkManager.Commit();
                }
                return RedirectToAction("QuizTestIndex", new { lessonId = courseSubjectViewModel.CourseLesson.Id });
            }
            return View("QuizTest/TestAdd", courseSubjectViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult QuizTestDelete(int id)
        {
            var test = _unitOfWorkManager.Repository<QuizTest>().GetSingle(t => t.Id == id);
            _unitOfWorkManager.Repository<QuizTest>().Delete(test);
            _unitOfWorkManager.Commit();
            return RedirectToAction("QuizTestIndex", new { lessonId = test.CourseLessonId });
        }

        #endregion

        #region Test

        public async Task<IActionResult> TestIndex(int lessonId)
        {
            var tests = await _unitOfWorkManager.Repository<CourseLessonTest>().FindBy(clt => clt.Status &&
                   clt.CourseLesson.LessonId == lessonId && clt.CourseLesson.Status,
                           clt => clt.Test.Answers, t => t.Test.TestAnswer).ToListAsync();
            return View("Test/TestIndex", tests.Select(t => t.Test).Distinct().ToList());
        }

        public IActionResult TestCreate(int lessonId)
        {
            var groups =
                _unitOfWorkManager.Repository<CourseLesson>().FindBy(cl => (cl.LessonId == lessonId) && cl.Status, cl => cl.Course)
                    .Where(c => c.Course.Status)
                    .Select(g => g.Course.Group).OrderBy(g => g.Id)
                    .ToList();
            var testModel = new TestViewModel
            {
                Groups = groups,
                GroupIds = new List<int>(),
                Test = new CourseLessonTest { CourseLesson = new CourseLesson { LessonId = lessonId }, Test = new Test() },
                Answers = new List<Answer>()
            };

            ViewBag.LessonId = lessonId;
            return View("Test/TestCreate", testModel);
        }

        public IActionResult TestEdit(int? id, int lessonId)
        {
            if (id == null) return NotFound();
            var test = _unitOfWorkManager.Repository<Test>().GetSingle(t => t.Id == id.Value, t => t.Answers, t => t.TestAnswer.Answer);
            var groups =
                _unitOfWorkManager.Repository<CourseLesson>().FindBy(cl => (cl.LessonId == lessonId) && cl.Status, cl => cl.Course)
                    .Where(c => c.Course.Status)
                    .Select(g => g.Course.Group)
                    .ToList();
            var gropIds = _unitOfWorkManager.Repository<CourseLessonTest>().FindBy(clt => clt.TestId == id.Value && clt.Status, clt => clt.CourseLesson, clt => clt.CourseLesson.Course)
                .Where(clt => clt.CourseLesson.Status && clt.CourseLesson.Course.Status)
                .Select(clt => clt.CourseLesson.Course.GroupId).ToList();
            var testmodel = new TestViewModel
            {
                Test = new CourseLessonTest { Test = test, CourseLesson = new CourseLesson { LessonId = lessonId } },
                Answers = new List<Answer>(test.Answers),
                Groups = new List<Group>(groups),
                GroupIds = gropIds
            };
            testmodel.CorrectAnswerId = testmodel.Answers.IndexOf(test.TestAnswer.Answer);
            ViewBag.LessonId = lessonId;
            return View("Test/TestEdit", testmodel);
        }

        [HttpPost]
        public async Task<IActionResult> TestSave(TestViewModel testViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    testViewModel.Test.Test.Answers = testViewModel.Answers;
                    if (testViewModel.Test.Test.Id > 0)
                    {
                        var testold = _unitOfWorkManager.Repository<Test>().GetSingle(t => t.Id == testViewModel.Test.Test.Id, t => t.Answers, t => t.TestAnswer);
                        testold.Status = false;
                        _unitOfWorkManager.Repository<Test>().Update(testold);
                        foreach (var answer in testold.Answers)
                        {
                            answer.Status = false;
                            _unitOfWorkManager.Repository<Answer>().Update(answer);
                        }
                        var courseLessonTests = _unitOfWorkManager.Repository<CourseLessonTest>().FindBy(clt => clt.TestId == testold.Id).ToList().Select(clt => { clt.Status = false; return clt; }).ToList();
                        foreach (var clt in courseLessonTests)
                        {
                            _unitOfWorkManager.Repository<CourseLessonTest>().Update(clt);
                        }
                    }
                    testViewModel.Test.Test.Id = 0;
                    _unitOfWorkManager.Repository<Test>().Add(testViewModel.Test.Test);
                    if (testViewModel.Test.Test.TestType == 0)
                    {
                        foreach (var answer in testViewModel.Test.Test.Answers)
                        {
                            answer.TestId = testViewModel.Test.Test.Id;
                            answer.Status = true;
                            if (answer.Equals(testViewModel.Answers[testViewModel.CorrectAnswerId]))
                            {
                                _unitOfWorkManager.Repository<Answer>().Add(answer);
                                _unitOfWorkManager.Repository<TestAnswer>().Add(new TestAnswer { TestId = testViewModel.Test.Test.Id, AnswerId = answer.Id });
                            }
                            else
                            {
                                _unitOfWorkManager.Repository<Answer>().Add(answer);
                            }
                        }
                    }
                    else
                    {
                        testViewModel.Test.Test.TestAnswer.Answer.TestId = testViewModel.Test.Test.Id;
                        testViewModel.Test.Test.TestAnswer.Answer.Status = true;
                        _unitOfWorkManager.Repository<Answer>().Add(testViewModel.Test.Test.TestAnswer.Answer);
                        _unitOfWorkManager.Repository<TestAnswer>().Add(new TestAnswer { TestId = testViewModel.Test.Test.Id, AnswerId = testViewModel.Test.Test.TestAnswer.Answer.Id });
                    }
                    foreach (var id in testViewModel.GroupIds)
                    {
                        var courseLesson =
                            await
                                _unitOfWorkManager.Repository<CourseLesson>().FindBy(
                                        c => (c.LessonId == testViewModel.Test.CourseLesson.LessonId) && c.Status, c => c.Course).Where(c => c.Course.GroupId == id)
                                    .FirstOrDefaultAsync();
                        _unitOfWorkManager.Repository<CourseLessonTest>().Add(new CourseLessonTest
                        {
                            CourseLessonId = courseLesson.Id,
                            TestId = testViewModel.Test.Test.Id,
                            Status = true
                        });
                    }
                    _unitOfWorkManager.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction("TestIndex", new { lessonId = testViewModel.Test.CourseLesson.LessonId });
                }
                return RedirectToAction("TestIndex", new { lessonId = testViewModel.Test.CourseLesson.LessonId });
            }
            return View("Test/TestEdit", testViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TestDelete(int id, int lessonId)
        {
            var test = _unitOfWorkManager.Repository<Test>().GetSingle(t => t.Id == id, t => t.Answers, t => t.TestAnswer);
            test.Status = false;
            _unitOfWorkManager.Repository<Test>().Update(test);
            _unitOfWorkManager.Repository<CourseLessonTest>().FindBy(clt => clt.TestId == test.Id).ToList().Select(t => { t.Status = false; return t; }).ToList().ForEach(t => { _unitOfWorkManager.Repository<CourseLessonTest>().Update(t); });
            _unitOfWorkManager.Repository<Answer>().FindBy(clt => clt.TestId == test.Id).ToList().Select(t => { t.Status = false; return t; }).ToList().ForEach(t => { _unitOfWorkManager.Repository<Answer>().Update(t); });
            ViewBag.LessonId = lessonId;
            _unitOfWorkManager.Commit();
            return RedirectToAction("TestIndex", new { lessonId });
        }

        public IActionResult TestTypeChange(TestViewModel testViewModel)
        {
            var groups =
                _unitOfWorkManager.Repository<CourseLesson>().FindBy(cl => (cl.LessonId == testViewModel.Test.CourseLesson.LessonId) && cl.Status, cl => cl.Course)
                    .Where(c => c.Course.Status)
                    .Select(g => g.Course.Group)
                    .ToList();
            testViewModel.Groups = groups;
            testViewModel.GroupIds = new List<int>();
            testViewModel.Answers = new List<Answer>();

            ViewBag.LessonId = testViewModel.Test.CourseLesson.LessonId;
            if ((int)testViewModel.Test.Test.TestType == 0)
            {
                return View("Test/_Edit", testViewModel);
            }
            return View("Test/_Edit1", testViewModel);
        }

        public ActionResult TestAnswerAdd(TestViewModel testViewModel)
        {
            var groups =
                _unitOfWorkManager.Repository<CourseLesson>().FindBy(cl => (cl.LessonId == testViewModel.Test.CourseLesson.LessonId) && cl.Status, cl => cl.Course)
                    .Where(c => c.Course.Status)
                    .Select(g => g.Course.Group)
                    .ToList();
            testViewModel.Groups = groups;
            if (testViewModel.GroupIds == null)
                testViewModel.GroupIds = new List<int>();
            if (testViewModel.Answers != null)
            {
                testViewModel.Answers.Add(new Answer { Text = "", Id = 0 });
            }
            else
            {
                testViewModel.Answers = new List<Answer> { new Answer { Text = "", Id = 0 } };
            }
            return PartialView("Test/_Edit", testViewModel);
        }

        public ActionResult TestAnswerDelete(int id, TestViewModel testViewModel)
        {
            var groups =
                _unitOfWorkManager.Repository<CourseLesson>().FindBy(cl => (cl.LessonId == testViewModel.Test.CourseLesson.LessonId) && cl.Status, cl => cl.Course)
                    .Where(c => c.Course.Status)
                    .Select(g => g.Course.Group)
                    .ToList();
            testViewModel.Groups = groups;
            if (testViewModel.GroupIds == null)
                testViewModel.GroupIds = new List<int>();
            testViewModel.Answers.RemoveAt(id);
            return PartialView("Test/_Edit", testViewModel);
        }

        #endregion

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }

        #region CkEditor Image Upload

        public ActionResult UploadTestImage(IFormFile upload, string ckEditorFuncNum, string ckEditor, string langCode)
        {
            var newName = upload.FileName.Replace(".", "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".");
            var imageUrl = "/images/formulas/" + newName;
            using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + imageUrl, FileMode.Create))
            {
                upload.CopyToAsync(fileStream);
            }
            var vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + ckEditorFuncNum + ", \"" + imageUrl + "\", \"\");</script></body></html>";
            return Content(vOutput, "text/html");
        }

        public ActionResult UploadNewsImage(IFormFile upload, string ckEditorFuncNum, string ckEditor, string langCode)
        {
            var newName = upload.FileName.Replace(".", "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".");
            var imageUrl = "/images/news/" + newName;
            using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + imageUrl, FileMode.Create))
            {
                upload.CopyToAsync(fileStream);
            }
            var vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + ckEditorFuncNum + ", \"" + imageUrl + "\", \"\");</script></body></html>";
            return Content(vOutput, "text/html");
        }

        #endregion
    }
}