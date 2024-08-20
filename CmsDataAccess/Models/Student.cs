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
    [Table("Student")]
    public class Student : Person
    {
        public int Type { get; set; } = 0;
        public string SAddress { get; set; } = "";

        [UIHint("Date")]
        public DateTime? ContractStartDate { get; set; } = DateTime.Now;


        public List<CourseBase> CourseBase { get; set; }


        [ForeignKey("MarsCenter")]
        [Display(Name = "المركز")]
        [Required(ErrorMessage = "يجب عليك تحديد المركز الخاص بهذا للطالب")]
        public Guid MarsCenterId { get; set; }

        [ForeignKey("Parent")]
        [Display(Name = "ولي الأمر")]
        public Guid? ParentId { get; set; }


        [Required(ErrorMessage = "يجب عليك إدخال البريد الالكتروني")]
        [EmailAddress(ErrorMessage = "صيغة البريد الالكتروني غير صحيحة")]
        [Display(Name = "البريد الالكتروني")]
        [NotMapped]

        public string StudentEmail { get; set; }

        [Required(ErrorMessage = "يجب عليك إدخال هاتف الطالب")]
        [Phone]
        [Display(Name = "الهاتف")]
        [NotMapped]

        public string StudentPhone { get; set; }

        [Required(ErrorMessage = "يجب عليك إدخال اسم المستخدم")]
        [Phone]
        [Display(Name = "اسم المستخدم")]
        [NotMapped]
        public string StudentUserName { get; set; }

        [NotMapped]
        [Display(Name = "كلمة السر")]
        public string? Password { get; set; }


        [Display(Name = "ملاحظات")]
        [NotMapped]
        public string? Notes { get; set; }


        [NotMapped]
        public string? ParentName 
        {
            get
            {
                if(ParentId!=null)
                {
                    return new ApplicationDbContext().Parent.FirstOrDefault(x => x.Id==ParentId).FullName;   
                }

                return null;
            }
                }

        [NotMapped]
        public string? CenterName
        {
            get
            {
                if (MarsCenterId != null)
                {
                    MarsCenter mc= new ApplicationDbContext().MarsCenter.FirstOrDefault(x => x.Id == MarsCenterId);
                    if(mc!=null)
                    {
                        return mc.CenterName;
                    }
                }

                return null;
            }
        }
    }
}
