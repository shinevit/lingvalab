﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Lingva.DataAccessLayer.Entities
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Tag { get; set; }

        public DateTime Date { get; set; }

        public int? FilmId { get; set; }

        public virtual Film Film { get; set; }

        public int? SubtitleId { get; set; }

        public virtual Subtitles Subtitle { get; set; }
    }
}