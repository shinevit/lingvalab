using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lingva.DataAccessLayer.Entities
{
    public class SubtitleRow
    {
        public SubtitleRow()
        {
            Words = new List<ParserWord>();
        }

        [Key]
       public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Value { get; set; }

        public int LineNumber { get; set; }

        public int? SubtitleId { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string LanguageName { get; set; }

        public virtual Language Language { get; set; }

        public virtual Subtitles Subtitles { get; set; }

        public virtual ICollection<ParserWord> Words { get; set; }
    }
}