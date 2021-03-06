﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lingva.DataAccessLayer.Entities
{
    public class Film
    {
        public Film()
        {
            Subtitles = new List<Subtitles>();
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

        public virtual Language Language { get; set; }

        public int? GengeId { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Subtitles> Subtitles { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}