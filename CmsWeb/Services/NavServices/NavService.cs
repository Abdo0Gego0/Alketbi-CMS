
using System.Linq.Expressions;
using System.Security.Claims;

using CmsWeb.Services.UserServices;
using CmsDataAccess;
using CmsDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Controllers;

namespace CmsWeb.Services.NavServices
{
    public class NavService : INavService
    {
        private readonly IActionContextAccessor _actionContextAccessor;

        public NavService(IActionContextAccessor actionContextAccessor)
        {
            _actionContextAccessor = actionContextAccessor;
        }

        public string GetStylGetStyleMainNav(List<string> controllers, List<string> actions, string activeStyle, string inactiveStyle)
        {
            var routeData = _actionContextAccessor.ActionContext.RouteData.Values;
            var controller = routeData["controller"].ToString();
            var action = routeData["action"].ToString();

            foreach (var ctrl in controllers)
            {
                if (ctrl == controller)
                {
                    if (actions.Contains(action))
                        return activeStyle;
                }
            }

            return inactiveStyle;
        }

        public bool IsNavItemSelected(List<string> controllers, List<string> actions)
        {
            var routeData = _actionContextAccessor.ActionContext.RouteData.Values;
            var controller = routeData["controller"].ToString();
            var action = routeData["action"].ToString();

            foreach (var ctrl in controllers)
            {
                if (ctrl == controller)
                {
                    if (actions.Contains(action))
                        return true;
                }
            }
            return false;
        }


        public List<string> GetSiteMap()
        {
            var routeData = _actionContextAccessor.ActionContext.RouteData.Values;
            string area = routeData["area"].ToString();
            string controller = routeData["controller"].ToString();
            string action = routeData["action"].ToString();
            
            return new List<string> { area,controller, action};
        }


    }
}
