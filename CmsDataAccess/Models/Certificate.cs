using CmsDataAccess.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
	public class Certificate 
	{
		[Key]
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string? CertificateImage { get; set; } = "";

		[ForeignKey("Doctor")]
		public Guid? DoctorId { get; set; }


        [NotMapped]
        public bool IsDeleted { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }


        public string? Image
        {
            get
            {
                return SiteUrls.ApiUrl + "pImages/" + CertificateImage;
            }
        }

    }
}
