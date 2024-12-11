using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class Course : Entity
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public byte ScoreRate { get; set; }
        public bool Status { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
       
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<CourseLesson> CourseLessons { get; set; }
        public ICollection<CourseRating> CourseRatings { get; set; }
    }
}