using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class CourseAttendanceType
    {

        [Key]
        public Guid Id { get; set; }

        [Display(Name="نوع الحضور")]
        [Required(ErrorMessage = "يجب عليك إدخال الاسم")]
        public string Name { get; set; }

        [ForeignKey("MarsCenter")]
        [Display(Name = "المركز")]
        [Required(ErrorMessage = "يجب عليك تحديد المركز")]
        public Guid MarsCenterId { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
