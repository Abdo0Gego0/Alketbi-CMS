using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class NewRequest
    {
        [Key]
        public Guid Id { get; set; }


        [UIHint("Date")]
        public DateTime CreateDate { set; get; }



        [ForeignKey("CompanyEmployee")]
        public Guid? CompanyEmployeeId { get; set; }

        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public string ClientObservation { get; set; }
        public int RequestType { get; set; }
        public int Status { get; set; } = 0;

    }






}
