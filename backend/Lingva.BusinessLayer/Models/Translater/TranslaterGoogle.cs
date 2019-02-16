using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Lingva.BusinessLayer.Translater
{
    public class TranslaterGoogle : ITranslater
    {
        private readonly string _serviceKey;

        public TranslaterGoogle(string serviceKey)
        {
            _serviceKey = serviceKey;
        }

        public string Translate(string text, string originalLanguage, string translationLanguage)
        {
            if (text.Length == 0)
            {
                return "";
            }

            WebRequest request = WebRequest.Create("https://translation.googleapis.com/language/translate/v2/?"
                + "key=" + _serviceKey//?? to find later
                + "&q=" + text
                + "&source=" + originalLanguage
                + "&target=" + translationLanguage);

            WebResponse response = request.GetResponse();

            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string line = stream.ReadToEnd();
                dynamic translation = JsonConvert.DeserializeObject<dynamic>(line);
                text = translation.data.translations[0].translatedText;
            }
            return text;
        }
    }
}
