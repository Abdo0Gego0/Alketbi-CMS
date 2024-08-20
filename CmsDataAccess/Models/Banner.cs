using CmsDataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class Banner
    {
        [Key]
        public Guid Id { get; set; }

        public string? BannerImage { get; set; } = "";

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? Image
        {
            get
            {
                return SiteUrls.ApiUrl + "pImages/" + BannerImage;
            }
        }

    }
}
