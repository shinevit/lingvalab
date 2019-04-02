using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lingva.DataAccessLayer.Entities
{
    public class Film
    {
        public Film()
        {
            Subtitles = new List<Subtitle>();
            Events = new List<Event>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        [StringLength(250)]
        public string PosterLink { get; set; }

        [StringLength(3)]
        public string LanguageName { get; set; }

        public virtual ICollection<Subtitle> Subtitles { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}