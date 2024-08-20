using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CmsWeb.CustomTagHelpers
{
    [HtmlTargetElement("td", Attributes = "i-user")]

    public class SysUsersTH: TagHelper
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public SysUsersTH(UserManager<IdentityUser> usermgr, RoleManager<IdentityRole> rolemgr)
        {
            userManager = usermgr;
            roleManager = rolemgr;
        }

        [HtmlAttributeName("i-user")]
        public string UserName { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();
            IdentityUser user = await userManager.FindByIdAsync(UserName);
            var roles=userManager.GetRolesAsync(user);
            output.Content.SetContent(roles.Result.Count == 0 ? "No Roles" : string.Join(", ", roles.Result));
        }

    }
}
