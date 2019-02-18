using Lingva.BusinessLayer.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.BusinessLayer.Services
{
    public class TranslaterService : ITranslaterService
    {
        private readonly ITranslaterService _translaterService;
        private readonly IOptions<StorageOptions> _storageOptions;

        public TranslaterService(ITranslaterService translaterService, IOptions<StorageOptions> storageOptions)
        {
            _translaterService = translaterService;
            _storageOptions = storageOptions;
        }

        public string Translate(string text, string originalLanguage, string translationLanguage)
        {
            return _translaterService.Translate(text, originalLanguage, translationLanguage);
        }

        public string[] GetTranslationVariants(string text, string originalLanguage, string translationLanguage)
        {
            return _translaterService.GetTranslationVariants(text, originalLanguage, translationLanguage);
        }
    }
}
