using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lingva.DataAccessLayer.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public int? UserId { get; set; }

        public virtual User Users { get; set; }

        public int? EventId { get; set; }

        public virtual User Event { get; set; }
    }
}
