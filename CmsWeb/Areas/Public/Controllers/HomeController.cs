using CmsDataAccess;
using CmsWeb.Areas.Auth.Controllers;
using CmsWeb.Hubs;
using CmsWeb.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using System.Reflection.Metadata;

namespace CmsWeb.Areas.Public.Controllers
{
    [Area("Public")]
    public class HomeController : Controller
    {

        private readonly ISession session;

        private readonly IStringLocalizer<CmsResources.Messages> _localizer;

        private readonly IEmailSender _emailSender;



        private readonly ILogger<LoginController> _logger;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        private readonly IConfiguration _configuration;

        private readonly IHubContext<MyHub> _hubContext1;

        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext cmsContext;


        private IHubContext<MyHub> _hubContext
        { get; set; }

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
            IConfiguration configuration,

            ILogger<LoginController> logger)
        {
            _configuration= configuration;
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


        public IActionResult Index()
        {
            ViewBag.msg = "";

            return View();
        }


        public IActionResult OurServices()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OurServices(string name, string mail, string phone, string obs, string serviceType)
        {


            if (string.IsNullOrEmpty(obs))
            {
                obs = "No thing";
            }



            int indx = 0;

            if (!string.IsNullOrEmpty(serviceType)) {
                indx=int.Parse(serviceType);
            }

            List<string> services = new List<string> {"Not specified", "Airline Reservations", "Hotel / Apartment Reservations", "Luxury Travel services", "Honeymoon packages", "Tour Packages" };

            string subject = "New Booking Details";


            string body = $@"
        <center>  <img src='{_configuration.GetValue<string>("ApiUrl")}public/images/footerLogo.png'>  </center>
        <h2>A new trip request was made.</h2>
        <p>Below are the details of your client:</p>
        <ul>
            <li><b>Name:</b> {name}</li>    
            <li><b>Email:</b> {mail}</li>
            <li><b>Phone Number:</b> {phone}</li> 
            <li><b>Obeservations:</b> {obs}</li>           
            <li><b>Service Type:</b> {services[indx]}</li>     
        </ul>
        <center>  <img src='{_configuration.GetValue<string>("ApiUrl")}public/images/mailFooter.png'>  </center>

";

            var d = _emailSender.SendEmailAsync("bookings@alketbitravelofficial.ae", subject, body);


            ViewBag.msg = _localizer["ThanksMSG"];







            cmsContext.NewRequest.Add(
                new CmsDataAccess.Models.NewRequest
                {
                    RequestType= indx,
                    ClientName=name,
                    ClientEmail=mail,
                    ClientObservation= obs,
                    ClientPhone=phone,
                    CreateDate= _userService.getGulfTime()
                }
            );

            cmsContext.SaveChanges();


            await _hubContext.Clients.All.SendAsync("RefreshAdminOrderGrid_", "ddd");

            //return RedirectToAction("Index");
            return View();
        }

        public IActionResult Partners()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}
