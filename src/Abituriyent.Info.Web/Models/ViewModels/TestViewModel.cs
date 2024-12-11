using System.Collections.Generic;
using Abituriyent.Info.Core.Models;

namespace Abituriyent.Info.Web.Models.ViewModels
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public TestType TestType { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}