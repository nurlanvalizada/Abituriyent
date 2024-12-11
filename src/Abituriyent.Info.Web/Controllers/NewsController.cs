using System.Linq;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Repository.UOW;
using Abituriyent.Info.Web.MyExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Abituriyent.Info.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public NewsController(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public IActionResult Index(int? pageNumber, string searchPattern)
        {
            ViewBag.searchPattern = searchPattern;
            const int pageSize = 3;
            var paginationParams = this.GetPaginationParams(pageNumber, pageSize, 7,
                _unitOfWorkManager.NewsRepository.Count(n => string.IsNullOrEmpty(searchPattern) || n.Title.Contains(searchPattern)));

            ViewBag.PageParams = paginationParams;

            var newses = _unitOfWorkManager.NewsRepository.FindNewsWithoutFullContent(
                n => string.IsNullOrEmpty(searchPattern) || n.Title.Contains(searchPattern))
                .Skip((paginationParams.Item3 - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            foreach (var news in newses)
            {
                var admin = _unitOfWorkManager.Repository<Admin>().GetSingle(a => a.Id == news.AdminId, a => a.Person);
                news.Admin = admin;
            }

            return View(newses.OrderByDescending(news => news.PublishDate));
        }

        public IActionResult ReadDetailed(int id)
        {
            var news = _unitOfWorkManager.NewsRepository.GetSingle(n => n.Id == id, n => n.Admin.Person);
            if (news == null) return BadRequest();
            return View(news);
        }
    }
}