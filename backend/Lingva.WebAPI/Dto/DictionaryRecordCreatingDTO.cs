using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Dto
{
    public class DictionaryRecordCreatingDTO
    {
        public int User { get; set; }
        public string OriginalPhrase { get; set; }
        public string TranslationText { get; set; }
        public string TranslationLanguage { get; set; }
        public string Context { get; set; }
        public string Picture { get; set; }
    }
}
