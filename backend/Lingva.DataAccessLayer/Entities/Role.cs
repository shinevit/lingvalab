using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    public class Role
    {
        [Key]
        [StringLength(100)]
        public string Name { get; set; }

        [ForeignKey("RoleName")]
        public virtual ICollection<Group> Groups { get; set; }

        public Role()
        {
            Groups = new List<Group>();
        }
    }
}
