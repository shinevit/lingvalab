using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DataAccessLayer.Entities
{
    public class UserSetItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public UserSet UserSet { get; set; }
        [Required]
        public DictionaryRecord DictionaryTranslationRecord { get; set; }
    }
}
