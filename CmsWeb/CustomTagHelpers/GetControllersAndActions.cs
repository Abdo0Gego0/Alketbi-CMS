using CmsDataAccess;
using CmsDataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CmsWeb.CustomTagHelpers
{

    public class ReturnedActions
    {
        public ReturnedActions() { }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ReturnType { get; set; }
        public string Attributes { get; set; }
        public string Area { get; set; }
        public string DisplayName { get; set; } = "";
        public string Documentation { get; set; }="";

    }

    public static class GetControllersAndActions
    {
        public static List<string> GetAllControllerActions()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var controllerActions = assembly.GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                
                )
                .Where(method => !method.IsSpecialName)
                .Select(method => $"{method.DeclaringType?.Name.Replace("Controller", "")} --> {method.Name}")
                .ToList();

            return controllerActions;
        }
        public static List<ReturnedActions> GetAllControllerActionsWithAttributes()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            //List<ReturnedActions> controlleractionlist = asm.GetTypes()
            //        .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
            //        .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            //        .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
            //        .Select(x => new ReturnedActions  { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
            //        .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();


            List<ReturnedActions> controlleractionlist = asm.GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && type.Namespace != null)
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()
                
                &
                m.DeclaringType.Namespace.StartsWith("CmsWeb.Areas.UsersAndRolesManagement")

                )
                .Select(x =>
                {
                    string area = "";
                    string controller = x.DeclaringType.Name;
                    // Extract the area from the namespace
                    if (x.DeclaringType.Namespace.StartsWith("CmsWeb.Areas.UsersAndRolesManagement"))
                    {
                        area = x.DeclaringType.Namespace.Split('.')[2]; // Adjust this index as per your project's namespace structure
                    }
                    return new ReturnedActions
                    {
                        Area = area,
                        Controller = controller.Replace("Controller",""),
                        Action = x.Name,
                        ReturnType = x.ReturnType.Name,
                        Attributes = string.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")))
                    };
                })
                    .GroupBy(x => new { x.Controller, x.Action }) // Group by Controller and Action
                    .Select(group => group.First()) // Select the first entry in each group
                    .OrderBy(x => x.Area).ThenBy(x => x.Controller).ThenBy(x => x.Action)
                    .ToList();



            return controlleractionlist;

        }
        public static List<ReturnedActions> GetAllControllerActionsByArea(string areaName)
        {

            Assembly asm = Assembly.GetExecutingAssembly();

            // Load the XML documentation file
            string xmlDocumentationFile = $"{asm.Location}.xml";
            xmlDocumentationFile = xmlDocumentationFile.Replace(".dll", "");
            XDocument xmlDocument = XDocument.Load(xmlDocumentationFile);

            List<ReturnedActions> controlleractionlist = asm.GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && type.Namespace != null)
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any() &&
                            m.DeclaringType.Namespace.StartsWith($"CmsWeb.Areas.{areaName}"))
                .Select(x =>
                {
                    string area = "";
                    string controller = x.DeclaringType.Name.Replace("Controller", "");

                    // Extract the area from the namespace
                    if (x.DeclaringType.Namespace.StartsWith("CmsWeb.Areas.UsersAndRolesManagement"))
                    {
                        area = x.DeclaringType.Namespace.Split('.')[2]; // Adjust this index as per your project's namespace structure
                    }

                    // Get the DisplayName attribute
                    string displayName = x.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
                    //string displayName = x.GetCustomAttribute<DisplayAttribute>()?.Name;

                    // Get XML documentation comments
                    string documentation = xmlDocument.XPathSelectElement($"//member[@name='M:{x.DeclaringType.FullName}.{x.Name}']")?.Value;

                    return new ReturnedActions
                    {
                        Area = area,
                        Controller = controller,
                        Action = x.Name,
                        ReturnType = x.ReturnType.Name,
                        Attributes = string.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))),
                        DisplayName = displayName,
                        Documentation = documentation
        
                    };
                })
                .GroupBy(x => new { x.Controller, x.Action }) // Group by Controller and Action
                .Select(group => group.First()) // Select the first entry in each group
                .OrderBy(x => x.Area).ThenBy(x => x.Controller).ThenBy(x => x.Action)
                .ToList();

            return controlleractionlist;

        }
        public static List<NavigationMenu> GetNavMenuItems()
        {
            return new ApplicationDbContext().NavigationMenu.ToList();
        }

        public static List<NavigationMenu> GetRolePermissions(string Id)
        {
            return new ApplicationDbContext().RoleMenuPermission.Include(a=>a.NavigationMenu)
                .Where(a=>a.RoleId==Id)
                .Select(a=>a.NavigationMenu)
                .ToList();
        }

        public static List<NavigationMenu> GetRoleNoPermissions(string Id)
        {

            List<Guid> rpm= new ApplicationDbContext().RoleMenuPermission
                .Where(a => a.RoleId == Id)
                .Select(a => a.NavigationMenuId)
                .ToList();

            return new ApplicationDbContext().NavigationMenu
                .Where(a => !rpm.Contains(a.Id))
                .ToList();
        }
    }
}
