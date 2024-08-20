using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime AppointmentDate { get; set; }


        public bool IsDeleted { get; set; }=false;

        public int AppointmentStatus { get; set; } = 0;

        [ForeignKey("Clinic")]
        public Guid ClinicId { get; set; }


        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }

        public int ReminderSent { set; get; } = 0;
        
        [NotMapped]
        public DateTime? EstimatedEndDate {

            get
            {
                AppointmentSetting appointmentSetting = new ApplicationDbContext().AppointmentSetting.FirstOrDefault();

                if(appointmentSetting!=null)
                {
                    AppointmentDate.AddMinutes(appointmentSetting.AppointmentAveragePeriod);
                }

                return null;
            }
        }


        [NotMapped]
        public DateTime? FirstReminderDate
        {

            get
            {
                AppointmentSetting appointmentSetting = new ApplicationDbContext().AppointmentSetting.FirstOrDefault();

                if (appointmentSetting != null)
                {
                    DateTime firstReminderDateTime = new DateTime(
                     AppointmentDate.Year,
                     AppointmentDate.Month,
                     AppointmentDate.Day,
                     appointmentSetting.FirstReminderHour, // 10 am
                     0,
                     0
                    ).AddDays(-1);

                    return firstReminderDateTime;
                }

                return null;
            }
        }

        [NotMapped]
        public DateTime? SecondReminderDate
        {

            get
            {
                AppointmentSetting appointmentSetting = new ApplicationDbContext().AppointmentSetting.FirstOrDefault();

                if (appointmentSetting != null)
                {
                    DateTime firstReminderDateTime = new DateTime(
                     AppointmentDate.Year,
                     AppointmentDate.Month,
                     AppointmentDate.Day,
                     AppointmentDate.Hour- appointmentSetting.SecondReminderHour,
                     0,
                     0
                    );

                    return firstReminderDateTime;
                }

                return null;
            }
        }

    }
}
