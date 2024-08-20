namespace CmsWeb.Services.NavServices
{
    public interface INavService
    {
        string GetStylGetStyleMainNav(List<string> controllers, List<string> actions, string activeStyle, string inactiveStyle);

        bool IsNavItemSelected(List<string> controllers, List<string> actions);

        List<string> GetSiteMap();
    }
}
