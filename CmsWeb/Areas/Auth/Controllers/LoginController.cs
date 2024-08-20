using Kendo.Mvc.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Drawing;

using System.Security.Claims;

using CmsDataAccess;
using CmsResources;
using Microsoft.AspNetCore.Identity.UI.Services;
using CmsWeb.Services.UserServices;
using Azure.Core;
using CmsDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using CmsWeb.Utils.UserAccount;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using NuGet.Common;

namespace CmsWeb.Areas.Auth.Controllers
{
	[Area("Auth")]
	public class LoginController : Controller
	{


        private readonly IStringLocalizer<CmsResources.Messages> _localizer;


        private readonly ILogger<LoginController> _logger;

        private readonly SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        private readonly IEmailSender _emailSender;


        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext cmsContext;

        public LoginController(
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

        public async Task<IActionResult> Index()
		{

            //List<CenterSupervisor> c = cmsContext.CenterSupervisor.Include(a => a.User).ToList();
            //List<CenterTutor> t = cmsContext.CenterTutor.Include(a => a.WorkingHours).Include(a => a.User).ToList();
            //List<Student> s = cmsContext.Student.Include(a => a.User).ToList();
            //List<SysAdmin> sa = cmsContext.SysAdmin.Include(a => a.User).ToList();

            //cmsContext.CenterTutor.RemoveRange(t);
            //cmsContext.CenterSupervisor.RemoveRange(c);
            //cmsContext.Student.RemoveRange(s);
            //cmsContext.SysAdmin.RemoveRange(sa);
            //cmsContext.SaveChanges();

            //var user1 = await _userManager.FindByNameAsync("admin");
            //_userManager.DeleteAsync(user1);

            //var role = await _roleManager.FindByNameAsync("Admin");
            //_roleManager.DeleteAsync(role);

            ////Insert Admin into database


            try
            {
                // First add the user login info
                var user = new IdentityUser
                {
                    Email = "admin@admin.com",
                    PhoneNumber = "0000",
                    UserName = "admin",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                };

                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _userManager.CreateAsync(user, "P@ssw0rd");
                await _userManager.AddToRoleAsync(user, "Admin");

                cmsContext.SysAdmin.Add(
                new SysAdmin
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    MiddleName = "Admin",
                    User = user,
                }
                );

                cmsContext.SaveChanges();

            }
            catch
            {

            }

            ViewBag.ErrorMessage = "";

            return View();
		}


		[HttpPost]
        public async Task<ActionResult> Index(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			{
				ViewBag.ErrorMessage = "Please fill user name and password !!";
				return View();
			}

            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(username);
            }

            if (user != null)
            {

                Person prsn=cmsContext.Person.Include(a=>a.User).FirstOrDefault(a=>a.User.Id==user.Id);
                if (prsn.IsDeleted || !prsn.Status || !prsn.CenterStatus)
                {
                    ViewBag.ErrorMessage = "Wrong login info";
                    return View();
                }


                var result = await _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false);


                if (result.Succeeded)
                {

                    var roles=await _userManager.GetRolesAsync(user);

                    foreach (var item in roles)
                    {
                        string tem = item.ToString().ToLower();
                        switch (tem)
                        {
                            case "admin":



                                //https://weblog.west-wind.com/posts/2022/Mar/29/Combining-Bearer-Token-and-Cookie-Auth-in-ASPNET

                                Person person = cmsContext.Person.Include(a => a.User)
                                    .FirstOrDefault(a => a.User.Id == user.Id);

                                List<Claim> claims = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                                    new Claim(ClaimTypes.SerialNumber, user.Id.ToString()),
                                    new Claim("FirstName", person.FirstName),
                                    new Claim(ClaimTypes.Name, person.FullName ),
                                    new Claim(ClaimTypes.Email, user.Email),
                                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                                    new Claim(ClaimTypes.Role, "Admin"),
                                };

                                            // also add cookie auth for Swagger Access
                               var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                                identity.AddClaims(claims);


                                var principal = new ClaimsPrincipal(identity);
                                await HttpContext.SignInAsync(
                                    CookieAuthenticationDefaults.AuthenticationScheme,
                                    principal,
                                    new AuthenticationProperties
                                    {
                                        IsPersistent = true,
                                        AllowRefresh = true,
                                        ExpiresUtc = DateTime.UtcNow.AddDays(1)
                                    });


                                return RedirectToAction("Index","Home",new { area="Admin"});

                            case "employee":


                                //https://weblog.west-wind.com/posts/2022/Mar/29/Combining-Bearer-Token-and-Cookie-Auth-in-ASPNET

                                Person person1 = cmsContext.Person.Include(a => a.User)
                                    .FirstOrDefault(a => a.User.Id == user.Id);

                                CompanyEmployee person2 = cmsContext.CompanyEmployee.Include(a => a.User)
                                    .FirstOrDefault(a => a.User.Id == user.Id);

                                List<Claim> claims1 = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                                    new Claim(ClaimTypes.SerialNumber, user.Id.ToString()),
                                    new Claim("FirstName", person1.FirstName),
                                    new Claim("Id", person1.Id.ToString()),
                                    new Claim("Type", person2.Type.ToString()),
                                    new Claim(ClaimTypes.Name, person1.FullName ),
                                    new Claim(ClaimTypes.Email, user.Email),
                                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                                    new Claim(ClaimTypes.Role, "Employee"),
                                };

                                // also add cookie auth for Swagger Access
                                var identity1 = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                                identity1.AddClaims(claims1);


                                var principal1 = new ClaimsPrincipal(identity1);
                                await HttpContext.SignInAsync(
                                    CookieAuthenticationDefaults.AuthenticationScheme,
                                    principal1,
                                    new AuthenticationProperties
                                    {
                                        IsPersistent = true,
                                        AllowRefresh = true,
                                        ExpiresUtc = DateTime.UtcNow.AddDays(1)
                                    });

                                if (person2.Type == 1)
                                {

                                    return RedirectToAction("Index", "Home", new { area = "Employee" });
                                }
                                else
                                {
                                    return RedirectToAction("LiveChatGroups", "Home", new { area = "Employee" });
                                }

                            default:
                                ViewBag.ErrorMessage = "Wrong login info"; ;
                                return View();
                        }
                    }

                    ViewBag.ErrorMessage = "Wrong login info"; ;
                    return View();


                }
                else
                {
                    ViewBag.ErrorMessage = "Wrong login info"; 
                    return View();

                }
            }
            else
            {
                ViewBag.ErrorMessage = "Wrong login info"; 
                return View();
            }

        }


        public IActionResult ForgotPassword(string? msg)
        {

            ViewBag.ErrorMessage =string.IsNullOrEmpty(msg)? "":msg;

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> renewPassword(string username,string? msg)
        {
            try
            { 

                ViewBag.ErrorMessage = "";
                ViewBag.UserName = username;

                string token = new PasswordUtil(_userManager).CreateRandomTokenN(5);


                var user = await _userManager.FindByEmailAsync(username);
                if (user == null)
                {
                    user = await _userManager.FindByNameAsync(username);
                }

                if (user == null)
                {
                    return RedirectToAction("ForgotPassword", new { msg= "Make sure that the e-mail or the username is correct" });
                }


                try
                {


                    string subject = "Reset passsword";
                    string body = $@"
            <center>  <img src='{_config.GetValue<string>("ApiUrl")}public/images/footerLogo.png'>  </center>

    <h2>Please use the following token to reset password:<br/> {token}</h2></ul>
            <center>  <img src='{_config.GetValue<string>("ApiUrl")}public/images/mailFooter.png'>  </center>

    ";
                    var d = _emailSender.SendEmailAsync(user.Email, subject, body);
                }
                catch
                {

                }



                cmsContext.UserResetToken.Add(new UserResetToken
                {
                    UserName= username,
                    Token= token
                });

                cmsContext.SaveChanges();

                return View();
            }
            catch
            {
                return View("ForgotPassword");

            }
        }

        public async Task<IActionResult> saveNewPassword(string username,string usrToken,string Password,string repPassword)
        {

            try
            {

 
                var user = await _userManager.FindByEmailAsync(username);
                if (user == null)
                {
                    user = await _userManager.FindByNameAsync(username);
                }

                // check token 

                bool isTokenValid = (cmsContext.UserResetToken
                    .Where(a=>a.UserName==user.UserName
                    && a.Token==usrToken
                    && DateTime.Now<=a.EndDate
                    ).Any());


                if(!isTokenValid)
                {
                    ViewBag.ErrorMessage = "Either token is not correct or it is expired";
                    return View("renewPassword");
                }

                if(Password!= repPassword)
                {
                    ViewBag.ErrorMessage = "Passwords fields must match";
                    return View("renewPassword");
                }

                List<UserResetToken> tokens = cmsContext.UserResetToken
                    .Where(a => a.UserName == user.UserName
                    ).ToList();

                cmsContext.UserResetToken.RemoveRange(tokens);
                cmsContext.SaveChanges();

                string token = await _userManager.GeneratePasswordResetTokenAsync(user);

                await _userManager.ResetPasswordAsync(user,token,repPassword);

                // if token is not correct redirect to renewPassword

                ViewBag.ErrorMessage = "";
                return RedirectToAction("Index");
            }
            catch
            {
                return View("ForgotPassword");
            }
        }


        public async Task<IActionResult> logOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }


    }
}
