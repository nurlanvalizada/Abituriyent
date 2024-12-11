using System;
using System.Collections.Generic;
using Abituriyent.Info.Core.Models;

namespace Abituriyent.Info.Core.Domain
{
    public class Exam : Entity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public ExamType ExamType { get; set; }

        public ICollection<GroupExam> GroupExams { get; set; }
    }
}