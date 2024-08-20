using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    [Table("EmployeeModel")]
    public class EmployeeModel : Person
    {
        //[Key]
        //public Guid PId { get; set; }

        public int Type { get; set; } = 0;
        public string SAddress { get; set; } = "";

        [UIHint("Date")]

        public DateTime? ContractStartDate { get; set; }

        public bool IsDeleted { get; set; }=false;


        public List<ShiftTable>? ShiftTables { get; set; }



    }
}
