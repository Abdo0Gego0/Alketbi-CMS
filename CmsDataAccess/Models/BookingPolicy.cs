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
	public class BookingPolicy
    {

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Maximum number of appointments that user can make per day")]
        [Range(minimum:0,maximum:int.MaxValue)]
        public int? AllowedBookingNumberPerday { set; get; }
        [Display(Name = "User will be blocked if he missed this number of appointments")]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int? MaximunAllowedMissedAppointments { set; get; }

    }

}
