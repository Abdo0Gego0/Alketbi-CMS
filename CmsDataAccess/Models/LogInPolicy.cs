using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public class LogInPolicy
    {
        [Key]
        public Guid Id { get; set; }
        public TimeSpan StartPeriod { get; set; } = new TimeSpan(09, 0, 0, 0);
        public TimeSpan EndPeriod { get; set; } = new TimeSpan(17, 0, 0, 0);

        [Display(Name = "Allow login to system all the time")]

        public bool AllTheTime { get; set; }

        [Display(Name = "Monday")]


        public bool Mon { get; set; } = true;
        [Display(Name = "Tuesday")]

        public bool Tues { get; set; } = true;
        [Display(Name = "Wednesday")]

        public bool Wed { get; set; } = true;
        [Display(Name = "Thursday")]

        public bool Thurs { get; set; } = true;
        [Display(Name = "Friday")]

        public bool Fri { get; set; } = true;
        [Display(Name = "Saturday")]

        public bool Sat { get; set; } = false;
        [Display(Name = "Sunday")]

        public bool Sun { get; set; } = false;
    }

}
