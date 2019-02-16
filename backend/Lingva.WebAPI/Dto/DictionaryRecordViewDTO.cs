using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DataAccessLayer.Dto
{
    public class DictionaryRecordViewDTO
    {
        public string OriginalPhrase { get; set; }
        public string TranslationText { get; set; }
        public string TranslationLanguage { get; set; }
        public string Context { get; set; }
        public string Picture { get; set; }
    }
}
