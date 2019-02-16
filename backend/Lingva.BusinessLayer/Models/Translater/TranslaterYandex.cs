using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Lingva.BusinessLayer.Translater
{
    public class TranslaterYandex : ITranslater
    {
        private readonly string _serviceKey;

        public TranslaterYandex(string serviceKey)
        {
            _serviceKey = serviceKey;
        }
        
        public string Translate(string text, string originalLanguage, string translationLanguage)
        {
            if (text.Length == 0)
            {
                return "";
            }

            WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?"
                + "key=" + _serviceKey
                + "&text=" + text
                + "&lang=" + translationLanguage);

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
    }
}