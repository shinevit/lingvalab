using System.IO;
using System.Net;
using Lingva.BusinessLayer.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Lingva.BusinessLayer.Services
{
    public class TranslaterGoogleService : ITranslaterService
    {
        private readonly IOptions<StorageOptions> _storageOptions;

        public TranslaterGoogleService(IOptions<StorageOptions> storageOptions)
        {
            _storageOptions = storageOptions;
        }

        public string Translate(string text, string originalLanguage, string translationLanguage)
        {
            if (text.Length == 0) return "";

            var request = WebRequest.Create("https://translation.googleapis.com/language/translate/v2/?"
                                            + "key=" + _storageOptions.Value.ServicesGoogleTranslaterKey
                                            + "&q=" + text
                                            + "&source=" + originalLanguage
                                            + "&target=" + translationLanguage);

            var response = request.GetResponse();

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                var line = stream.ReadToEnd();
                var translation = JsonConvert.DeserializeObject<dynamic>(line);
                text = translation.data.translations[0].translatedText;
            }

            return text;
        }

        public string[] GetTranslationVariants(string text, string originalLanguage, string translationLanguage)
        {
            var translations = new string[1];
            translations[0] = Translate(text, originalLanguage, translationLanguage);

            return translations;
        }
    }
}