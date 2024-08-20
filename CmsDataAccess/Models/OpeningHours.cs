using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public class OpeningHours
	{
		[Key]
		public Guid Id { get; set; }
		public DayOfWeek DayOfWeek { get; set; }
		public TimeSpan OpeningTime { get; set; } 
		public TimeSpan ClosingTime { get; set; }

		[ForeignKey("Clinic")]
		public Guid ClinicId { get; set; }


		[NotMapped]

		public bool IsDeleted { get; set; }

		[NotMapped]

		public string Display { get; set; } = "";

        public string HoursOfOperation() => string.Format("{0} : {1} to {2}", (object)this.DayOfWeek, (object)this.OpeningTime, (object)this.ClosingTime);
	}
}
