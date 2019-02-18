using Lingva.BusinessLayer.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Lingva.BusinessLayer.Services
{
    public class TranslaterYandexService : ITranslaterService
    {
        private readonly IOptions<StorageOptions> _storageOptions;

        public TranslaterYandexService(IOptions<StorageOptions> storageOptions)
        {
            _storageOptions = storageOptions;
        }

        public string Translate(string text, string originalLanguage, string translationLanguage)
        {
            if (text.Length == 0)
            {
                return "";
            }

            WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?"
                + "key=" + _storageOptions.Value.ServicesYandexTranslaterKey
                + "&text=" + text
                + "&lang=" + originalLanguage + "-" + translationLanguage);

            WebResponse response = request.GetResponse();

            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string line = stream.ReadToEnd();
                dynamic translation = JsonConvert.DeserializeObject<dynamic>(line);
                text = "";
                foreach (string str in translation.text)
                {
                    text += str;
                }
            } 
            return text;
        }

        public string[] GetTranslationVariants(string text, string originalLanguage, string translationLanguage)
        {
            string[] translations;
            if (text.Length == 0)
            {
                translations = new string[0];
                translations[0] = "";
                return translations;
            }

            WebRequest request = WebRequest.Create("https://dictionary.yandex.net/api/v1/dicservice.json/lookup?"
                + "key=" + _storageOptions.Value.ServicesYandexDictionaryKey
                + "&lang=" + originalLanguage + "-" + translationLanguage
                + "&text=" + text);

            WebResponse response = request.GetResponse();

            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string line = stream.ReadToEnd();
                dynamic translation = JsonConvert.DeserializeObject<dynamic>(line);
                translations = new string[translation.def[0].tr[0].syn.Count + 1];
                translations[0] = translation.def[0].tr[0].text;
                for (int i = 1; i <= translation.def[0].tr[0].syn.Count; i++)
                {
                    translations[i] = translation.def[0].tr[0].syn[i - 1].text;
                }
            }
            return translations;
        }
    }
}