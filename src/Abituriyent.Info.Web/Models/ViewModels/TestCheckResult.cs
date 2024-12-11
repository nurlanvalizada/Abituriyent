using System.Collections.Generic;
using Abituriyent.Info.Core.Models;

namespace Abituriyent.Info.Web.Models.ViewModels
{
    public class TestCheckResult
    {
        public IEnumerable<TestCorrectness> Results { get; set; }
        public int TotalScore { get; set; }
    }
}