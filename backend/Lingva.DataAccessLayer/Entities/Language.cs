﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lingva.DataAccessLayer.Entities
{
    public class Language
    {
        public Language()
        {
            Words = new List<Word>();
            TranslationDictionary = new List<DictionaryRecord>();
            Subtitles = new List<Subtitles>();
            Films = new List<Film>();
            SubtitleRows = new List<SubtitleRow>();
        }

        [Key]
        [StringLength(3)]
        public string Name { get; set; }

        [ForeignKey("LanguageName")]
        public virtual IEnumerable<Word> Words { get; set; }

        [ForeignKey("LanguageName")]
        public virtual IEnumerable<ParserWord> ParserWords { get; set; }

        [ForeignKey("TranslationLanguageName")]
        public virtual IEnumerable<DictionaryRecord> TranslationDictionary { get; set; }

        public virtual ICollection<Film> Films { get; set; }

        public virtual ICollection<Subtitles> Subtitles { get; set; }

        public virtual ICollection<SubtitleRow> SubtitleRows { get; set; }
    }
}