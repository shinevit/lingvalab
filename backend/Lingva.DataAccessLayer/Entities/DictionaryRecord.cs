using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DataAccessLayer.Entities
{
    public class DictionaryRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public User Owner { get; set; }
        [Required]
        public Phrase OriginalPhrase { get; set; }
        [Required]
        [StringLength(200)]
        public string TranslationText { get; set; }
        [Required]
        public Language TranslationLanguage { get; set; }
        [StringLength(200)]
        public string Context { get; set; }
        public string Picture { get; set; }

        public virtual IEnumerable<UserSetItem> UserSetItems { get; set; }
    }
}
