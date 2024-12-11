using System.Linq;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Repository.UOW;
using Abituriyent.Info.Web.MyExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abituriyent.Info.Web.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MyApiController : Controller
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private UserManager<ApplicationUser> UserManager { get; }

        public MyApiController(IUnitOfWorkManager unitOfWorkManager, UserManager<ApplicationUser> userManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            UserManager = userManager;
        }

        [HttpGet]
        [Route("[action]")]
        public SelectList GetSchools([FromQuery]int cityId)
        {
            return new SelectList(_unitOfWorkManager.SamplesRepository.GetScools(cityId).Select(s => new { s.Id, s.Name }), "Id", "Name");
        }

        [HttpPost]
        [Route("[action]")]
        public bool RateCourse([FromForm]int courseId, [FromForm]byte rateValue, [FromForm]string rateComment)
        {
            var student = this.GetStudent(UserManager, _unitOfWorkManager.StudentRepository);
            var courseRating = _unitOfWorkManager.Repository<CourseRating>().GetSingle(cr => cr.StudentId == student.Id && cr.CourseId == courseId);
            if (courseRating==null)
            {
                _unitOfWorkManager.Repository<CourseRating>().Add(new CourseRating
                {
                    StudentId = student.Id,
                    CourseId = courseId,
                    Stars = rateValue,
                    Comment = rateComment
                });
            }
            else
            {
                courseRating.Stars = rateValue;
                courseRating.Comment = rateComment;
                _unitOfWorkManager.Repository<CourseRating>().Update(courseRating);
            }
            _unitOfWorkManager.Commit();
            return true;
        }
    }
}