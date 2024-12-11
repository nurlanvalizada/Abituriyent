using System.Collections.Generic;
using System.Threading.Tasks;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Repository.UOW;
using Abituriyent.Info.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Abituriyent.Info.Web.MyExtensions
{
    public class SocialViewComponent : ViewComponent
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SocialViewComponent(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var socialDictionary =
                await _unitOfWorkManager.Repository<DataDictionary>().FindBy(
                        dd => new List<string> { "FacebookPage", "TwitterPage", "LinkedinPage"}.Contains(dd.Key))
                    .ToDictionaryAsync(dd => dd.Header, dd => dd.Value);

            var socialViewModel = new SocialViewModel
            {
                FacebookPage =
                    socialDictionary.ContainsKey("FacebookPage") ? socialDictionary["FacebookPage"] : string.Empty,
                TwitterPage = 
                    socialDictionary.ContainsKey("TwitterPage") ? socialDictionary["TwitterPage"] : string.Empty,
                LinkedinPage = 
                    socialDictionary.ContainsKey("LinkedinPage") ? socialDictionary["LinkedinPage"] : string.Empty,
            };

            return View(socialViewModel);
        }
    }
}