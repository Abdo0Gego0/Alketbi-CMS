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
    [Table("Parent")]
    public class Parent : Person 
    {
        public int Type { get; set; } = 0;
        public string SAddress { get; set; } = "";

        [UIHint("Date")]
        public DateTime? ContractStartDate { get; set; } = DateTime.Now;

        [ForeignKey("MarsCenter")]
        [Display(Name = "المركز")]
        [Required(ErrorMessage = "يجب عليك تحديد المركز الخاص بهذا للطالب")]
        public Guid MarsCenterId { get; set; }

        public bool Status { get; set; } = true;

        [Required(ErrorMessage = "يجب عليك إدخال البريد الالكتروني")]
        [EmailAddress(ErrorMessage = "صيغة البريد الالكتروني غير صحيحة")]
        [Display(Name = "البريد الالكتروني")]

        public string StudentEmail { get; set; }

        [Required(ErrorMessage = "يجب عليك إدخال الهاتف")]
        [Phone]
        [Display(Name = "الهاتف")]

        public string StudentPhone { get; set; }

        [Display(Name = "ملاحظات")]
        [NotMapped]
        public string? Notes { get; set; }

        [NotMapped]
        public string? CenterName
        {
            get
            {
                if (MarsCenterId != null)
                {
                    MarsCenter mc = new ApplicationDbContext().MarsCenter.FirstOrDefault(x => x.Id == MarsCenterId);
                    if (mc != null)
                    {
                        return mc.CenterName;
                    }
                }

                return null;
            }
        }

    }
}
