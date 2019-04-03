using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.BusinessLayer
{
    public class StorageOptions
    {
        public string ServicesGoogleTranslaterKey { get; set; }
        public string ServicesYandexTranslaterKey { get; set; }
        public string ServicesYandexDictionaryKey { get; set; }

        public StorageOptions() { }
    }
}
