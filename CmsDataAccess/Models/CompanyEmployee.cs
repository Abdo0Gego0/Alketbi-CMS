using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    [Table("CompanyEmployee")]
    public class CompanyEmployee : Person
    {

        [Display(Name = "Job Position *")]

        public int Type { get; set; } = 0;
        [NotMapped]
        public string SAddress { get; set; } = "";

        [UIHint("Date")]
        public DateTime? ContractStartDate { set; get; }


        [Display(Name = "Labor Card Number")]

        public string? JobCardNumber { get; set; }


        [Required(ErrorMessage = "You have to enter the employee e-mail")]
        [Display(Name = "E-mail *")]
        [NotMapped]
        [Remote(action: "IsEmployeeEmailUnique", controller: "Employees",areaName:"Admin", HttpMethod = "GET", ErrorMessage = "Email is already in use")]
        public string EmployeeEmail { get; set; }

        [Required(ErrorMessage = "You have to enter the phone number")]
        [Phone]
        [Display(Name = "Phone Number *")]
        [NotMapped]

        public string EmployeePhone { get; set; }


        [Required(ErrorMessage = "You have to enter the username")]
        [Display(Name = "Username *")]
        [NotMapped]
        [Remote(action: "IsEmployeeUserNameUnique", controller: "Employees", areaName: "Admin", HttpMethod = "GET", ErrorMessage = "Username is already in use")]
        public string EmployeeUserName { get; set; }

        [NotMapped]
        [Display(Name = "Password *")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_])[A-Za-z\d\W_]{8,}$",
        ErrorMessage = "Password must have at least 8 characters and include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string? Password { get; set; }



    }
}
