using System;
using System.Collections.Generic;
using System.Linq;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Repository.UOW;
using Abituriyent.Info.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Abituriyent.Info.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private UserManager<ApplicationUser> UserManager { get; }
        public HomeController(IUnitOfWorkManager unitOfWorkManager, UserManager<ApplicationUser> userManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            UserManager = userManager;
        }

        public IActionResult Index()
        {
            var indexPageViewModel = new IndexPageViewModel
            {
                HappyStudents = _unitOfWorkManager.Repository<HappyStudent>().GetAll(),
                Teachers = _unitOfWorkManager.Repository<Teacher>().GetAll(),
                OurServices =
                    _unitOfWorkManager.Repository<DataDictionary>().FindBy(
                        dd => new List<string> { "LessonQuality", "LessonPDFandTest", "examEachMonth" }.Contains(dd.Key)).ToList(),
                OurAboutDatas =
                    _unitOfWorkManager.Repository<DataDictionary>().FindBy(
                        dd => new List<string> { "WhatWeDo", "WhoWeAre", "OurMission" }.Contains(dd.Key)).ToList()
            };
            return View(indexPageViewModel);
        }

        public IActionResult Index2()
        {
            var indexPageViewModel = new IndexPageViewModel
            {
                HappyStudents = _unitOfWorkManager.Repository<HappyStudent>().GetAll(),
                Teachers = _unitOfWorkManager.Repository<Teacher>().GetAll(),
                OurServices =
                    _unitOfWorkManager.Repository<DataDictionary>().FindBy(
                        dd => new List<string> { "LessonQuality", "LessonPDFandTest", "examEachMonth" }.Contains(dd.Key)).ToList(),
                OurAboutDatas =
                    _unitOfWorkManager.Repository<DataDictionary>().FindBy(
                        dd => new List<string> { "WhatWeDo", "WhoWeAre", "OurMission" }.Contains(dd.Key)).ToList()
            };
            return View(indexPageViewModel);
        }

        public IActionResult About()
        {
            var aboutMain = _unitOfWorkManager.Repository<DataDictionary>().GetSingle(dd => dd.Key == "AboutMain");
            var aboutPageViewModel = new AboutPageViewModel
            {
                Teachers = _unitOfWorkManager.Repository<Teacher>().GetAll(),
                AboutMain = new Tuple<string, string>(aboutMain?.Header, aboutMain?.Value),
                WhyWeCases =
                     _unitOfWorkManager.Repository<DataDictionary>().FindBy(
                            dd => new List<string> {"WhyWe", "WhatIsOurWebSite", "OurKeyFeatures"}.Contains(dd.Key))
                        .ToDictionary(dd => dd.Header, dd => dd.Value),
                AchivementsByPercentage =
                     _unitOfWorkManager.Repository<DataDictionary>().FindBy(
                            dd =>
                                new List<string>
                                {
                                    "EntranceAchivements",
                                    "StudentSatisfaction",
                                    "ParentSatisfaction",
                                    "LessonsQuality"
                                }.Contains(dd.Key))
                        .ToDictionary(dd => dd.Header, dd => int.Parse(dd.Value))
            };
            return View(aboutPageViewModel);
        }

        public IActionResult Contact()
        {
            var contactMessage = new ContactMessageViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var user = UserManager.GetUserAsync(User).Result;
                var student = _unitOfWorkManager.StudentRepository.GetSingleWithoutImage(si => si.PersonId == user.PersonId);
                if (student != null)
                {
                    contactMessage.FullName = student.Person.FirstName + " " + student.Person.LastName;
                    contactMessage.Email = user.Email;
                }
            }
            return View(contactMessage);
        }

        [HttpPost]
        public bool Contact(ContactMessageViewModel contactMessageViewModel)
        {
            if (ModelState.IsValid)
            {
                var contactMessage = new ContactMessage
                {
                    FullName = contactMessageViewModel.FullName,
                    Email = contactMessageViewModel.Email,
                    Topic = System.Net.WebUtility.HtmlEncode(contactMessageViewModel.Topic),
                    Message = System.Net.WebUtility.HtmlEncode(contactMessageViewModel.Message),
                    SendDate = DateTime.Now
                };
                _unitOfWorkManager.Repository<ContactMessage>().Add(contactMessage);
                _unitOfWorkManager.Commit();
               return true;
            }
            return false;
        }

        public IActionResult Error(int? id)
        {
            if (id.HasValue && id.Value == 404)
            {
               return View("404");
            }
            return View();
        }

        public IActionResult TermsAndConditions()
        {
            var termAndConditionsDictionary =
                _unitOfWorkManager.Repository<DataDictionary>().FindBy(
                        dd => new List<string> { "TermsAndConditions" }.Contains(dd.Key))
                    .ToDictionary(dd => dd.Header, dd => dd.Value);

            ViewBag.TermsAndConditions = termAndConditionsDictionary.ContainsKey("TermsAndConditions")
                ? termAndConditionsDictionary["TermsAndConditions"]
                : string.Empty;
            return View();
        }
    }
}