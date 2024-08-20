using CmsDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Enums
{
    public class CourseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class CourseTypeEnum
    {
        public  CourseType Fixed { get { return new CourseType { Id = 1, Name = "حلقة ثابتة" }; }}
        public  CourseType Flexible { get { return new CourseType { Id = 2, Name = "حلقة مرنة" }; } }
        public List<CourseType> CourseTypes { get { return new List<CourseType> { Fixed,Flexible }; } }
    }


    public class AttendanceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AttendanceTypeEnum
    {
        public AttendanceType Men { get { return new AttendanceType { Id = 1, Name = "رجال" }; } }
        public AttendanceType Women { get { return new AttendanceType { Id = 2, Name = "نساء" }; } }
        public List<CourseAttendanceType> AttendanceTypes 
        { 
            get {
                return new ApplicationDbContext().CourseAttendanceType.ToList();
            } 
        }
    }


}
