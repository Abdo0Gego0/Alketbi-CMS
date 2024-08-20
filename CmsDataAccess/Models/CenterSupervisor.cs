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
    [Table("CenterAdmin")]
    public class CenterSupervisor : Person
    {
        public int Type { get; set; } = 0;
        public string SAddress { get; set; } = "";
        
        [UIHint("Date")]
        public DateTime? ContractStartDate { get; set; }
        
        [ForeignKey("MarsCenter")]
        [Display(Name = "المركز")]
        [Required(ErrorMessage ="يجب عليك تحديد المركز الخاص بهذا المشرف")]
        public Guid MarsCenterId { get; set; }

        [Required(ErrorMessage = "يجب عليك إدخال البريد الالكتروني")]
        [EmailAddress(ErrorMessage = "صيغة البريد الالكتروني غير صحيحة")]
        [Display(Name = "البريد الالكتروني")]
        [NotMapped]

        public string AdminEmail { get; set; }

        [Required(ErrorMessage = "يجب عليك إدخال هاتف المشرف")]
        [Phone]
        [Display(Name = "الهاتف")]
        [NotMapped]

        public string AdminPhone { get; set; }


        [Required(ErrorMessage = "يجب عليك إدخال اسم المستخدم")]
        [Phone]
        [Display(Name = "اسم المستخدم")]
        [NotMapped]
        public string AdminUserName { get; set; }

        [NotMapped]
        [Display(Name = "كلمة السر")]
        public string Password { get; set; }

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
