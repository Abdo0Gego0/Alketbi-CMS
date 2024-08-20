
using System.Linq.Expressions;
using System.Security.Claims;

using CmsWeb.Services.UserServices;
using CmsDataAccess;
using CmsDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CmsWeb.Services.UserServices
{
    public class UserService: IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext cMDbContext;

        public UserService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext _cMDbContext )
        {
            _httpContextAccessor= httpContextAccessor;
			cMDbContext = _cMDbContext;
        }
        

        public DateTime getGulfTime()
        {
            DateTime date1 = DateTime.UtcNow;

            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time");

            DateTime date2 = TimeZoneInfo.ConvertTime(date1, tz);
            return date2;
        }

        public object GetPatientInfo()
        {
            if(_httpContextAccessor !=null)
            {
                var result1 = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result2 = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                var result3 = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                var result4 = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.MobilePhone);
                var result5 = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
				return new { UserName=result1, FullName=result2, Email=result3, PhoneNumber=result4,Role=result5 };
            }
            return null;
        }

        public string GetUserFirstName()
        {
            if (_httpContextAccessor != null)
            {
                return _httpContextAccessor.HttpContext.User.FindFirstValue("FirstName");
            }
            return null;
        }


        public string GetUserName()
        {
            if (_httpContextAccessor != null)
            {
                return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return null;
        }

        public object GetPatientDetails()
		{
			if (_httpContextAccessor != null)
			{
                try
                {
                    Guid UserId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.SerialNumber));

                    Patient patient= cMDbContext.Patient.Include(a=>a.User).FirstOrDefault(p => p.Id == UserId); ;


					return (object)new
                    {
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        BirthDate = patient.BirthDate,
						NationalCardId = patient.NationalCardId,
						InsuranceCompany = patient.InsuranceCompany,
						InsuranceId = patient.InsuranceId,
						Gendre = patient.Gendre,
						BloodType = patient.BloodType,

						PhoneNumber = patient.User.PhoneNumber,
						UserName = patient.User.UserName,
                        Email = patient.User.Email,


					};
                }
                catch
                {
                    return null;
                }
			}
			return null;
		}

		//public Guid? GetMyId()
  //      {
  //          if (_httpContextAccessor != null)
  //          {
  //              try
  //              {
  //                  return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.SerialNumber));
  //              }
  //              catch
  //              { return null; }
  //          }
  //          return null;
  //      }


        public Guid? GetMyId()
        {
            if (_httpContextAccessor != null)
            {
                try
                {
                    return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("Id"));
                }
                catch
                { return null; }
            }
            return null;
        }

        public int? GetJobType()
        {
            if (_httpContextAccessor != null)
            {
                try
                {
                    return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("Type"));
                }
                catch
                { return null; }
            }
            return null;
        }
        public string GetMyRole()
        {
            if (_httpContextAccessor != null)
            {
                return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            return null;
        }

		public string GetMyLanguage()
		{   
			if (_httpContextAccessor != null)
			{
				return _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
			}
			return "en";
		}


        public Guid GetMyCenterId()
        {
            Guid userId = (Guid)GetMyId();
            string userRole = GetMyRole().ToLower();

            if(userRole=="tutor")
            {
                return cMDbContext.CenterTutor.Include(a=>a.User).FirstOrDefault(a => a.User.Id == userId.ToString()).MarsCenterId;
            }

            if(userRole== "supervisor")
            {
                return cMDbContext.CenterSupervisor.Include(a=>a.User).FirstOrDefault(a => a.User.Id == userId.ToString()).MarsCenterId;
            }

            return new Guid();
        }


	}
}
