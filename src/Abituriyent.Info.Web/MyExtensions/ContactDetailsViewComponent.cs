using System.Collections.Generic;
using System.Threading.Tasks;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Repository.UOW;
using Abituriyent.Info.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Abituriyent.Info.Web.MyExtensions
{
    public class ContactDetailsViewComponent : ViewComponent
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ContactDetailsViewComponent(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var contactDetailDictionary =
                await _unitOfWorkManager.Repository<DataDictionary>().FindBy(
                        dd =>
                            new List<string> {"OurWebSite", "OurEmail", "OurContactPhone", "OurLocation"}.Contains(
                                dd.Key))
                    .ToDictionaryAsync(dd => dd.Key, dd => dd.Value);

            var contactDetails = new ContactDetailsViewModel
            {
                OurWebSite =
                    contactDetailDictionary.ContainsKey("OurWebSite") ? contactDetailDictionary["OurWebSite"] : string.Empty,
                OurEmail = contactDetailDictionary.ContainsKey("OurEmail") ? contactDetailDictionary["OurEmail"] : string.Empty,
                OurPhone =
                    contactDetailDictionary.ContainsKey("OurContactPhone")
                        ? contactDetailDictionary["OurContactPhone"]
                        : string.Empty,
                OurLocation =
                    contactDetailDictionary.ContainsKey("OurLocation") ? contactDetailDictionary["OurLocation"] : string.Empty
            };

            return View(contactDetails);
        }
    }
}