namespace Lingva.BusinessLayer.Contracts
{
    public interface ITranslaterService
    {
        string Translate(string text, string originalLanguage, string translationLanguage);
        string[] GetTranslationVariants(string text, string originalLanguage, string translationLanguage);
    }
}