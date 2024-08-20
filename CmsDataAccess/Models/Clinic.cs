using CmsDataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public class Clinic
	{		[Key]
		public Guid Id { get; set; }


		public string? ClinicImage { get; set; } = "";
		public string? RoomNumber { get; set; } = "";

        [Display(Name = "Average Examination Period in Minutes")]
        public int? AverageExaminationPeriod { get; set; } = 15;
        public double Cost { get; set; } = 0;
        public ClinicSpeciality? ClinicSpeciality { get; set; }
		public List<ClinicTranslation>? ClinicTranslation { get; set; }
        public List<OpeningHours>? OpeningHours { get; set; }
		public ClinicTranslation? GetByLang(string lang)
		{
			ClinicTranslation clinicTranslation = new ApplicationDbContext().ClinicTranslation
				.FirstOrDefault(a=>a.LangCode== lang);

			if(clinicTranslation != null)
			{
				return clinicTranslation;
			}

			return new ClinicTranslation { Description = "", Name = "" };

		}
		public string WorkHoursToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (OpeningHours branchTime in (IEnumerable<OpeningHours>)this.OpeningHours)
				stringBuilder.AppendLine(branchTime.HoursOfOperation());
			return stringBuilder.ToString();
		}
        public string? Image
        {
            get
            {
				if(!string.IsNullOrEmpty(ClinicImage))
					return SiteUrls.ApiUrl + "pImages/" + ClinicImage;

				return null;
            }
        }
		public double? AverageRating
		{
			get
			{

				List<ClinicRating> list = new ApplicationDbContext().ClinicRating.Where(a=>a.ClinicId==Id).ToList();

				if (list.Count > 0)
				{
					return list.Sum(a => a.Points) / list.Count;
				}

				return null;
			}
		}

	}
}
