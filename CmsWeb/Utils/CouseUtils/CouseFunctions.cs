using CmsDataAccess;
using CmsDataAccess.Enums;
using CmsDataAccess.Models;

namespace CmsWeb.Utils.CouseUtils
{
    public class CouseFunctions
    {

        public static void DeleteCourseLevel(CourseLevel cl)
        {

            ApplicationDbContext cms = new ApplicationDbContext();


            if(cms.CourseBase.Where(a => a.CourseLevelId == cl.Id).Any())
            {

                cl.IsDeleted=true;

                cms.Entry(cl).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                cms.SaveChanges();

                return;
            }

            cms.CourseLevel.Remove(cl);
            cms.SaveChanges();

        }

        public static void DeleteAttendanceType(CourseAttendanceType cl)
        {

            ApplicationDbContext cms = new ApplicationDbContext();


            if (cms.CourseBase.Where(a => a.CourseAttendanceTypeId == cl.Id).Any())
            {

                cl.IsDeleted = true;

                cms.Entry(cl).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                cms.SaveChanges();

                return;
            }

            cms.CourseAttendanceType.Remove(cl);
            cms.SaveChanges();

        }


    }
}
