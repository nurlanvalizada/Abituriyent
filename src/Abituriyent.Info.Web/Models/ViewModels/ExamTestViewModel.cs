using System.Collections.Generic;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Core.Models;

namespace Abituriyent.Info.Web.Models.ViewModels
{
    public class ExamTestViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public TestType TestType { get; set; }
        public bool isCorrect { get; set; }
        public Answer correctAnswer { get; set; }
        public Answer studentAnswer { get; set; }

        public IEnumerable<Answer> Answers { get; set; }

    }
}