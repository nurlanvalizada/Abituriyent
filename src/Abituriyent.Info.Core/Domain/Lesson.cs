using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class Lesson : Entity
    {
        public string Name { get; set; }
        public string VideoUrl { get; set; }
        public bool Status { get; set; }
        public string PdfContentType { get; set; }
        public byte[] PdfFile { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
       
        public ICollection<CourseLesson> CourseLessons { get; set; }
    }
}