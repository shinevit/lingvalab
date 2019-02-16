using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DataAccessLayer.Entities
{
    public class Phrase
    {
        [Key]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public Language Language { get; set; }

        public virtual IEnumerable<DictionaryRecord> DictionaryRecords { get; set; }
    }
}
