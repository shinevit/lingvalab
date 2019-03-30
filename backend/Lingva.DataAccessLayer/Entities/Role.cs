using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lingva.DataAccessLayer.Entities
{
    public class Role
    {
        public Role()
        {
            Groups = new List<Group>();
        }

        [Key]
        [StringLength(100)]
        public string Name { get; set; }

        [ForeignKey("RoleName")]
        public virtual ICollection<Group> Groups { get; set; }
    }
}