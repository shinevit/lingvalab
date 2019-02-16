using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.BusinessLayer.Services
{
    public interface ITranslaterService
    {
        string Translate(string text, string originalLanguage, string translationLanguage);
    }
}
