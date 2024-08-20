using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class MedicalVisit
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Clinic")]
        public Guid? ClinicId { get; set; }

        [ForeignKey("Doctor")]
        public Guid? DoctorId { get; set; }

        [ForeignKey("Patient")]
        public Guid? PatientId { get; set; }

        public DateTime VisitDate { get; set; }
        public DateTime NextVisitDate { get; set; }
        public DateTime ExaminationStartDate { get; set; }
        public DateTime ExaminationEndDate { get; set; }


        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? BloodPressure { get; set; }
        public double? GlucoseLevel { get; set; }
        public double? HeartBeat { get; set; }
        public double? Temperature { get; set; }

        

        public string Reason { get; set; }
        public string Symptoms { get; set; }

        public string MedicalStatus { get; set; }
        public string Diagnosis  { get; set; }


        public double? Cost { get; set; }


        public int ProcessingStatus { get; set; }


        [NotMapped]

        public double? Duration
        {

            get
            {

                if(ExaminationEndDate !=null && ExaminationStartDate!=null)
                {

                    return (ExaminationEndDate-ExaminationStartDate).TotalMinutes;

                }

                return null;
            }
            
        }

    }
}
