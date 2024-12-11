using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.Core.Models;
using Abituriyent.Info.Web.Models.AccountViewModels;
using Abituriyent.Info.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Abituriyent.Info.Repository.UOW;
using Abituriyent.Info.Web.Models.ViewModels;
using Abituriyent.Info.Web.MyExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Abituriyent.Info.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILoggerFactory loggerFactory,
            IUnitOfWorkManager unitOfWorkManager,
            IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _unitOfWorkManager = unitOfWorkManager;
            _hostingEnvironment = hostingEnvironment;
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result =
                    await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Uğursuz giriş cəhdi.");
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var model = new RegisterViewModel
            {
                Groups = new SelectList(_unitOfWorkManager.Repository<Group>().GetAll().Select(g => new { g.Id, g.Name }), "Id", "Name"),
                Cities = new SelectList(_unitOfWorkManager.SamplesRepository.GetAllCities().Select(c => new { c.Id, c.Name }), "Id", "Name")
            };
            ViewBag.Days = this.GetDays();
            ViewBag.Months = this.GetMonths();
            ViewBag.Years = this.GetYears();

            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string requestOrigin, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var person = new Person
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = (Gender)model.Gender,
                    Student = new Student
                    {
                        FatherName = model.FatherName,
                        HomePhone = model.HomePhone,
                        MobilePhone = model.MobilePhone,
                        Address = model.Address,
                        DateOfBirth = new DateTime(model.Year,model.Month,model.Day),
                        SchoolId = model.SchoolId,
                        GroupId = model.GroupId
                    }
                };

                var user = new ApplicationUser {UserName = model.Username, Email = model.Email,LockoutEnabled = false, EmailConfirmed = true, Person = person};
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (requestOrigin == "Facebook")
                    {
                        var info = await _signInManager.GetExternalLoginInfoAsync();
                        if (info!=null)
                        {
                            result = await _userManager.AddLoginAsync(user, info);
                            if (result.Succeeded)
                            {
                                await _signInManager.SignInAsync(user, false);
                                _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
                                return RedirectToLocal(returnUrl);
                            }
                        }
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, false);
                        _logger.LogInformation(3, "User created a new account with password.");
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.RequestOrigin = requestOrigin;
            model.Groups = new SelectList(_unitOfWorkManager.Repository<Group>().GetAll().Select(g => new {g.Id, g.Name}), "Id", "Name");
            model.Cities = new SelectList(_unitOfWorkManager.SamplesRepository.GetAllCities().Select(c => new {c.Id, c.Name}), "Id","Name");
            if (model.CityId > 0)
            {
                model.Schools = new SelectList(_unitOfWorkManager.SamplesRepository.GetScools(model.CityId).Select(c => new { c.Id, c.Name }), "Id", "Name");
            }

            ViewBag.Days = this.GetDays();
            ViewBag.Months = this.GetMonths();
            ViewBag.Years = this.GetYears();

            return View("Register", model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new {ReturnUrl = returnUrl});
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        //
        // GET: /Account/ExternalLoginCallback
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (result.Succeeded)
            {
                _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }

            // If the user does not have an account, then ask the user to create an account.
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["LoginProvider"] = info.LoginProvider;
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var givenName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
            var surName = info.Principal.FindFirstValue(ClaimTypes.Surname);
            var dateOfBirth = info.Principal.FindFirstValue(ClaimTypes.DateOfBirth);
            var gender = info.Principal.FindFirstValue(ClaimTypes.Gender);
            var homePhone = info.Principal.FindFirstValue(ClaimTypes.HomePhone);
            var mobilePhone = info.Principal.FindFirstValue(ClaimTypes.MobilePhone);

            ViewBag.RequestOrigin = "Facebook";

            var registerViewModel = new RegisterViewModel
            {
                Username = email,
                Email = email,
                FirstName = givenName,
                LastName = surName,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                Gender = (byte)(gender != null ? (gender == "male" ? 1 : 2) : 0),
                Groups = new SelectList(_unitOfWorkManager.Repository<Group>().GetAll().Select(g => new { g.Id, g.Name }), "Id", "Name"),
                Cities = new SelectList(_unitOfWorkManager.SamplesRepository.GetAllCities().Select(c => new { c.Id, c.Name }), "Id", "Name")
            };

            if (dateOfBirth!=null)
            {
                var dt = DateTime.Parse(dateOfBirth);
                registerViewModel.Day = dt.Day;
                registerViewModel.Month = dt.Month;
                registerViewModel.Year = dt.Year;
            }

            ViewBag.Days = this.GetDays();
            ViewBag.Months = this.GetMonths();
            ViewBag.Years = this.GetYears();

            return View("Register", registerViewModel);
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,
            string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                    return View("ExternalLoginFailure");
                var user = new ApplicationUser {UserName = model.Email, Email = model.Email};
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user !=null)
                {
                    //For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, HttpContext.Request.Scheme);
                    await _emailSender.SendEmailAsync(model.Email, "Unudulmuş şifrənin bərpası - abituriyent.info",
                       $"Şifrənizi bərpa etmək üçün aşağıdakı keçiddən istifadə edin:<br/> <a href='{callbackUrl}'>Şifrəni Bərpa Et!</a>");
                    return View("ForgotPasswordConfirmation");
                }
                ModelState.AddModelError("Email", "Daxil edilən email ünvanına uyğun istifadəçi tapılmadı");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return RedirectToAction(nameof(ResetPasswordConfirmation), "Account");
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
                return RedirectToAction(nameof(ResetPasswordConfirmation), "Account");
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/Edit
        public async Task<ActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            var student = _unitOfWorkManager.StudentRepository.GetSingleWithoutImage(s => s.PersonId == user.PersonId);
            var school = _unitOfWorkManager.Repository<School>().GetSingle(s => s.Id == student.SchoolId);
            var accountEditViewModel = new AccountEditViewModel
            {
                FirstName = student.Person.FirstName,
                LastName = student.Person.LastName,
                FatherName = student.FatherName,
                HomePhone = student.HomePhone,
                MobilePhone = student.MobilePhone,
                Address = student.Address,
                Day = student.DateOfBirth.Day,
                Month = student.DateOfBirth.Month,
                Year = student.DateOfBirth.Year,
                SchoolId = student.SchoolId,
                CityId = school.CityId,
                GroupId = student.GroupId,
                Gender = (byte)student.Person.Gender,
                Username = user.UserName,
                Email = user.Email,
                Groups = new SelectList(_unitOfWorkManager.Repository<Group>().GetAll().Select(g => new { g.Id, g.Name }), "Id", "Name"),
                Cities = new SelectList(_unitOfWorkManager.SamplesRepository.GetAllCities().Select(c => new { c.Id, c.Name }), "Id", "Name"),
                Schools = new SelectList(_unitOfWorkManager.SamplesRepository.GetScools(school.CityId), "Id", "Name"),
                Genders = new SelectList(new List<object> { new { Id = 1, Name = "Kişi" }, new { Id = 2, Name = "Qadın" } }, "Id", "Name")
            };

            ViewBag.Days = this.GetDays();
            ViewBag.Months = this.GetMonths();
            ViewBag.Years = this.GetYears();

            return View(accountEditViewModel);
        }

        //
        // POST: /Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountEditViewModel model, IFormFile studentImage)
        {
            if (ModelState.IsValid && (studentImage?.Length ?? 0) <= 1 * 1024 * 1024)
            {
                var user = await _userManager.GetUserAsync(User);
                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    user.UserName = model.Username;
                    user.Email = model.Email;

                    var student = _unitOfWorkManager.StudentRepository.GetSingleWithoutImage(s => s.PersonId == user.PersonId);

                    student.FatherName = model.FatherName;
                    student.HomePhone = model.HomePhone;
                    student.MobilePhone = model.MobilePhone;
                    student.Address = model.Address;
                    student.DateOfBirth = new DateTime(model.Year, model.Month, model.Day);
                    student.SchoolId = model.SchoolId;
                    student.GroupId = model.GroupId;

                    student.Person.FirstName = model.FirstName;
                    student.Person.LastName = model.LastName;
                    student.Person.Gender = (Gender)model.Gender;

                    var shouldUpdateImage = true;

                    if (studentImage?.Length > 0)
                    {
                        byte[] buffer;
                        using (var stream = studentImage.OpenReadStream())
                        {
                            buffer = new byte[stream.Length];
                            stream.Read(buffer, 0, (int)stream.Length);
                        }

                        student.ImageContentType = studentImage.ContentType;
                        student.Image = buffer;
                    }
                    else
                    {
                        shouldUpdateImage = false;
                    }

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        if (shouldUpdateImage)
                            _unitOfWorkManager.StudentRepository.Update(student);
                        else
                            _unitOfWorkManager.StudentRepository.UpdateExceptImage(student);
                        _unitOfWorkManager.Commit();
                        return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                }
                else
                {
                    ModelState.AddModelError("Password", "Daxil etdiyiniz şifrə yalnışdır");
                }
            }
            else if(studentImage?.Length > 1 * 1024 * 1024)
            {
                ViewBag.FileSizeInvalid = "Profil şəklinin həcmi 1 Mb -dan az olmalıdır";
            }

            model.Groups = new SelectList(_unitOfWorkManager.Repository<Group>().GetAll().Select(g => new {g.Id, g.Name}), "Id", "Name");
            model.Cities = new SelectList(_unitOfWorkManager.SamplesRepository.GetAllCities().Select(c => new {c.Id, c.Name}), "Id", "Name");
            model.Schools = new SelectList(_unitOfWorkManager.SamplesRepository.GetScools(model.CityId), "Id", "Name");
            model.Genders = new SelectList(new List<object> {new {Id = 1, Name = "Kişi"}, new {Id = 2, Name = "Qadın"}}, "Id", "Name");

            ViewBag.Days = this.GetDays();
            ViewBag.Months = this.GetMonths();
            ViewBag.Years = this.GetYears();

            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult ViewStudentImage(int? id)
        {
            return id == null
                ? this.ViewCurrentStudentImage(_userManager, _unitOfWorkManager.StudentRepository, _hostingEnvironment.WebRootPath)
                : this.ViewStudentImage(id.Value, _unitOfWorkManager.StudentRepository, _hostingEnvironment.WebRootPath);
        }

        //
        // GET: /Account/Rating
        public async Task<ActionResult> Rating(int examId)
        {
            var student =  this.GetStudent(_userManager, _unitOfWorkManager.StudentRepository);
            var exams = await _unitOfWorkManager.Repository<StudentExam>().FindBy(se => se.StudentId == student.Id,se => se.GroupExam.Exam).ToListAsync();

            student.Group = _unitOfWorkManager.Repository<Group>().GetSingle(g => g.Id == student.GroupId);

            var ratingViewModel = new RatingViewModel
            {
                Student = student,
                CompletedLessonCount = _unitOfWorkManager.StudentLessonRepository.Count(sl=>sl.StudentId==student.Id && sl.EndDate!=null),
                CompletedExamCount = _unitOfWorkManager.Repository<StudentExam>().Count(sl => sl.StudentId == student.Id && sl.EndTime != null),
                Exams = new SelectList(exams, "GroupExam.Id", "GroupExam.Exam.Name"),
                ExamId=examId
            };

            if (exams != null && exams.Any())
            {
                if (examId == 0) {
                    var studentExams =
       _unitOfWorkManager.Repository<StudentExam>().GetAll(se => se.Student, se => se.Student.Person).
       ToList().GroupBy(se=>se.Student).
       Select(se=>new StudentExam {Student=se.Key,ExamResult=se.Sum(sm=>sm.ExamResult) }).
       OrderByDescending(se => se.ExamResult).ToList();
                ratingViewModel.StudentExams = studentExams;
                } else {
                    var studentExams =
                        await _unitOfWorkManager.Repository<StudentExam>().FindBy(
                                    se => se.GroupExamId == (examId == 0 ? exams.ElementAt(0).GroupExam.ExamId : examId), se => se.Student,se=>se.Student.Person)
                                .OrderByDescending(se => se.ExamResult)
                                .ToListAsync();
                    ratingViewModel.StudentExams = studentExams;
                }
                ViewBag.ExamId = examId == 0 ? exams.ElementAt(0).GroupExam.ExamId : examId;
            }

            return View(ratingViewModel);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion
    }
}