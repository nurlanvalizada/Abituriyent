using System;
using System.Collections.Generic;
using Abituriyent.Info.Core.Models;

namespace Abituriyent.Info.Web.Models.ViewModels
{
    public class ExamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public ExamType ExamType { get; set; }
        public IEnumerable<TestViewModel> Tests { get; set; }
    }
}