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

        
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
<<<<<<< HEAD
        [Required]
        [StringLength(50)]
        public string Username { get; set; }       
=======

        [Required]
        public string Username { get; set; }

>>>>>>> feature-subtitle
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<DictionaryRecord> UserDictionaryRecords { get; set; }
        [ForeignKey("UserId")]
        public virtual ICollection<Group> UserGroups { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }

        public User()
        {
            UserDictionaryRecords = new List<DictionaryRecord>();
<<<<<<< HEAD
            UserGroups = new List<Group>();
=======
            UserGroups = new List<UserGroup>();
>>>>>>> feature-subtitle
        }

        //[Key]
        //public int Id { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string FirstName { get; set; }
        //[Required]
        //[StringLength(50)]
        //public string LastName { get; set; }
        //[Required]
        //[StringLength(50)]
        //public string Username { get; set; }
        //public byte[] PasswordHash { get; set; }
        //public byte[] PasswordSalt { get; set; }

        //[ForeignKey("UserId")]
        //public virtual ICollection<DictionaryRecord> UserDictionaryRecords { get; set; }

        //public User()
        //{
        //    UserDictionaryRecords = new List<DictionaryRecord>();
        //}
    }
}
