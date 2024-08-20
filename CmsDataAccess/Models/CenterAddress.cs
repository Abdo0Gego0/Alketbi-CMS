using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public  class CenterAddress
    {
		[Key]
		public Guid Id { get; set; }
        [Required(ErrorMessage = "You have to select country")]
        [Display(Name ="Country *")]
        public string Country { get; set; }
        [Required(ErrorMessage = "You have to select the state")]
        [Display(Name = "State *")]

        public string State { get; set; }
        [Required(ErrorMessage = "You have to select the city")]
        [Display(Name = "City *")]

        public string City { get; set; }
        [Required(ErrorMessage = "You have to enter address details")]
        [Display(Name = "Address *")]

        public string AddressDetails { get; set; }

    }

}
