using CmsDataAccess.Models;
using Kendo.Mvc.UI;
using System.ComponentModel.DataAnnotations;

namespace CmsWeb.Models
{
    public class CourseSessionModel : CourseSession, ISchedulerEvent
    {
        public int TaskID { get; set; }
        [Display(Name="عنوان الجلسة")]
        public string Title { get; set; }
        public string Description { get; set; }

        private DateTime start;
        public DateTime Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value.ToUniversalTime();
            }
        }

        public string StartTimezone { get; set; } = "";

        private DateTime end;
        public DateTime End
        {
            get
            {
                return end;
            }
            set
            {
                end = value.ToUniversalTime();
            }
        }

        public string EndTimezone { get; set; } = "";

        public string RecurrenceRule { get; set; } = "";
        public int? RecurrenceID { get; set; }
        public string RecurrenceException { get; set; } = "";
        public bool IsAllDay { get; set; }
        public Guid? OwnerID { get; set; }

        //public CourseSession ToEntity()
        //{
        //    return new CourseSession
        //    {
        //        Title = Title,
        //        Start = Start,
        //        StartTimezone = StartTimezone,
        //        End = End,
        //        EndTimezone = EndTimezone,
        //        Description = Description,
        //        RecurrenceRule = RecurrenceRule,
        //        RecurrenceException = RecurrenceException,
        //        RecurrenceID = RecurrenceID,
        //        IsAllDay = IsAllDay,
        //        CenterTutorId = (Guid)OwnerID,

        //    };
        //}








    }




}
