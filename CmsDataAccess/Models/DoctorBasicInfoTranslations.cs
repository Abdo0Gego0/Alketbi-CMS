using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public  class DoctorBasicInfoTranslations 
	{
		[Key]
		public Guid Id { get; set; }
		public string? LangCode { get; set; } = "en-US";
		public string FirstName { get; set; }
		public string LastName { get; set; }

        [ForeignKey("Doctor")]
        public Guid? DoctorId { get; set; }

		[NotMapped]
		public string FullName
		{
			get
			{ return FirstName+" "+ LastName; }
		}

    }
}
