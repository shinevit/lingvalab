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
        public int UserId { get; set; }
        [Required]
        public string OriginalPhraseName { get; set; }
        [Required]
        [StringLength(200)]
        public string TranslationText { get; set; }
        [Required]
        [StringLength(3)]
        public string TranslationLanguageName { get; set; }
        [StringLength(200)]
        public string Context { get; set; }
        public string Picture { get; set; }

        public virtual IEnumerable<UserSetItem> UserSetItems { get; set; }
    }
}
