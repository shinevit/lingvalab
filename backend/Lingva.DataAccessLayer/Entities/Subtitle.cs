using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    [Table("subtitles")]
    public class Subtitle
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Path { get; set; }
        [Column("film_id")]
        public int FilmId { get; set; }
        public virtual ICollection<SubtitleRow> SubtitlesRow { get; set; }
        public virtual Film Film { get; set; }
        public virtual Language Language { get; set; }

        public Subtitle()
        {
            SubtitlesRow = new List<SubtitleRow>();
        }
    }
}
