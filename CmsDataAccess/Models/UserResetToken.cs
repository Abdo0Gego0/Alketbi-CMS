using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class UserResetToken
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime EndDate { get; set; }=DateTime.Now.AddHours(1);



    }
}
