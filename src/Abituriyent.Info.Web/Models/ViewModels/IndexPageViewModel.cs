using System.Collections.Generic;
using Abituriyent.Info.Core.Domain;

namespace Abituriyent.Info.Web.Models.ViewModels
{
    public class IndexPageViewModel
    {
        public IEnumerable<HappyStudent> HappyStudents { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<DataDictionary> OurServices { get; set; }
        public IEnumerable<DataDictionary> OurAboutDatas { get; set; }
    }
}