using CmsDataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public class MedicalCenter
	{
		[Key]
		public Guid Id { get; set; }
		public string CenterImage { get; set; } = "";
		public string PhoneNumber { get; set; }
		public string MobileNumber { get; set; }
		public string WhatsApp { get; set; }
		public string Instagram { get; set; }
		public Address? Address { get; set; }
		public List<MedicalCenterTranslation>? MedicalCenterTranslation { get; set; }

		public MedicalCenterTranslation? GetByLang(string lang)
		{
			MedicalCenterTranslation medicalCenterTranslations = new ApplicationDbContext().MedicalCenterTranslation
				.FirstOrDefault(a=>a.LangCode== lang && a.MedicalCenterId==this.Id);

			if(medicalCenterTranslations!=null)
			{
				return medicalCenterTranslations;
			}

			return new MedicalCenterTranslation { Description = "", Name = "" };

		}

        public string? Image
        {
            get
            {
                return SiteUrls.ApiUrl + "pImages/" + CenterImage;
            }
        }


		public double? AverageRating
		{
			get
			{

				List<CenterRating> list = new ApplicationDbContext().CenterRating.ToList();

				if(list.Count>0)
				{
					return list.Sum(a=>a.Points)/list.Count;
				}

				return null;
			}
		}

    }
}
