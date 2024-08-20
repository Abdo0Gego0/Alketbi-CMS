using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public class WorkingHours
    {
		[Key]
		public Guid Id { get; set; }
		[Display(Name ="اليوم")]
		public DayOfWeek DayOfWeek { get; set; }
        [Display(Name = "ساعة البدء")]

        public TimeSpan StartTime { get; set; }
        [Display(Name = "ساعةالانتهاء")]

        public TimeSpan EndTime { get; set; }

		[ForeignKey("CenterTutor")]
		public Guid? CenterTutorId { get; set; }


		[NotMapped]

		public bool IsDeleted { get; set; }

		[NotMapped]

		public string? Display { get; set; } = "";

        public string HoursOfOperation() => string.Format("{0} : {1} to {2}", (object)this.DayOfWeek, (object)this.StartTime, (object)this.EndTime);
	}
}
