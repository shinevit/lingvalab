using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DataAccessLayer.Entities
{
    public class Language
    {
        [Key]
        [StringLength(2)]
        public string Name { get; set; }

        public virtual ICollection<Word> Words { get; set; }
        public virtual ICollection<DictionaryRecord> UserDictionaryRecords { get; set; }

        public Language()
        {
            Words = new List<Word>();
            UserDictionaryRecords = new List<DictionaryRecord>();
        }
    }
}
