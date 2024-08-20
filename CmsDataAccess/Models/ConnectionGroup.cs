using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class ConnectionGroup
    {
        [Key]
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public string? GroupOldName { get; set; } = "";
       
        [UIHint("Date")]
        public DateTime CreateDate { set;get; }

        [ForeignKey("CompanyEmployee")]
        public Guid? CompanyEmployeeId { get; set; }

        public int Status { get; set; }

    }
}
