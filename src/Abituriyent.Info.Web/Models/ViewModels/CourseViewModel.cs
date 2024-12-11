using System.Collections.Generic;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abituriyent.Info.Web.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string Description { get; set; }
        public LessonStatus CourseStatus { get; set; }
        public string ImageUrl { get; set; }
        public int StudentCount { get; set; }
        public int AverageRating { get; set; }
        public Course Course { get; set; }
        public Group Group { get; set; }
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
        public SelectList Teachers { get; set; }
        public SelectList Subjects { get; set; }
        public IEnumerable<CourseLessonViewModel> Lessons { get; set; }
        public IEnumerable<CourseRating> CourseRatings { get; set; }
    }
}