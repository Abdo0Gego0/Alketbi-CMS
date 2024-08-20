using CmsDataAccess.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class CourseSession
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? GroupNumber { get; set; }


        [ForeignKey("CourseBase")]
        [Display(Name = "الحلقة")]
        [Required(ErrorMessage = "يجب اختيار الحلقة")]
        public Guid? CourseBaseId { get; set; }

        [ForeignKey("MarsCenter")]
        [Display(Name = "المركز")]
        [Required(ErrorMessage = "يجب عليك تحديد المركز")]
        public Guid? MarsCenterId { get; set; }

        [ForeignKey("CenterTutor")]
        [Display(Name = "المعلم")]
        [Required(ErrorMessage = "يجب عليك تحديد المعلم")]
        public Guid CenterTutorId { get; set; }
        public DailyRecurrence? DailyRecurrence { get; set; }
        public WeeklyRecurrence? WeeklyRecurrence { get; set; }

        public string? TimeZone { get; set; }
        public int? RecurrenceID { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? SessionOrder { get; set; } = 0;
        public int Status { get; set; }=0;

        [NotMapped]
        public string SessionRecurrenceType
        {
            get
            {
                if (DailyRecurrence != null)
                    return "يومي";

                if (WeeklyRecurrence != null)
                    return "أسبوعي";

                return "";
            }
        }
    }

    public class DailyRecurrence
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "الفاصل الزمني (بالأيام)")]
        public int? Interval { get; set; } = 1;
        [Display(Name = "عدد التكرارات")]

        public int? EndAfterNumberOfOccurence { get; set; }
        [Display(Name = "الانتهاء عندانتهاء الحلقة")]
        [UIHint("checkbox")]

        public bool? EndWhenCourseEnd { get; set; }
        [Display(Name = "منذ الساعة")]
        public TimeSpan StartTime { get; set; }
        [Display(Name = "حتى الساعة")]

        public TimeSpan EndTime { get; set; }

        [NotMapped]
        public bool Included { get; set; } = false;
    }

    public class WeeklyRecurrence
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "الفاصل الزمني (بالأسابيع)")]

        public int? Interval { get; set; } = 1;

        [Display(Name = "الأحد")]
        public bool? Sun { get; set; }
        [Display(Name = "منذ الساعة")]
        public TimeSpan? SunStart { get; set; }
        [Display(Name = "حتى الساعة")]
        public TimeSpan? SunEnd { get; set; }


        [Display(Name = "الإثنين")]
        public bool? Mon { get; set; }
        [Display(Name = "منذ الساعة")]
        public TimeSpan? MonStart { get; set; }
        [Display(Name = "حتى الساعة")]
        public TimeSpan? MonEnd { get; set; }


        [Display(Name = "الثلاثاء")]
        public bool? Tue { get; set; }
        [Display(Name = "منذ الساعة")]
        public TimeSpan? TueStart { get; set; }
        [Display(Name = "حتى الساعة")]
        public TimeSpan? TueEnd { get; set; }


        [Display(Name = "الأربعاء")]
        public bool? Wed { get; set; }
        [Display(Name = "منذ الساعة")]
        public TimeSpan? WedStart { get; set; }
        [Display(Name = "حتى الساعة")]
        public TimeSpan? WedEnd { get; set; }


        [Display(Name = "الخميس")]
        public bool? Thu { get; set; }
        [Display(Name = "منذ الساعة")]
        public TimeSpan? ThuStart { get; set; }
        [Display(Name = "حتى الساعة")]
        public TimeSpan? ThuEnd { get; set; }


        [Display(Name = "الجمعة")]
        public bool? Fri { get; set; }
        [Display(Name = "منذ الساعة")]
        public TimeSpan? FriStart { get; set; }
        [Display(Name = "حتى الساعة")]
        public TimeSpan? FriEnd { get; set; }


        [Display(Name = "السبت")]
        public bool? Sat { get; set; }
        [Display(Name = "منذ الساعة")]
        public TimeSpan? SatStart { get; set; }
        [Display(Name = "حتى الساعة")]
        public TimeSpan? SatEnd { get; set; }



        [Display(Name = "عدد التكرارات")]

        public int? EndAfterNumberOfOccurence { get; set; }
        [Display(Name = "الانتهاء عندانتهاء الحلقة")]
        [UIHint("checkbox")]
        public bool? EndWhenCourseEnd { get; set; }

        [NotMapped]
        public bool Included { get; set; }=false;
    }

    public class MyDayOfWeek
    {
        [Key]
        public Guid Id { get; set; }
        public int MyDayNumber { get; set; }
        public string MyDayName { get; set; }

        [NotMapped]
        [UIHint("checkbox")]
        public bool Included { get; set; } = false;

    }


    public class LessonMemorize
    {
        [Key]
        public Guid Id { set; get; }
        public string SessContent { set;get; }

    }

    public class LessonNearReview
    {
        [Key]
        public Guid Id { set; get; }
        public string SessContent { set; get; }


    }

    public class LessonFarReview
    {
        [Key]
                public Guid Id { set; get; }
                public string SessContent { set;get; }


    }

    public class LessonTajweed
    {
[Key]
        public Guid Id { set; get; }
                public string SessContent { set;get; }


    }

    public class LessonTest
    {
        [Key]
                public Guid Id { set; get; }
                public string SessContent { set;get; }


    }

    public class LessonTask
    {
[Key]
        public Guid Id { set; get; }
                public string SessContent { set;get; }


    }

}
