using System.Collections.Generic;
using Abituriyent.Info.Core.Models;

namespace Abituriyent.Info.Web.Models.ViewModels
{
    public class CourseLessonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VideoUrl { get; set; }
        public int CourseId { get; set; }
        public LessonStatus Status { get; set; }
        public IEnumerable<TestViewModel> Tests { get; set; }
    }
}
