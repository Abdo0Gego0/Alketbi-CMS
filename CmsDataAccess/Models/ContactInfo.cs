using CmsDataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class ContactInfo
    {
        [Key]
        public Guid Id { get; set; }
        public string? ContactType { get; set; } = "";
        public string? ContactValue { get; set; } = "";

        [NotMapped]
        public bool IsDeleted { get; set; }

        [NotMapped]

        public string? Display { get; set; } = "";
    }
}
