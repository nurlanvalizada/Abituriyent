using System;
using System.Collections.Generic;
using Abituriyent.Info.Core.Domain;

namespace Abituriyent.Info.Web.Models.ViewModels
{
    public class AboutPageViewModel
    {
        public IEnumerable<Teacher> Teachers { get; set; }
        public IDictionary<string, int> AchivementsByPercentage { get; set; }
        public Tuple<string,string> AboutMain { get; set; }
        public IDictionary<string, string> WhyWeCases { get; set; }
    }
}