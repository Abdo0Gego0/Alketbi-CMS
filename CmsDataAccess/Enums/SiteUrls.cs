using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Enums
{
    public static class SiteUrls 
    {
        public static string ApiUrl { get { return "https://localhost:1055/"; } }
        public static string WebUrl { get { return "https://localhost:1055/"; } }
        public static string AttendanceUrl { get { return "https://localhost:1055/Account/attend-me/"; } }

    }
}
