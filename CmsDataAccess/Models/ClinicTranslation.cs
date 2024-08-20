using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public class ClinicTranslation
	{
		[Key]
		public Guid Id { get; set; }

		public string? LangCode { get; set; } = "en-US";


		[ForeignKey("Clinic")]
		public Guid ClinicId { get; set; }

		public string  Name { get; set; }
		public string  Description { get; set; }

	}
}
