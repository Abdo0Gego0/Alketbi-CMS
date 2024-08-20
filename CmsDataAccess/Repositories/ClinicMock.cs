using CmsDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Repositories
{
    public static class ClinicMock
    {


        public static void DeleteAllClinicTranslations(Guid id)
        {
            ApplicationDbContext db=new ApplicationDbContext();

            List<ClinicTranslation> translations=db.ClinicTranslation.Where(a=>a.ClinicId==id).ToList();
            if (translations.Count()>0)
            {
                db.RemoveRange(translations);
                db.SaveChanges();
            }

        }


        public static void DeleteAllClinicOpeningHours(Guid id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            List<OpeningHours> openingHours = db.OpeningHours.Where(a => a.ClinicId == id).ToList();
            if (openingHours.Count() > 0)
            {
                db.RemoveRange(openingHours);
                db.SaveChanges();

            }

        }


    }
}
