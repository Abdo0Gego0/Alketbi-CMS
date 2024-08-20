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
	public class CenterRating
	{
		[Key]
		public Guid Id { get; set; }

		[ForeignKey("IdentityUser")]
		public Guid IdentityUserId { set; get;}

		public int Points { get; set; }

		public string Comment { get; set; }

		public int RatingOwner { set; get; }

	}

}
