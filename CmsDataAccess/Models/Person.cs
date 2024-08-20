using CmsDataAccess.Enums;
using CmsDataAccess.Models;
using CmsResources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess
{
	public class Person
	{

		[Key]
		public Guid Id { get; set; }

        [Display(Name = "Title")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Lenght must be between 2 and 50")]
        public string Title { get; set; } = "";

        [Display(Name = "Name *")]
		[StringLength(maximumLength:50,MinimumLength =2,ErrorMessage ="Lenght must be between 2 and 50")]
		[Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }
        
		[Display(Name = "Middle name *")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "This field is required")]
        public string MiddleName { get; set; } = "";
		
		[Display(Name = "Surname *")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "This field is required")]
        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }


		[Display(Name = "Gender")]
		[Required(ErrorMessage = "This field is required")]
		public int Gendre { get; set; } = 0;
		        
		[Display(Name = "Birthdate")]
		[UIHint("Date")]
		public DateTime? BirthDate { get; set; }
		        
		[Display(Name = "Nationality *")]
		public string Nationality { get; set; } = "";

        [Display(Name = "Passport number *")]
        public string PassportNumber { get; set; } = "";

        [Display(Name = "Emirates ID *")]
        [RegularExpression(@"^784-\d{4}-\d{7}-\d$", ErrorMessage = "Emirates ID must follow the format 784-xxxx-xxxxxxx-x")]

        public string NationalCardId { get; set; } = "";
		        
		[Display(Name = "ProfileImage")]
		public string? ProfileImage { get; set; } = "";
		       
		[Display(Name = "LangCode")]
        public string? LangCode { get; set; } = "en-US";
        public string fcm_token { get; set; } = "";
        [Display(Name = "Notes")]
        public string? Description { get; set; } = "";
        public Address? Address { get; set; }
        public CenterAddress? CenterAddress { get; set; }
        public IdentityUser? User { get; set; }


		public bool Status { set; get; } = true;

		public bool CenterStatus { set; get; } = true;

		[NotMapped]
        [Display(Name = "Full name")]

        public string FullName
		{
			get
			{
				return FirstName+" "+MiddleName+" "+LastName;
			}
		}

		[NotMapped]
        [Display(Name = "Age")]

        public int? Age
		{
			get
			{
				if (BirthDate.HasValue)
				{
					DateTime today = DateTime.Today;
					int age = today.Year - BirthDate.Value.Year;

					// Check if the birthday has occurred this year
					if (BirthDate.Value.Date > today.AddYears(-age))
					{
						age--;
					}

					return age;
				}

				return null;
			}
		}

		public string? Image
		{
			get
			{
				return ConfigurationManager.ApiUrl + "pImages/" + ProfileImage;
			}
		}


		public bool IsDeleted { set; get; } = false;

		[NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
