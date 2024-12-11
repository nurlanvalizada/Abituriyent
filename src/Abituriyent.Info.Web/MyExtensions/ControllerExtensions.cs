using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Repository.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abituriyent.Info.Web.MyExtensions
{
    public static class ControllerExtensions
    {
        public static Student GetStudent(this ControllerBase controller, UserManager<ApplicationUser> userManager, IStudentRepository studentRepository)
        {
            var user = userManager.GetUserAsync(controller.User).Result;
            return studentRepository.GetSingleWithoutImage(s => s.PersonId == user.PersonId);
        }

        public static ActionResult ReadLessonPdf(this ControllerBase controller, int lessonId, ILessonRepository lessonRepository)
        {
            var lesson = lessonRepository.GetLessonPdf(lessonId);
            if (lesson.Item2 != null)
            {
                return new FileStreamResult(new MemoryStream(lesson.Item2), lesson.Item1);
            }
            return
                controller.BadRequest(
                    "Bu dərs üçün PDF faylı hələ yüklənməmişdir. Zəhmət olmasa bir müddət sonra yenidən yoxlayın ...");
        }

        public static ActionResult ViewCurrentStudentImage(this ControllerBase controller, UserManager<ApplicationUser> userManager, IStudentRepository studentRepository, string webRootPath)
        {
            var user = userManager.GetUserAsync(controller.User).Result;
            var student = studentRepository.GetStudentImageByPersonId(user.PersonId);
            return student.Item2 != null
                ? new FileStreamResult(new MemoryStream(student.Item2), student.Item1)
                : new FileStreamResult(
                    new FileStream(webRootPath + "/images/profile_avatar.png", FileMode.Open, FileAccess.Read,
                        FileShare.ReadWrite), "image/png");
        }

        public static ActionResult ViewStudentImage(this ControllerBase controller, int studentId, IStudentRepository studentRepository, string webRootPath)
        {
            var student = studentRepository.GetStudentImage(studentId);
            return student.Item2 != null
                ? new FileStreamResult(new MemoryStream(student.Item2), student.Item1)
                : new FileStreamResult(
                    new FileStream(webRootPath + "/images/profile_avatar.png", FileMode.Open, FileAccess.Read,
                        FileShare.ReadWrite), "image/png");
        }

        public static Tuple<int, int, int, int> GetPaginationParams(this ControllerBase controller, int? pageNumber,
            int pageSize, int maxShowingPages, int totalCount)
        {
            var pageCount = totalCount/pageSize + (totalCount%pageSize == 0 ? 0 : 1);
            pageNumber = pageNumber ?? 1;

            var pageStartIndex = pageNumber.Value - maxShowingPages/2;
            var difference = 0;
            if (pageStartIndex < 1)
            {
                difference = 1 - pageStartIndex;
                pageStartIndex = 1;
            }

            var pageEndIndex = pageNumber.Value + maxShowingPages/2 + difference;
            if (pageEndIndex > pageCount)
            {
                if (pageStartIndex - (pageEndIndex - pageCount) > 0)
                {
                    pageStartIndex = pageEndIndex - pageCount;
                }
                pageEndIndex = pageCount;
            }

            return new Tuple<int, int, int, int>(pageStartIndex, pageEndIndex, pageNumber.Value, pageCount);
        }

        public static SelectList GetDays(this ControllerBase controller)
        {
            return new SelectList(Enumerable.Range(1, 31).Select(r => new {Id = r, Name = r}), "Id", "Name");
        }

        public static SelectList GetMonths(this ControllerBase controller)
        {
            var months = new Dictionary<int, string>
            {
                {1, "Yanvar"},
                {2, "Fevral"},
                {3, "Mart"},
                {4, "Aprel"},
                {5, "May"},
                {6, "İyun"},
                {7, "İyul"},
                {8, "Avqust"},
                {9, "Sentyabr"},
                {10, "Oktyabr"},
                {11, "Noyabr"},
                {12, "Dekabr"}
            };
            return new SelectList(months.Select(m => new {Id = m.Key, Name = m.Value}), "Id", "Name");
        }

        public static SelectList GetYears(this ControllerBase controller)
        {
            return
                new SelectList(
                    Enumerable.Range(DateTime.Now.Year - 80, 74)
                        .OrderByDescending(r => r)
                        .Select(r => new {Id = r, Name = r}), "Id", "Name");
        }
    }
}