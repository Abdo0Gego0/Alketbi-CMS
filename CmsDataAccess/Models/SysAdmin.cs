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
    [Table("SysAdmin")]
    public class SysAdmin : Person
    {
        public int Type { get; set; } = 0;
        public string SAddress { get; set; } = "";


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


        [Required(ErrorMessage = "يجب عليك إدخال هاتف المشرف")]
        [Phone]
        [Display(Name = "اسم المستخدم")]
        [NotMapped]
        public string AdminUserName { get; set; }

        [NotMapped]
        [Display(Name = "كلمة السر")]
        public string Password { get; set; }

    }
}
