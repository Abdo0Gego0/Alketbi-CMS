using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class ArchivedNotification
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Message { get; set; } = "";
        public int? NotiType { get; set; }

        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }
        public bool IsAuto { get; set; } = true;
        public bool IsRead { get; set; } = true;
    }
}
