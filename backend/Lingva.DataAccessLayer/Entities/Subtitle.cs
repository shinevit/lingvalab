using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    public class Subtitle
    {
        public Subtitle()
        {
            SubtitlesRow = new List<SubtitleRow>();
            //Events = new List<Event>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(200)]
        public string Path { get; set; }

        public int? FilmId { get; set; }

        public virtual Film Film { get; set; }

        public string LanguageName { get; set; }

        public virtual IEnumerable<SubtitleRow> SubtitlesRow { get; set; }
    }
}
