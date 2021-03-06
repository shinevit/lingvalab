﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lingva.DataAccessLayer.Entities
{
    public class User
    {
        public User()
        {
            UserDictionaryRecords = new List<DictionaryRecord>();
        }

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
    }
}