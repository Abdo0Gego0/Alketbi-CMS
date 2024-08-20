using CmsDataAccess.Enums;
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
    public class CourseBase 
    {
        [Key] public Guid Id { get; set; }

        [Display(Name = "نوع الحلقة")]
        [Required(ErrorMessage = "يجب عليك اختيار نوع الحلقة")]
        public int Type{ get; set; }



        [ForeignKey("CourseAttendanceType")]

        [Display(Name = "نوع الحضور")]
        [Required(ErrorMessage = "يجب عليك اختيار نوع الحضور")]
        public Guid CourseAttendanceTypeId { get; set; }

        [Display(Name = "اسم الحلقة")]
        [Required(ErrorMessage = "يجب عليك إدخال اسم الحلقة")]
        public string Name { get; set; }

        [ForeignKey("CourseLevel")]
        [Display(Name = "مستوى الحلقة")]
        [Required(ErrorMessage = "يجب عليك تحديد اختيار مستوى الحلقة")]
        public Guid CourseLevelId { get; set; }

        [Display(Name = "عدد الطلاب")]
        [Required(ErrorMessage = "يجب عليك تحديد عدد الطلاب في الحلقة")]
        public int MaxNumberOfStudents { get; set; }

        [Display(Name = "وصف عام")]
        public string? Description { get; set; } = "";

        [Display(Name = "تاريخ بداية الحلقة")]
        [UIHint("Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "تاريخ نهاية الحلقة")]
        [UIHint("Date")]
        public DateTime? EndDate { get; set; }


        [ForeignKey("MarsCenter")]
        [Display(Name = "المركز")]
        [Required(ErrorMessage = "يجب عليك تحديد المركز")]
        public Guid MarsCenterId { get; set; }


        public bool IsDeleted { get; set; } = false;

        public List<Student>? Student { get; set; }

        [NotMapped]
        [Display(Name = "حالة الحلقة")]

        public bool Status
        {
            get
            {
                if(StartDate!=null && EndDate!=null)
                {
                    if (DateTime.Now>= StartDate && DateTime.Now<=EndDate)
                    {
                        return true;
                    }
                }

                return false;
            }
        }


        [NotMapped]
        [Display(Name = "عدد الطلاب")]

        public int EnrolledStudents
        {
            get
            {
                if (Student != null && Student.Count() != 0)
                {
                    return Student.Count();
                }

                return 0;
            }
        }



        [NotMapped]
        [Display(Name = "المستوى")]

        public string CourseLevel
        {
            get
            {
                return new ApplicationDbContext().CourseLevel.Find(CourseLevelId).Name;
            }
        }




    }



}
