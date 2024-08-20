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
using CmsWeb.Services.Chat;
using Microsoft.AspNetCore.Authorization;

namespace CmsWeb.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles ="Employee")]
    public class HomeController : Controller
    {

        private readonly IStringLocalizer<CmsResources.Messages> _localizer;


        private readonly ILogger<LoginController> _logger;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        private readonly IEmailSender _emailSender;
        private readonly IChatService chatService;

        private readonly IHubContext<MyHub> _hubContext;

        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext cmsContext;

        public HomeController(
            IChatService chatService_,
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
            chatService = chatService_;
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

            return View();
        }




        public ActionResult Read_NewRequests([DataSourceRequest] DataSourceRequest request,  int serviceType = -1, string preferredCulture = "en-US")
        {

            if (serviceType < 0)
            {
                var result = cmsContext.NewRequest.Where(a=>a.CompanyEmployeeId==_userService.GetMyId()).OrderByDescending(a => a.CreateDate)
            .ToList().ToDataSourceResult(request);


                return Json(result);
            }


            var result1 = cmsContext.NewRequest.Where(a => a.RequestType == serviceType && a.CompanyEmployeeId == _userService.GetMyId()).OrderByDescending(a => a.CreateDate)
                .ToList().ToDataSourceResult(request);
            return Json(result1);
        }

        [HttpPost]
        public async Task<ActionResult> Update_NewRequests([DataSourceRequest] DataSourceRequest request, NewRequest model)
        {


            NewRequest congroup = cmsContext.NewRequest.FirstOrDefault(a => a.Id == model.Id);

            if (congroup.Status > 1)
            {
                return null;
            }


            congroup.Status = 2;


            cmsContext.NewRequest.Attach(congroup);

            cmsContext.Entry(congroup).State = EntityState.Modified;

            cmsContext.SaveChanges();


            await _hubContext.Clients.All.SendAsync("RefreshEmployeeOrderGrid_", "ddd");
            await _hubContext.Clients.All.SendAsync("RefreshAdminOrderGrid_", "ddd");


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



            var result = cmsContext.ConnectionGroup.OrderByDescending(a => a.CreateDate).Where(a => a.CompanyEmployeeId == _userService.GetMyId())
        .ToList().ToDataSourceResult(request);


            return Json(result);
        }
        [HttpPost]

        public ActionResult Update_Chats([DataSourceRequest] DataSourceRequest request, ConnectionGroup model, Guid? eId)
        {

            ConnectionGroup congroup = cmsContext.ConnectionGroup.FirstOrDefault(a => a.Id == model.Id);

            congroup.Status = 1;

            congroup.CompanyEmployeeId = model.CompanyEmployeeId;

            cmsContext.ConnectionGroup.Attach(congroup);

            cmsContext.Entry(congroup).State = EntityState.Modified;

            cmsContext.SaveChanges();


            return Json(new[] { model }.ToDataSourceResult(request, ModelState));

        }

        [Route("Employee/Home/SingleChatPage/{id}")]

        public async Task<IActionResult> SingleChatPage(string id)
        {




            ViewBag.GroupName = id;
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


        [Route("Employee/Home/SingleChatPageHistory/{id}")]
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



        [HttpPost]
        public async Task<ActionResult> End_Conversation(string gId)
        {
            int xx = 1;
            xx++;

            ConnectionGroup congroup = cmsContext.ConnectionGroup.FirstOrDefault(a => a.Id == chatService.GetGroupId(gId));

            congroup.Status = 2;

            congroup.GroupOldName = congroup.GroupName;

            congroup.GroupName = "Chat: ("+ cmsContext.ConnectionGroup.Count().ToString()+ ")";

            cmsContext.ConnectionGroup.Attach(congroup);

            cmsContext.Entry(congroup).State = EntityState.Modified;

            cmsContext.SaveChanges();

            await _hubContext.Clients.All.SendAsync("RefreshEmployeeChatGrid_", "ddd");
            await _hubContext.Clients.All.SendAsync("RefreshAdminChatGrid_", "ddd");

            //await new MyHub(chatService).RemoveGroubFromHub(congroup.GroupOldName);

            return Json("OK");

        }


      










    }
}
