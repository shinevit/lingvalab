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
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }       
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<DictionaryRecord> UserDictionaryRecords { get; set; }
        [ForeignKey("UserId")]
        public virtual ICollection<Group> UserGroups { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public User()
        {
            UserDictionaryRecords = new List<DictionaryRecord>();
            UserGroups = new List<Group>();
        }
    }
}
