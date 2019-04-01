namespace Lingva.WebAPI.Dto
{
    public class DictionaryRecordCreatingDTO
    {
        public int UserId { get; set; }
        public string Word { get; set; }
        public string Translation { get; set; }
        public string Language { get; set; }
        public string Context { get; set; }
        public string Picture { get; set; }
    }
}