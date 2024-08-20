using CmsDataAccess.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public class MarsCenter
    {		[Key]
		public Guid Id { get; set; }

		public string? CenterImage { get; set; } = "";

        [Display(Name = "اسم المركز")]
        [Required(ErrorMessage = "يجب عليك إدخال اسم المركز")]

        public string CenterName { get; set; }
        [Display(Name = "الوصف")]

        public string? CenterDescription { get; set; } = "";

        [Required(ErrorMessage = "يجب عليك إدخال البريد الالكتروني")]
		[EmailAddress(ErrorMessage ="صيغة البريد الالكتروني غير صحيحة")]
        [Display(Name = "البريد الالكتروني")]

        public string CenterEmail { get; set; }

        [Required(ErrorMessage = "يجب عليك إدخال هاتف المركز")]
        [Phone]
        [Display(Name = "الهاتف")]

        public string CenterPhone { get; set; }

        [Required(ErrorMessage = "يجب عليك إدخال عنوان المركز")]

        public CenterAddress CenterAddress { get; set; }

        [Required(ErrorMessage = "يجب عليك إدخال السجل التجاري")]
        [Display(Name = "السجل التجاري")]
        public string CommercialRegister { get; set; }

        [Required(ErrorMessage = "يجب عليك إدخال الرقم الضريبي")]
        [Display(Name = "الرقم الضريبي")]
        public string TaxNumber { get; set; }

        public List<ContactInfo>? ContactInfo { get; set; }

		public DateTime CreateDate { get; set; }=DateTime.Now;

        public bool Status { get; set; }=true;

        public bool IsDeleted { get; set; }  =false;

        public string? Image
        {
            get
            {
				if(!string.IsNullOrEmpty(CenterImage))
					return SiteUrls.ApiUrl + "pImages/" + CenterImage;

				return null;
            }
        }

        [Display(Name = "تققيم المركز")]

        public double? AverageRating
		{
			get
			{

				List<ClinicRating> list = new ApplicationDbContext().ClinicRating.Where(a=>a.ClinicId==Id).ToList();

				if (list.Count > 0)
				{
					return list.Sum(a => a.Points) / list.Count;
				}

				return null;
			}
		}

		[NotMapped]
		public IFormFile? ImageFile { get; set; }



    }
}
