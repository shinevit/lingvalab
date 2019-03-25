using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }

        public virtual User Users { get; set; }

        [StringLength(100)]
        public string RoleName { get; set; }

        public virtual Role Role { get; set; }

        public int? EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
