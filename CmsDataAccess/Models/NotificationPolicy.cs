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
	public class NotificationPolicy
    {

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Send notification to patient: ")]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int? HoursBeforeNotification { set; get; } = 1;
 
    }

}
