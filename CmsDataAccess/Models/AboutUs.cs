using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class AboutUs
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }

    }
}
