using CmsWeb.Areas.Auth.Controllers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using CmsWeb.Services.UserServices;
using CmsDataAccess;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using CmsDataAccess.Models;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Localization;
using NuGet.Versioning;
using System.ComponentModel.DataAnnotations;
using CmsResources;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using CmsDataAccess.Repositories;
using System.IO;
using CmsWeb.Utils.FileUtils;
using System.Collections.Generic;
using Azure.Core;
using System.Numerics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CmsWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class EmployeesController : Controller
    {

        private readonly IStringLocalizer<CmsResources.Messages> _localizer;


        private readonly ILogger<LoginController> _logger;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private RoleManager<IdentityRole> _roleManager;

        private readonly IEmailSender _emailSender;


        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext cmsContext;


        public EmployeesController(
           IStringLocalizer<CmsResources.Messages> localizer,
           IConfiguration config,
           IUserService userService,
           ApplicationDbContext _cMDbContext,

           UserManager<IdentityUser> userManager,
           IUserStore<IdentityUser> userStore,
           SignInManager<IdentityUser> signInManager,
           IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,

           ILogger<LoginController> logger)
        {
            _localizer = localizer;

            _logger = logger;
            _config = config;
            _userService = userService;
            cmsContext = _cMDbContext;

            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _roleManager = roleManager;

        }


        [AcceptVerbs("Get","Post")]
        public async Task<JsonResult> IsEmployeeEmailUnique(string EmployeeEmail)
        {
            var user = await _userManager.FindByEmailAsync(EmployeeEmail);
            bool isUnique = (user == null);
            return Json(isUnique);
        }

        [HttpGet]
        public async Task<JsonResult> IsEmployeeUserNameUnique(string employeeUserName)
        {
            var user = await _userManager.FindByNameAsync(employeeUserName);
            bool isUnique = (user == null);
            return Json(isUnique);
        }

        #region Specialities

        public IActionResult ListOfDoctorSpeciality()
        {
            var requestCultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            string preferredCulture = requestCultureFeature.RequestCulture.UICulture.Name;

            ViewBag.PreferredCulture = preferredCulture;



            return View();
        }

        public ActionResult Read_Speciality([DataSourceRequest] DataSourceRequest request, string preferredCulture = "en-US")
        {
            //var result = cmsContext.ClinicSpeciality
            //    .Include(a => a.ClinincSpecialityTranslations)
            //    .Where(c => c.ClinincSpecialityTranslations.Any(ct => ct.LangCode == preferredCulture)) // Filter first
            //    .ToList().ToDataSourceResult(request);

            var result = cmsContext.DoctorSpeciality
    .Include(a => a.DoctorSpecialityTranslations)
    //.Where(c => c.ClinincSpecialityTranslations.Any(ct => ct.LangCode == preferredCulture)) // Filter first
    .ToList().ToDataSourceResult(request);

            return Json(result);
        }

        [HttpPost]
        public async Task<object> Create_Speciality([DataSourceRequest] DataSourceRequest request, DoctorSpeciality model)
        {

            if (model.Id == Guid.Empty)
            {
                cmsContext.DoctorSpeciality.Add(
                    new DoctorSpeciality
                    {

                        DoctorSpecialityTranslations = new List<DoctorSpecialityTranslations>
                        {

                                new DoctorSpecialityTranslations{ Description=model.DoctorSpecialityTranslations[0].Description,LangCode="en-Us"},
                                new DoctorSpecialityTranslations{ Description=model.DoctorSpecialityTranslations[1].Description,LangCode="ar"},
                        }

                    }

                    );
            }
            else
            {
                cmsContext.Entry(model).State = EntityState.Modified;
            }

            cmsContext.SaveChanges();

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }


        [HttpPost]
        public async Task<object> Update_Speciality([DataSourceRequest] DataSourceRequest request, DoctorSpeciality model)
        {

           DoctorSpeciality doctorSpeciality = cmsContext.DoctorSpeciality.Include(a => a.DoctorSpecialityTranslations).FirstOrDefault(a => a.Id == model.Id);


            List<DoctorSpecialityTranslations> temp = cmsContext.DoctorSpecialityTranslations.Where(a => a.DoctorSpecialityId == model.Id).ToList();

            cmsContext.DoctorSpecialityTranslations.RemoveRange(temp);

            cmsContext.SaveChanges();



            doctorSpeciality.DoctorSpecialityTranslations = new List<DoctorSpecialityTranslations>
                    {
                            new DoctorSpecialityTranslations{ Description=model.DoctorSpecialityTranslations[0].Description,LangCode="en-Us"},
                            new DoctorSpecialityTranslations{ Description=model.DoctorSpecialityTranslations[1].Description,LangCode="ar"},
                    };

            cmsContext.Entry(doctorSpeciality).State = EntityState.Modified;

            cmsContext.SaveChanges();

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public async Task<object> Destroy_Speciality([DataSourceRequest] DataSourceRequest request, DoctorSpeciality model)
        {
            if (model != null)
            {
                // Perform data deletion logic here (you'll have to handle cascading deletes)

                // For example, deleting the entity:
                cmsContext.DoctorSpeciality.Remove(model);
                cmsContext.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }


        #endregion

 


        #region Employees

        public IActionResult ListOfEmployees()
        {
            var requestCultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            string preferredCulture = requestCultureFeature.RequestCulture.UICulture.Name;

            ViewBag.PreferredCulture = preferredCulture;

            ViewData["Clinics"] = new SelectList(cmsContext.Clinic.Include(a => a.ClinicTranslation)
                .Select(a => new { Id = a.Id, Name = a.ClinicTranslation[0].Description + " " + a.ClinicTranslation[1].Description })
                .ToList(), "Id", "Name");

            return View();
        }

        public ActionResult Read_Employees([DataSourceRequest] DataSourceRequest request, string preferredCulture = "en-US")
        {

            //List<Doctor> doctors = cmsContext.Doctor
            //    .Include(a => a.DoctorBasicInfoTranslations)
            //    .Include(a => a.DoctorSpeciality).ThenInclude(a => a.DoctorSpecialityTranslations).ToList();

            //cmsContext.Doctor.RemoveRange(doctors);
            //cmsContext.SaveChanges();


            var result = cmsContext.CompanyEmployee.OrderByDescending(a=>a.ContractStartDate)
        .ToList().ToDataSourceResult(request);

            

            return Json(result);
        }


        /// <summary>
        /// Create new user.
        /// </summary>
        /// <returns>This function add new user to to the system</returns>
        public IActionResult CreateEmployee()
        {
            ViewBag.DispalyName = "Create Employee";
            ViewBag.PreviousAction = "ListOfEmployees";
            ViewBag.ErrorMessage = "";

            return View();
        }


        /// <summary>
        /// Create new user.
        /// </summary>
        /// <returns>This function add new user to to the system</returns>
        [HttpPost]
        public async Task<object> CreateEmployee(CompanyEmployee model, IEnumerable<IFormFile> ProfileImage,

            string Email,
            string Telephone,
            string UserName,
            string Password

            )
        {

            using (var transaction = cmsContext.Database.BeginTransaction())
            {
                try
                {

                    ViewBag.DispalyName = "Create Employee";
                    ViewBag.PreviousAction = "ListOfEmployees"; ViewBag.ErrorMessage = "";

                    var user = new IdentityUser
                    {
                        Email = model.EmployeeEmail,
                        PhoneNumber = model.EmployeePhone,
                        UserName = model.EmployeeUserName,
                        PhoneNumberConfirmed = true,
                        EmailConfirmed = true,
                    };

                    var resul = await _userManager.CreateAsync(user, model.Password);

                    if (!resul.Succeeded)
                    {
                        string msg = "";
                        foreach (var item in resul.Errors)
                        {
                            msg += item.Description + " ";
                        }
                        ViewBag.ErrorMessage = "Please make sure that the employee username is not used earlier, and password has capital letter, numbers and special character";
                        return View(model);
                    }



                    string uniqueFileName = FileHandler.SaveUploadedFile(model.ImageFile);


                    cmsContext.CompanyEmployee.Add(
                        new CompanyEmployee
                        {
                            FirstName = model.FirstName,
                            MiddleName = model.MiddleName,
                            LastName = model.LastName,
                            CenterAddress = model.CenterAddress,
                            Description = model.Description,
                            NationalCardId = model.NationalCardId,
                            PassportNumber = model.PassportNumber,
                            Nationality = model.Nationality,
                            User = user,
                            EmployeeEmail = model.EmployeeEmail,
                            EmployeePhone = model.EmployeePhone,
                            EmployeeUserName = model.EmployeeUserName,
                            ProfileImage = uniqueFileName,
                            JobCardNumber= model.JobCardNumber,
                            Type= model.Type,
                            ContractStartDate= _userService.getGulfTime(),


                        }
                        );

                    try
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Employee"));

                    }
                    catch
                    {

                    }

                    if(model.Type==0)
                    {
                                                await _userManager.AddToRoleAsync(user, "Admin");

                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Employee");

                    }

                    cmsContext.SaveChanges();
                    transaction.Commit();
                    return RedirectToAction("ListOfEmployees");
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    try
                    {
                        var user1 = await _userManager.FindByNameAsync(model.EmployeeUserName);
                        _userManager.DeleteAsync(user1);
                    }
                    catch { }

                    ViewBag.ErrorMessage = ex.ToString();

                    return View(model);

                }
            }
        }



        /// <summary>
        /// Create new user.
        /// </summary>
        /// <returns>This function add new user to to the system</returns>
        public IActionResult EditEmployee(Guid Id)
        {

            ViewBag.DispalyName = "Edit Employee";
            ViewBag.PreviousAction = "ListOfEmployees";
            ViewBag.ErrorMessage = "";

            CompanyEmployee model = cmsContext.CompanyEmployee
                .Include(a => a.User)
                .Include(a=>a.CenterAddress)
                .FirstOrDefault(a => a.Id == Id);

            return View(model);
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <returns>This function add new user to to the system</returns>
        [HttpPost]
        public async Task<object> EditEmployee(CompanyEmployee model)
        {

            ViewBag.DispalyName = "Edit Employee";
            ViewBag.PreviousAction = "ListOfEmployees";
            ViewBag.ErrorMessage = "";

            using (var transaction = cmsContext.Database.BeginTransaction())
            {
                try
                {
                    CompanyEmployee centerTutor = cmsContext.CompanyEmployee
                .Include(a => a.CenterAddress)
                .Include(a => a.User)
                .FirstOrDefault(a => a.Id == model.Id);

                    if (centerTutor.User.Email != model.User.Email)
                    {
                        centerTutor.User.Email = model.User.Email;
                        var resul1 = await _userManager.UpdateAsync(centerTutor.User);

                        if (!resul1.Succeeded)
                        {
                            string msg = "";

                            foreach (var item in resul1.Errors)
                            {
                                msg += item.Description + " ";
                            }

                            ViewBag.ErrorMessage = "Please make sure that the employee username is not used earlier, and password has capital letter, numbers and special character";

                            return View(model);
                        }

                        centerTutor.User.EmailConfirmed = true;
                    }

                    if (centerTutor.User.UserName != model.User.UserName)
                    {
                        centerTutor.User.UserName = model.User.UserName;
                        var resul2 = await _userManager.UpdateAsync(centerTutor.User);

                        if (!resul2.Succeeded)
                        {
                            string msg = "";
                            foreach (var item in resul2.Errors)
                            {
                                msg += item.Description + " ";
                            }

                            ViewBag.ErrorMessage = "Please make sure that the employee username is not used earlier, and password has capital letter, numbers and special character";

                            return View(model);
                        }
                        centerTutor.User.EmailConfirmed = true;
                    }

                    if (centerTutor.User.PhoneNumber != model.User.PhoneNumber)
                    {
                        centerTutor.User.PhoneNumber = model.User.PhoneNumber;
                        var resul3 = await _userManager.UpdateAsync(centerTutor.User);
                        if (!resul3.Succeeded)
                        {
                            string msg = "";
                            foreach (var item in resul3.Errors)
                            {
                                msg += item.Description + " ";
                            }

                            ViewBag.ErrorMessage = "Please make sure that the employee username is not used earlier, and password has capital letter, numbers and special character";



                            return View(model);
                        }
                        centerTutor.User.PhoneNumberConfirmed = true;
                    }

                    if (model.ImageFile != null)
                    {
                        string uniqueFileName = FileHandler.UpdateProfileImage(model.ImageFile, model.ProfileImage);
                        centerTutor.ProfileImage = uniqueFileName;
                    }

                    centerTutor.FirstName = model.FirstName;
                    centerTutor.MiddleName = model.MiddleName;
                    centerTutor.LastName = model.LastName;
                    centerTutor.CenterAddress = model.CenterAddress;
                    centerTutor.Description = model.Description;
                    centerTutor.NationalCardId = model.NationalCardId;
                    centerTutor.PassportNumber = model.PassportNumber;
                    centerTutor.Nationality = model.Nationality;
                    centerTutor.EmployeeEmail = model.EmployeeEmail;
                    centerTutor.EmployeePhone = model.EmployeePhone;
                    centerTutor.EmployeeUserName = model.EmployeeUserName;
                    centerTutor.JobCardNumber = model.JobCardNumber;

                    if(model.Type!= centerTutor.Type)
                    {
                        if(model.Type==0)
                        {
                            await _userManager.RemoveFromRoleAsync(centerTutor.User, "Employee");
                            await _userManager.AddToRoleAsync(centerTutor.User, "Admin");
                        }
                        else
                        {
                            await _userManager.RemoveFromRoleAsync(centerTutor.User, "Admin");
                            await _userManager.AddToRoleAsync(centerTutor.User, "Employee");
                        }
                    }

                    centerTutor.Type = model.Type;



                    cmsContext.CompanyEmployee.Attach(centerTutor);
                    cmsContext.Entry(centerTutor).State = EntityState.Modified;
                    cmsContext.SaveChanges();
                    transaction.Commit();


                    return RedirectToAction("ListOfEmployees");

                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.ToString();
                    transaction.Rollback();
                    return View(model);
                }

            }

        }



        [HttpPost]
        public async Task<object> Destroy_Employee([DataSourceRequest] DataSourceRequest request, CompanyEmployee model)
        {
            if (model != null)
            {
                // Perform data deletion logic here (you'll have to handle cascading deletes)

                CompanyEmployee temp = cmsContext.CompanyEmployee.Include(a=>a.User)
                    .FirstOrDefault(a => a.Id == model.Id);



                // For example, deleting the entity:
                cmsContext.CompanyEmployee.Remove(temp);
                cmsContext.SaveChanges();

                IdentityUser idu = temp.User;

                await _userManager.DeleteAsync(idu);
                cmsContext.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }


        #endregion

    }
}
