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
    [Table("CenterTutor")]
    public class CenterTutor : Person
    {
        public int Type { get; set; } = 0;
        [NotMapped]
        public string color { get; set; } = "red";
        public string SAddress { get; set; } = "";

        [UIHint("Date")]
        public DateTime? ContractStartDate { get; set; } = DateTime.Now;

        [ForeignKey("MarsCenter")]
        [Display(Name = "المركز")]
        [Required(ErrorMessage = "يجب عليك تحديد المركز الخاص بهذا المشرف")]
        public Guid MarsCenterId { get; set; }

        [Required(ErrorMessage = "يجب عليك إدخال البريد الالكتروني")]
        [EmailAddress(ErrorMessage = "صيغة البريد الالكتروني غير صحيحة")]
        [Display(Name = "البريد الالكتروني")]
        [NotMapped]

        public string TutorEmail { get; set; }

        [Required(ErrorMessage = "يجب عليك إدخال هاتف المعلم")]
        [Phone]
        [Display(Name = "الهاتف")]
        [NotMapped]

        public string TutorPhone { get; set; }


        [Required(ErrorMessage = "يجب عليك إدخال اسم المستخدم")]
        [Display(Name = "اسم المستخدم")]
        [NotMapped]
        public string TutorUserName { get; set; }

        [NotMapped]
        [Display(Name = "كلمة السر")]
        public string? Password { get; set; }

        public List<WorkingHours>? WorkingHours { get; set; }

        public string WorkHoursToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (WorkingHours branchTime in (IEnumerable<WorkingHours>)this.WorkingHours)
                stringBuilder.AppendLine(branchTime.HoursOfOperation());
            return stringBuilder.ToString();
        }

        [NotMapped]
        public string? CenterName
        {
            get
            {
                if (MarsCenterId != null)
                {
                    return new ApplicationDbContext().MarsCenter.FirstOrDefault(x => x.Id == MarsCenterId).CenterName;
                }

                return null;
            }
        }

    }
}
