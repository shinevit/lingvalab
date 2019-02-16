using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.BusinessLayer.Translater
{
    public interface ITranslater
    {
        string Translate(string text, string originalLanguage, string translationLanguage);
    }
}
