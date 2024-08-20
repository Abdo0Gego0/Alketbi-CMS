using CmsDataAccess.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public class Doctor 
	{

		[Key]
		public Guid Id { get; set; }
        public string? DoctorImage { get; set; } = "";

        [ForeignKey("Clinic")]
		public Guid? ClinicId { get; set; }

		public IdentityUser? User { get; set; }

		public DoctorSpeciality? DoctorSpeciality { get; set; }

        public List<Certificate>? Certificates { get; set; }
		public List<DoctorBasicInfoTranslations>? DoctorBasicInfoTranslations { get; set; }

        public string? Image
        {
            get
            {
                return SiteUrls.ApiUrl + "pImages/" + DoctorImage;
            }
        }

		public double? AverageRating
		{
			get
			{
				List<DoctorRating> list = new ApplicationDbContext().DoctorRating.Where(a => a.DoctorId == Id).ToList();

				if (list.Count > 0)
				{
					return list.Sum(a => a.Points) / list.Count;
				}

				return null;
			}
		}

	}
}
