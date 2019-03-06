using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DataAccessLayer.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Login { get; set; }
        public string Password { get; set; }

        [ForeignKey("UserId")]
        public virtual IEnumerable<DictionaryRecord> UserDictionary { get; set; }
    }
}
