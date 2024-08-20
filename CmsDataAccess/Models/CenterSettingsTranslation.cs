using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public class CenterSettingsTranslation
    {
		[Key]
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "LangCode")]

        public string? LangCode { get; set; } = "en-US";


		[ForeignKey("CenterSettings")]
		public Guid CenterSettingsId { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
