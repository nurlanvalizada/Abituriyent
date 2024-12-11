using System;
using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class Student : Entity
    {
        public string FatherName { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] Image { get; set; }
        public string ImageContentType { get; set; }

        public int SchoolId { get; set; }
        public School School { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public ICollection<StudentLesson> StudentLessons { get; set; }
        public ICollection<StudentLessonTestAnswer> StudentLessonTestAnswers { get; set; }
        public ICollection<StudentLessonOpenTestAnswer> StudentLessonOpenTestAnswers { get; set; }
        public ICollection<StudentExamTestAnswer> StudentExamTestAnswers { get; set; }
        public ICollection<StudentExamOpenTestAnswer> StudentExamOpenTestAnswers { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
        public ICollection<CourseRating> CourseRatings { get; set; }
    }
}