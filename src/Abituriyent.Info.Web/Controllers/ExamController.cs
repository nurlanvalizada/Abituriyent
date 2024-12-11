using System;
using System.Threading.Tasks;
using System.Linq;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Web.MyExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Abituriyent.Info.Repository.UOW;
using Abituriyent.Info.Web.Models.ViewModels;

namespace Abituriyent.Info.Web.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExamController(IUnitOfWorkManager unitOfWorkManager, UserManager<ApplicationUser> userManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _userManager = userManager;
        }

        //
        // GET: Exam/Index
        public async Task<IActionResult> Index()
        {
            var student = this.GetStudent(_userManager, _unitOfWorkManager.StudentRepository);
            return View(await _unitOfWorkManager.GroupExamRepository.FindBy(e => e.GroupId == student.GroupId, e => e.Group, e=>e.Exam).ToListAsync());
        }

        //
        // GET: Exam/TakeExam/5
        public async Task<ActionResult> TakeExam(int id)
        {
            var student = this.GetStudent(_userManager, _unitOfWorkManager.StudentRepository);
            if (_unitOfWorkManager.GroupExamRepository.IsExamAvailable(student.GroupId, id))
            {
                if (!_unitOfWorkManager.Repository<StudentExam>().Any(se => (se.StudentId == student.Id) && (se.GroupExamId == id)))
                {
                    _unitOfWorkManager.Repository<StudentExam>().Add(new StudentExam
                    {
                        StudentId = student.Id,
                        GroupExamId = id,
                        StartTime = DateTime.Now.TimeOfDay
                    });
                    ViewBag.ExamStatus = "available";
                }
                else
                {
                    ViewBag.ExamStatus = "taken";
                    return View("Index",
                        await _unitOfWorkManager.GroupExamRepository.FindBy(e => e.GroupId == student.GroupId, e => e.Group, e => e.Exam).ToListAsync());
                }
            }
            else
            {
                ViewBag.ExamStatus = "notAvailable";
                return View("Index",
                    await _unitOfWorkManager.GroupExamRepository.FindBy(e => e.GroupId == student.GroupId, e => e.Group, e => e.Exam).ToListAsync());
            }

            var examTests = await _unitOfWorkManager.Repository<ExamTest>().FindBy(et => et.GroupExamId == id, et => et.CourseLessonTest.Test.Answers).ToListAsync();

            var testViewModels = examTests.Select(et => new TestViewModel
            {
                Id = et.CourseLessonTest.Test.Id,
                Content = et.CourseLessonTest.Test.Content,
                TestType = et.CourseLessonTest.Test.TestType,
                Answers = et.CourseLessonTest.Test.Answers.Select(a => new AnswerViewModel
                {
                    Id = a.Id,
                    Text = a.Text
                })
            }).ToList();

            var exam = _unitOfWorkManager.GroupExamRepository.GetSingle(e => e.Id == id,e=>e.Exam);

            var examViewModel = new ExamViewModel
            {
                Id = exam.Id,
                Name = exam.Exam.Name,
                Date = exam.Exam.Date,
                StartTime = exam.Exam.StartTime,
                EndTime = exam.Exam.EndTime,
                ExamType = exam.Exam.ExamType,
                Tests = testViewModels
            };
            ViewBag.ExamId = id;
            _unitOfWorkManager.Commit();
            return View(examViewModel);
        }

        //
        // GET: Exam/ViewExam/5
        public async Task<ActionResult> ViewExam(int id)
        {
            ViewBag.ExamId = id;
            var student = this.GetStudent(_userManager, _unitOfWorkManager.StudentRepository);
            if (_unitOfWorkManager.Repository<StudentExam>().Any(
                se => (se.GroupExamId == id) && (se.StudentId == student.Id) && (se.EndTime != null)))
            {
                var examTests = await
                    _unitOfWorkManager.Repository<ExamTest>().FindBy(et => et.GroupExamId == id,
                            et => et.CourseLessonTest.Test.Answers,
                            et => et.CourseLessonTest.Test.TestAnswer,
                            et => et.CourseLessonTest.Test.TestAnswer.Answer)
                        .ToListAsync();
                var list = new List<ExamTestViewModel>();
                foreach(var examTest in examTests)
                {
                    var tvm = new ExamTestViewModel
                    {
                        Id = examTest.CourseLessonTest.Id,
                        Content = examTest.CourseLessonTest.Test.Content,
                        Answers = examTest.CourseLessonTest.Test.Answers,
                        correctAnswer= examTest.CourseLessonTest.Test.TestAnswer.Answer,
                        TestType= examTest.CourseLessonTest.Test.TestType
                    };
                    if ((int)examTest.CourseLessonTest.Test.TestType == 0)
                    {
                        var sa = _unitOfWorkManager.Repository<StudentExamTestAnswer>().GetSingle(seta => seta.ExamTestId == examTest.Id && seta.StudentId == student.Id, seta => seta.Answer);
                        if (sa != null)
                        {
                            if (sa.AnswerId == examTest.CourseLessonTest.Test.TestAnswer.AnswerId)
                            {
                                tvm.isCorrect = true;
                            }
                            tvm.studentAnswer = sa.Answer;
                        }
                    }else
                    {
                        var sa = _unitOfWorkManager.Repository<StudentExamOpenTestAnswer>().GetSingle(seta => seta.ExamTestId == examTest.Id && seta.StudentId == student.Id);
                        if (sa != null)
                        {
                            if (sa.AnswerText == examTest.CourseLessonTest.Test.TestAnswer.Answer.Text)
                            {
                                tvm.isCorrect = true;
                            }
                            tvm.studentAnswer = new Answer {Text= sa.AnswerText };
                        }
                    }
                    list.Add(tvm);
                    
                }

                return View(list);
            }
            ViewBag.ExamStatus = "notTaken";
            return View("Index",await _unitOfWorkManager.GroupExamRepository.FindBy(e => e.GroupId == student.GroupId, e => e.Group, e => e.Exam).ToListAsync());
        }
    }
}