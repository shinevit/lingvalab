using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lingva.DataAccessLayer.Entities
{
    public class Subtitles
    {
        public Subtitles()
        {
            SubtitlesRow = new List<SubtitleRow>();
            Events = new List<Event>();
        }

        [Key] public int Id { get; set; }

        [StringLength(200)] public string Path { get; set; }

        public int? FilmId { get; set; }

        public virtual Film Film { get; set; }

        public string LanguageName { get; set; }

        public virtual Language Language { get; set; }

        public virtual ICollection<SubtitleRow> SubtitlesRow { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}