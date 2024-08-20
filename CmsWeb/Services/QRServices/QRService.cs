using CmsDataAccess;
using CmsDataAccess.Enums;
using CmsDataAccess.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.VisualStudio.Web.CodeGeneration;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CmsWeb.Services.QRServices
{
    public class QRService: IQRService
    {


        public string MakeQRIn()
        {

            

            string Secretkey = Convert.ToHexString(RandomNumberGenerator.GetBytes(150)); ;


            ApplicationDbContext applicationDbContext = new ApplicationDbContext();

            applicationDbContext.ValidQR.RemoveRange(applicationDbContext.ValidQR.Where(a => a.QRType).ToList());
            applicationDbContext.SaveChanges();

            applicationDbContext.ValidQR.Add(new ValidQR
            {
               Secretkey= Secretkey,
               QRType=true
            });

            applicationDbContext.SaveChanges();

            return $"?secretkey={Secretkey}";
        }


        public string MakeQROut()
        {



            string Secretkey = Convert.ToHexString(RandomNumberGenerator.GetBytes(150)); ;


            ApplicationDbContext applicationDbContext = new ApplicationDbContext();

            applicationDbContext.ValidQR.RemoveRange(applicationDbContext.ValidQR.Where(a=> !a.QRType).ToList());
            applicationDbContext.SaveChanges();

            applicationDbContext.ValidQR.Add(new ValidQR
            {
                Secretkey = Secretkey,
                QRType = false
            });

            applicationDbContext.SaveChanges();

            return SiteUrls.AttendanceUrl + $"?secretkey={Secretkey}";
        }

    }
}
