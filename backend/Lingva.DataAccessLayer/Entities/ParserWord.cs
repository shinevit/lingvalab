using System.ComponentModel.DataAnnotations;

namespace Lingva.DataAccessLayer.Entities
{
    public class ParserWord
    {
        [Key]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(3)]
        public string LanguageName { get; set; }

        public int? SubtitleRowId { get; set; }

        public virtual Language Language { get; set; }

        public virtual SubtitleRow SubtitleRow { get; set; }
    }
}