using Lingva.BusinessLayer.Translater;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.BusinessLayer.Services
{
    public class TranslaterService : ITranslaterService
    {
        private const string ServicesGoogleKey = "AIzaSyAeW7Gac7Nu7CoIwi7oTi6GEfsibWLgrkw";
        private const string ServicesYandexKey = "trnsl.1.1.20170125T084253Z.cc366274cc3474e9.68d49c802348b39b5d677c856e0805c433b7618c";

        //public TranslaterService(IOptions<StorageOptions> storageOptions)//??
        //{
        //    _storageOptions = storageOptions;
        //}

        public string Translate(string text, string originalLanguage, string translationLanguage)
        {
            ITranslater translater = new TranslaterGoogle(ServicesGoogleKey);//??
            string strTranslation = translater.Translate(text, originalLanguage, translationLanguage);

            return strTranslation;
        }

    }
}
