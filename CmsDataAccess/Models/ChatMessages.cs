using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Models
{
    public class ChatMessages
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ConnectionGroup")]
        public Guid ConnectionGroupId { get; set; }
        
        [UIHint("Date")]
        public DateTime CreateDate { set; get; }
        public int Sender { set; get; }

        public string Message { get; set;}

    }
}
