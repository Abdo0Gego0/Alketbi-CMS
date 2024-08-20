using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class DoctorSpeciality
    {
        [Key]
        public Guid Id { get; set; }

        public List<DoctorSpecialityTranslations> DoctorSpecialityTranslations { get; set; }

    }
}
