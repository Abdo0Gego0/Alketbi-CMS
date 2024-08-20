using CmsDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Repositories
{
    public static class DoctorMock
    {


        public static void DeleteAllBasicInfoTranslations(Guid id)
        {
            ApplicationDbContext db=new ApplicationDbContext();

            List<DoctorBasicInfoTranslations> translations=db.DoctorBasicInfoTranslations.Where(a=>a.DoctorId==id).ToList();
            if (translations.Count()>0)
            {
                db.RemoveRange(translations);
                db.SaveChanges();
            }

        }



    }
}
