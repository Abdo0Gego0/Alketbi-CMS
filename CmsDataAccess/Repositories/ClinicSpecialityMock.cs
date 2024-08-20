﻿using CmsDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Repositories
{
    public static class ClinicSpecialityMock
    {

        public static List<object> GetTranslationsInAllLanguages
        {
            get
            {

                List<object> res =new List<object>();

                var clinincSpecialityTranslations = new ApplicationDbContext()
                    .ClinincSpecialityTranslations.GroupBy(a=>a.ClinicSpecialityId);


                foreach (var item in clinincSpecialityTranslations)
                {
                    string temp = "";

                    foreach (var subItem in item)
                    {
                        temp += " - " + subItem.Description;
                    }

                    res.Add(new {Id=item.Key,Name= temp});
                }

                return res;
            }
        }



    }
}
