using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    public class Film
    {
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

        public virtual Language Language { get; set; }

        public int? GengeId { get; set; }

        public virtual Genre Genre { get; set; }
       
        public virtual ICollection<Subtitle> Subtitles { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public Film()
        {
            Subtitles = new List<Subtitle>();
            Events = new List<Event>();
        }
    }
}
