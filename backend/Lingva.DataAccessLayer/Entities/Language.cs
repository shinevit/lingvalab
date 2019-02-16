using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DataAccessLayer.Entities
{
    public class Language
    {
        [Key]
        [StringLength(3)]
        public string Name { get; set; }

        public virtual IEnumerable<Phrase> Phrases { get; set; }
        public virtual IEnumerable<DictionaryRecord> TranslationDictionary { get; set; }
    }
}
