// https://www.yogihosting.com/aspnet-core-identity-roles/

//https://www.codeproject.com/Articles/5165567/MVC-NET-Core-Dynamic-Role-Based-Authorization

//https://stackoverflow.com/questions/40302117/dynamically-assign-controller-action-permissions-to-roles-in-asp-net-mvc

using CmsDataAccess;
using CmsDataAccess.Models;
using CmsResources;
using CmsWeb.CustomTagHelpers;
using CmsWeb.Services.EmailServices;
using CmsWeb.Services.NavServices;
using CmsWeb.Services.QRServices;
using CmsWeb.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Text;

using CmsWeb.Hubs;
using System.Net;
using CmsWeb.Utils.MyCookies;
using CmsWeb.Services.Chat;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddSingleton<INavService, NavService>();
//builder.Services.AddSignalR();

builder.Services.AddSignalR().AddJsonProtocol(x => x.PayloadSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IQRService, QRService>();
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();


builder.Services.AddScoped<IMyCookie, MyCookie>();


// Database
builder.Services.AddDbContext<ApplicationDbContext>
    (options =>
    {
        options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
        //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:DemoConnection").Value);
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });




// Init Database:

// 1- Roles
ApplicationDbContext db = new ApplicationDbContext();









builder.Services.AddRazorPages()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            var assemblyName = new AssemblyName(typeof(Messages).GetTypeInfo().Assembly.FullName);
            return factory.Create("Messages", assemblyName.Name);
        };
    });


// Add services to the container.
builder.Services.AddControllersWithViews()
				// Maintain property names during serialization. See:
				// https://github.com/aspnet/Announcements/issues/194
				.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());
// Add Kendo UI services to the services container"
builder.Services.AddKendo();

// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    //.AddDefaultUI()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider)
    .AddEntityFrameworkStores<ApplicationDbContext>();


//services.AddRazorPages(options =>
//{
//    options.Conventions.AuthorizePage("/Contact");
//    options.Conventions.AuthorizeFolder("/Private");
//    options.Conventions.AllowAnonymousToPage("/Private/PublicPage");
//    options.Conventions.AllowAnonymousToFolder("/Private/PublicPages");
//});

//options.Conventions.AuthorizeFolder("/Private", "AtLeast21");
//options.Conventions.AuthorizeAreaFolder("Identity", "/Manage");

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
//});


//@if((await AuthorizationService.AuthorizeAsync(User, Model, Operations.Edit)).Succeeded)
//{
//    < p >< a class= "btn btn-default" role = "button"
//        href = "@Url.Action("Edit ", "Document ", new { id = Model.Id })" > Edit </ a ></ p >
//}

//@if((await AuthorizationService.AuthorizeAsync(User, "PolicyName")).Succeeded)
//{
//    < p > This paragraph is displayed because you fulfilled PolicyName.</ p >
//}

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireAdministratorRole",
//         policy => policy.RequireRole("Administrator"));
//});
//[Authorize(Policy = "RequireAdministratorRole")]


//builder.Services.ConfigureApplicationCookie(opts =>
//{
//    opts.AccessDeniedPath = "/Stop/Index";
//});




builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthentication(options =>
{
    // custom scheme defined in .AddPolicyScheme() below


    options.DefaultAuthenticateScheme   ="JWT_OR_COOKIE";
    options.DefaultChallengeScheme      ="JWT_OR_COOKIE";
    options.DefaultScheme               ="JWT_OR_COOKIE";

})
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = new PathString("/Auth/Login/Index");
        options.ExpireTimeSpan = TimeSpan.FromDays(15);
        options.AccessDeniedPath = new PathString("/Auth/Login/Index");

    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("jwt:Key").Value)),
            ValidIssuer = builder.Configuration.GetSection("jwt:Issuer").Value,
            ValidAudience = builder.Configuration.GetSection("jwt:Audience").Value,

            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
        };
    })
    // this is the key piece!
    .AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
    {
        // runs on each request
        options.ForwardDefaultSelector = context =>
        {

            return "Cookies";


            // filter by auth type
            string authorization = context.Request.Headers[HeaderNames.Authorization];
            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                return "Bearer";
            // otherwise always check for cookie auth
            return "Cookies";
        };
    });

var app = builder.Build();

var supportedCultures = new string[] { "en-US", "ar" };

var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), @"DesignFiles\\KendoUi")),
    RequestPath = new PathString("/KendoUi"),
    ServeUnknownFileTypes = true,
});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), @"DesignFiles\\AdminDashboard")),
    RequestPath = new PathString("/AdminDashboard"),
    ServeUnknownFileTypes = true,
});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), @"DesignFiles\\mnm")),
    RequestPath = new PathString("/mnm"),
    ServeUnknownFileTypes = true,
});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), @"DesignFiles\\RegisterationForm")),
    RequestPath = new PathString("/RegisterationForm"),
    ServeUnknownFileTypes = true,
});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), @"DesignFiles\\Auth")),
    RequestPath = new PathString("/Auth"),
    ServeUnknownFileTypes = true,
});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), @"DesignFiles\\NavBar")),
    RequestPath = new PathString("/nb"),
    ServeUnknownFileTypes = true,
});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), @"DesignFiles\\Admin")),
    RequestPath = new PathString("/admin"),
    ServeUnknownFileTypes = true,
});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), @"DesignFiles\\public")),
    RequestPath = new PathString("/public"),
    ServeUnknownFileTypes = true,
});


app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), @"DesignFiles")),
    RequestPath = new PathString("/GeneralStyles"),
    ServeUnknownFileTypes = true,
});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration.GetSection("Images:PathToImages").Value.ToString())),
    RequestPath = new PathString("/pImages"),
    ServeUnknownFileTypes = true,
});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration.GetSection("Files:PathToFiles").Value.ToString())),
    RequestPath = new PathString("/pFiles"),
    ServeUnknownFileTypes = true,
});

app.UseStaticFiles();

app.UseRouting();

app.UseSession();


app.UseAuthentication();

app.UseAuthorization();

app.MapHub<MyHub>("/chatHub");

//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

ControllerEndpointRouteBuilderExtensions.MapControllerRoute(app, "areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}", (object)new
{
	area = "Public",
	controller = "Home",
	action = "Index"
});


//app.MapControllerRoute(
app.MapRazorPages();


app.Run();