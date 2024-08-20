using CmsResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class ClinicSpeciality
    {
        [Key]
		public Guid Id { get; set; }
        public List<ClinincSpecialityTranslations> ClinincSpecialityTranslations { get; set; }


        


    }
}
