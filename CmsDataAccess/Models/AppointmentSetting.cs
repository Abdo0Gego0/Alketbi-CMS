using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class AppointmentSetting
    {
        [Key]
        public Guid Id { get; set; }

        public bool CanCancel { get; set; } = true;
        public int AppointmentAveragePeriod { get; set; } = 15; // Minutes
        public double CanCancelBefore { get; set; } = 1; // hour
        public int MaximumAllowedMissedAppointments { get; set; } = 3; 
        public int MaximumAllowedBookedAppointments{ get; set; } = 3;
        public int FirstReminderHour { set; get; } = 10;
        public int SecondReminderHour { set; get; } = 2;
        public string ArabicMessage { get; set; } = "عزيزي المستخدم نذكرك أن لديك موعد في عيادتنا، مع تمنياتنا لك بالصحة الدائمة.";
        public string EnglishMessage { get; set; } = "Dear client, we would like to remind you that you have an apponitment in our clinic.";
    }
}
