using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using CmsResources;

namespace CmsDataAccess.Models
{
    public class ClinincSpecialityTranslations
    {
        [Key]

		public Guid Id { get; set; }
        public string? LangCode { get; set; } = "en-US";
        [ForeignKey("ClinicSpeciality")]
        public Guid ClinicSpecialityId { get; set; }
		public string Description { get; set; }
    }
}
