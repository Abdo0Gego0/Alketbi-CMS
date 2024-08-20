using CmsDataAccess.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class CenterSettings
    {
        [Key]
        public Guid Id { get; set; }
        public List<CenterSettingsTranslation>? CenterSettingsTranslation { get; set; }
        public string? CenterLogo { get; set; } = "";
        public Address? Address { get; set; }
        public List<ContactInfo> ContactInfo { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string? Image
        {
            get
            {
                return SiteUrls.ApiUrl + "pImages/" + CenterLogo;
            }

        }

    }
}
