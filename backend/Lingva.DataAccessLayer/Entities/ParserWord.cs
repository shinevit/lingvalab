using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

       // public virtual SubtitleRow SubtitleRow { get; set; }
    }
}
