using CmsDataAccess.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{

    public class ShiftTable
    {
        [Key]
        public Guid Id { get; set; }

        public string ShiftName { get; set; } = "";
        [UIHint("time")]
        [Display(Name = "Start Time")]

        public TimeSpan StartTime { get; set; }

        [UIHint("time")]
        [Display(Name = "End Time")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]

        public TimeSpan EndTime { get; set; }



    }
}
