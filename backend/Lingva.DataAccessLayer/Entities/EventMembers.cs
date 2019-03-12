using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    public class EventMembers
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }
        public virtual User Users { get; set; }

        public int EventID { get; set; }

        //public int? RoleID { get; set; }
        //public virtual Role Role { get; set; }
    }
}
