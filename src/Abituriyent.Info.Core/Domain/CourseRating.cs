namespace Abituriyent.Info.Core.Domain
{
    public class CourseRating : Entity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public byte Stars { get; set; }
        public string Comment { get; set; }
    }
}