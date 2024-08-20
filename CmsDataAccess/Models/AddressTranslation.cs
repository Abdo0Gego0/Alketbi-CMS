using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public class AddressTranslation
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "LangCode")]

        public string? LangCode { get; set; } = "en-US";


		[ForeignKey("Address")]
		public Guid AddressId { get; set; }

        [Display(Name = "Building")]

        public string? Building { get; set; }
        [Display(Name = "Street")]

        public string? Street { get; set; }
        [Display(Name = "City")]

        public string? City { get; set; }
        [Display(Name = "Country")]

        public string? Country { get; set; }
	}
}
