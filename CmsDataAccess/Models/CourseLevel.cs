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
    public class CourseLevel
    {
        [Key] public Guid Id { get; set; }

        [Display(Name = "مستوى الحلقة")]
        [Required(ErrorMessage = "يجب عليك إدخال مستوى الحلقة")]
        public string Name { get; set; }

        [ForeignKey("MarsCenter")]
        [Display(Name = "المركز")]
        [Required(ErrorMessage = "يجب عليك تحديد المركز")]
        public Guid MarsCenterId { get; set; }
        public bool IsDeleted { get; set; } = false;



    }



}
