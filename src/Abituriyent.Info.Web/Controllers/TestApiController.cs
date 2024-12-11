using System;
using System.Linq;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Abituriyent.Info.Repository.UOW;
using Abituriyent.Info.Web.Models.ViewModels;
using Abituriyent.Info.Web.MyExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Abituriyent.Info.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TestApiController : Controller
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private UserManager<ApplicationUser> UserManager { get; }

        public TestApiController(IUnitOfWorkManager unitOfWorkManager, UserManager<ApplicationUser> userManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            UserManager = userManager;
        }

        [HttpPost]
        [Route("[action]")]
        public TestCheckResult CheckTestResults(IFormCollection collection)
        {
            var closedTestAnswers = collection.Where(x => x.Key.Contains("optionsRadios"))
                .Select(ta => new ClosedTestAnswer
                {
                    TestId = int.Parse(ta.Key.Replace("optionsRadios", "")),
                    AnswerId = int.Parse(ta.Value)
                }).ToList();

            var openTestAnswers = collection.Where(x => x.Key.Contains("openValue"))
               .Select(ta => new OpenTestAnswer
               {
                   TestId = int.Parse(ta.Key.Replace("openValue", "")),
                   AnswerText = ta.Value
               }).ToList();

            var lessonId = collection.ContainsKey("lessonId") ? int.Parse(collection["lessonId"]) : 0;
            var examId = collection.ContainsKey("examId") ? int.Parse(collection["examId"]) : 0;
            var type = collection.ContainsKey("type") ? collection["type"].ToString() : null;

            var student = this.GetStudent(UserManager, _unitOfWorkManager.StudentRepository);

            switch (type)
            {
                case "lesson":
                    if (!_unitOfWorkManager.StudentLessonTestAnswerRepository.IsAlreadyAnswered(student.Id, lessonId))
                    {
                        var studentLesson = _unitOfWorkManager.StudentLessonRepository.GetSingle(sl => sl.StudentId == student.Id && sl.CourseLessonId == lessonId);
                        studentLesson.EndDate = DateTime.Now;
                        _unitOfWorkManager.StudentLessonRepository.Update(studentLesson);

                        foreach (var testAnswer in closedTestAnswers)
                            _unitOfWorkManager.StudentLessonTestAnswerRepository.Add(new StudentLessonTestAnswer
                            {
                                StudentId = student.Id,
                                AnswerId = testAnswer.AnswerId
                            });

                        foreach (var testAnswer in openTestAnswers)
                            _unitOfWorkManager.Repository<StudentLessonOpenTestAnswer>().Add(new StudentLessonOpenTestAnswer
                            {
                                StudentId = student.Id,
                                AnswerText = testAnswer.AnswerText
                            });
                    }
                    else return null;
                    break;
                case "exam":
                    if (!_unitOfWorkManager.Repository<StudentExamTestAnswer>().Any(seta => seta.StudentId == student.Id && seta.ExamTest.GroupExamId == examId))
                    {
                        foreach (var testAnswer in closedTestAnswers)
                        {
                            var examTest = _unitOfWorkManager.Repository<ExamTest>().GetSingle(et => et.GroupExamId == examId && et.CourseLessonTest.TestId == testAnswer.TestId);
                            _unitOfWorkManager.Repository<StudentExamTestAnswer>().Add(new StudentExamTestAnswer
                            {
                                StudentId = student.Id,
                                ExamTestId = examTest.Id,
                                AnswerId = testAnswer.AnswerId
                            });
                        }

                        foreach (var testAnswer in openTestAnswers)
                        {
                            var examTest = _unitOfWorkManager.Repository<ExamTest>().GetSingle(et => et.GroupExamId == examId && et.CourseLessonTest.TestId == testAnswer.TestId);
                            _unitOfWorkManager.Repository<StudentExamOpenTestAnswer>().Add(new StudentExamOpenTestAnswer
                            {
                                StudentId = student.Id,
                                ExamTestId = examTest.Id,
                                AnswerText = testAnswer.AnswerText
                            });
                        }
                      
                    }
                    else return null;
                    break;
                default: throw new InvalidOperationException("Invalid test type parameter");
            }

            int totalScore;
            var results = _unitOfWorkManager.TestAnswerRepository.CheckTestAnswers(closedTestAnswers, openTestAnswers, out totalScore).ToList();

            if (type=="exam") //updating result and end date for the student
            {
                var studentExam = _unitOfWorkManager.Repository<StudentExam>().GetSingle(se => se.StudentId == student.Id && se.GroupExamId == examId);
                studentExam.EndTime = DateTime.Now.TimeOfDay;
                studentExam.ExamResult = (short) totalScore;
                _unitOfWorkManager.Repository<StudentExam>().Update(studentExam);
            }
            _unitOfWorkManager.Commit();
            return new TestCheckResult { TotalScore = type == "exam" ? (totalScore < 0 ? 0 : totalScore) : -1, Results = results };
        }
    }
}