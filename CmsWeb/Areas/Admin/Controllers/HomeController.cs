using CmsWeb.Areas.Auth.Controllers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using CmsWeb.Services.UserServices;

using CmsDataAccess;
using Microsoft.AspNetCore.Localization;
using CmsDataAccess.Models;
using CmsWeb.Hubs;
using Azure.Identity;
using Microsoft.AspNetCore.SignalR;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using CmsDataAccess.Enums;
using Microsoft.AspNetCore.Authorization;

namespace CmsWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class HomeController : Controller
    {

        private readonly IStringLocalizer<CmsResources.Messages> _localizer;


        private readonly ILogger<LoginController> _logger;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        private readonly IEmailSender _emailSender;

        private readonly IHubContext<MyHub> _hubContext;

        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext cmsContext;

        public HomeController(
            IStringLocalizer<CmsResources.Messages> localizer,
            IConfiguration config,
            IUserService userService,
            ApplicationDbContext _cMDbContext,

            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender,
            IHubContext<MyHub> hubContext,

            ILogger<LoginController> logger)
        {

            _hubContext = hubContext;
            _localizer = localizer;

            _logger = logger;
            _config = config;
            _userService = userService;
            cmsContext = _cMDbContext;

            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;

            _emailSender = emailSender;

        }

        public async Task<IActionResult> Index()
        {


            //List<ChatMessages> ChatMessagess = cmsContext.ChatMessages.ToList();

            //cmsContext.ChatMessages.RemoveRange(ChatMessagess);
            //cmsContext.SaveChanges();

            //List<ConnectionGroup> connectionGroups = cmsContext.ConnectionGroup.ToList();

            //cmsContext.ConnectionGroup.RemoveRange(connectionGroups);
            //cmsContext.SaveChanges();


            //List<NewRequest> connectiNewRequestonGroups = cmsContext.NewRequest.ToList();

            //cmsContext.NewRequest.RemoveRange(connectiNewRequestonGroups);
            //cmsContext.SaveChanges();


            //foreach (var item in connectionGroups)
            //{
            //    await _hubContext.Clients.All.SendAsync("AddNewGroup", "cccc", item.GroupName);
            //}

            var requestCultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            string preferredCulture = requestCultureFeature.RequestCulture.UICulture.Name;

            ViewBag.PreferredCulture = preferredCulture;

            var requestStats = cmsContext.NewRequest
    .GroupBy(e => e.CreateDate.Year)
    .Select(g => new RequestStat { Year = g.Key, Count = g.Count() })
    .ToList();

            if (requestStats.Count < 5)
            {
                // Generate dummy data for the remaining years
                var currentYear = DateTime.Now.Year;

                for (int year = currentYear; year > currentYear - 5; year--)
                {
                    // Check if the year is not already in the result
                    if (!requestStats.Any(stat => stat.Year == year))
                    {
                        // Add dummy data with count 0
                        requestStats.Add(new RequestStat { Year = year, Count = 0 });
                    }
                }

                // Order the result by year
                requestStats = requestStats.OrderBy(stat => stat.Year).ToList();
            }

            ViewBag.stats = requestStats;

            return View();
        }


        

        public ActionResult read_stat([DataSourceRequest] DataSourceRequest request)
        {


            var countsPerYear = cmsContext.NewRequest
    .GroupBy(e => e.CreateDate.Year)
    .Select(g => new RequestStat{ Year = g.Key, Count = g.Count() })
    .ToList().ToDataSourceResult(request);

            
            return Json(countsPerYear);
        }


        public ActionResult Read_NewRequests([DataSourceRequest] DataSourceRequest request,int serviceType=-1, string preferredCulture = "en-US")
        {

            if(serviceType<0)
            {
                var result = cmsContext.NewRequest.OrderByDescending(a => a.CreateDate)
            .ToList().ToDataSourceResult(request);


                return Json(result);
            }


            var result1 = cmsContext.NewRequest.Where(a=>a.RequestType==serviceType).OrderByDescending(a => a.CreateDate)
                .ToList().ToDataSourceResult(request);
            return Json(result1);
        }

        public async Task<ActionResult> Update_NewRequests([DataSourceRequest] DataSourceRequest request,NewRequest model)
        {


            NewRequest congroup = cmsContext.NewRequest.FirstOrDefault(a => a.Id == model.Id);

            if(congroup.Status>2)
            {
                return null;
            }


            congroup.Status = 1;

            congroup.CompanyEmployeeId = model.CompanyEmployeeId;

            cmsContext.NewRequest.Attach(congroup);

            cmsContext.Entry(congroup).State = EntityState.Modified;

            cmsContext.SaveChanges();


            await _hubContext.Clients.All.SendAsync("RefreshEmployeeOrderGrid_", "ddd");


            try
            {
        //< center >  < img src = '{_config.GetValue<string>("ApiUrl")}public/images/footerLogo.png' >  </ center >

                string email = cmsContext.CompanyEmployee.Include(a => a.User).FirstOrDefault(a => a.Id == model.CompanyEmployeeId).User.Email;
                string subject = "New Booking Request";
                string body = $@"

<h2>You have a new booking request.</h2></ul>
        <center>  <img src='{_config.GetValue<string>("ApiUrl")}public/images/mailFooter.png'>  </center>

";
                var d = _emailSender.SendEmailAsync(email, subject, body);
            }
            catch
            {

            }







            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }













        public async Task<IActionResult> LiveChatGroups()
        {

            //List<ConnectionGroup> connectionGroups=cmsContext.ConnectionGroup.ToList();

            //foreach (var item in connectionGroups)
            //{
            //    await _hubContext.Clients.All.SendAsync("AddNewGroup", "cccc", item.GroupName);
            //}

            var requestCultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            string preferredCulture = requestCultureFeature.RequestCulture.UICulture.Name;

            ViewBag.PreferredCulture = preferredCulture;



            ViewBag.Employees = new SelectList(new ApplicationDbContext().CompanyEmployee.Select(a => new { Id = a.Id, Name = a.FullName }), "Id", "Name");





            return View();
        }


        public ActionResult Read_Chats([DataSourceRequest] DataSourceRequest request, string preferredCulture = "en-US")
        {



            var result = cmsContext.ConnectionGroup.OrderByDescending(a=>a.CreateDate)
        .ToList().ToDataSourceResult(request);


            return Json(result);
        }
        [HttpPost]

        public async Task<ActionResult> Update_Chats([DataSourceRequest] DataSourceRequest request, ConnectionGroup model,Guid? eId)
        {

            ConnectionGroup congroup = cmsContext.ConnectionGroup.FirstOrDefault(a => a.Id == model.Id);

            congroup.Status = 1;

            congroup.CompanyEmployeeId = model.CompanyEmployeeId;

            cmsContext.ConnectionGroup.Attach(congroup);

            cmsContext.Entry(congroup).State = EntityState.Modified;

            cmsContext.SaveChanges();


            await _hubContext.Clients.All.SendAsync("RefreshEmployeeChatGrid_", "ddd");



            try
            {
        //< center >  < img src = '{_config.GetValue<string>("ApiUrl")}public/images/footerLogo.png' >  </ center >


                string email = cmsContext.CompanyEmployee.Include(a => a.User).FirstOrDefault(a => a.Id == model.CompanyEmployeeId).User.Email;
                string subject = "New Chat Request";
                string body = $@"

<h2>You have a new chat request.</h2></ul>
        <center>  <img src='{_config.GetValue<string>("ApiUrl")}public/images/mailFooter.png'>  </center>

";
                var d = _emailSender.SendEmailAsync(email, subject, body);
            }
            catch
            {

            }





            return Json(new[] { model }.ToDataSourceResult(request, ModelState));

        }

        public async Task<IActionResult> SingleChatPage(string groupName)
        {

            //List<ConnectionGroup> connectionGroups=cmsContext.ConnectionGroup.ToList();

            //foreach (var item in connectionGroups)
            //{
            //    await _hubContext.Clients.All.SendAsync("AddNewGroup", "cccc", item.GroupName);
            //}

            var requestCultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            string preferredCulture = requestCultureFeature.RequestCulture.UICulture.Name;

            ViewBag.PreferredCulture = preferredCulture;

            ViewData["groupName"] = groupName;

            return View();
        }



        [Route("Admin/Home/SingleChatPageHistory/{id}")]
        public async Task<IActionResult> SingleChatPageHistory(string id)
        {


            ViewBag.gId = id;
            //List<ConnectionGroup> connectionGroups=cmsContext.ConnectionGroup.ToList();

            //foreach (var item in connectionGroups)
            //{
            //    await _hubContext.Clients.All.SendAsync("AddNewGroup", "cccc", item.GroupName);
            //}

            var requestCultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            string preferredCulture = requestCultureFeature.RequestCulture.UICulture.Name;

            ViewBag.PreferredCulture = preferredCulture;

            ViewData["groupName"] = id;

            return View();
        }












    }
}
