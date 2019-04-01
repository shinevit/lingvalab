using System.IO;
using System.Net;
using Lingva.BusinessLayer.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
            if (text.Length == 0) return "";

            var request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?"
                                            + "key=" + _storageOptions.Value.ServicesYandexTranslaterKey
                                            + "&text=" + text
                                            + "&lang=" + originalLanguage + "-" + translationLanguage);

            var response = request.GetResponse();

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                var line = stream.ReadToEnd();
                var translation = JsonConvert.DeserializeObject<dynamic>(line);
                text = "";
                foreach (string str in translation.text) text += str;
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

            var request = WebRequest.Create("https://dictionary.yandex.net/api/v1/dicservice.json/lookup?"
                                            + "key=" + _storageOptions.Value.ServicesYandexDictionaryKey
                                            + "&lang=" + originalLanguage + "-" + translationLanguage
                                            + "&text=" + text);

            var response = request.GetResponse();

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                var line = stream.ReadToEnd();
                var translation = JsonConvert.DeserializeObject<dynamic>(line);
                translations = new string[translation.def[0].tr[0].syn.Count + 1];
                translations[0] = translation.def[0].tr[0].text;
                for (var i = 1; i <= translation.def[0].tr[0].syn.Count; i++)
                    translations[i] = translation.def[0].tr[0].syn[i - 1].text;
            }

            return translations;
        }
    }
}