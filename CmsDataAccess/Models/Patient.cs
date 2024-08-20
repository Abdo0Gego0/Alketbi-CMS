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
    [Table("Patient")]
    public class Patient : Person
    {
        //[Key]
        //public Guid PId { get; set; }

        private int GeneralNumber_;

        public int GeneralNumber
        {
            get
            {
                return GeneralNumber_;
            }
            set
            {
                if (value <= 0)
                {
                    GeneralNumber_ = new ApplicationDbContext().Patient.Count() + 1;
                }
                else
                {
                    GeneralNumber_ = value;
                }
             }
        }

        [Display(Name = "MedicalRecordNumber")]

        public string MedicalRecordNumber { get; set; } = "";

        [Display(Name = "InsuranceId")]

        public string InsuranceId { get; set; } = "";

        [Display(Name = "InsuranceCompany")]

        public string InsuranceCompany { get; set; } = "";

        [Display(Name = "BloodType")]

        public int? BloodType { get; set; }


        public bool IsDeleted { get; set; }=false;



	}
}
