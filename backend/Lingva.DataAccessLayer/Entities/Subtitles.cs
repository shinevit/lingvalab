using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    [Table("subtitles")]
    public class Subtitles
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("film_id")]
        public int FilmId { get; set; }
        public virtual ICollection<SubtitlesRow> SubtitlesRow { get; set; }
        public virtual Film Film { get; set; }

        public Subtitles()
        {
            SubtitlesRow = new List<SubtitlesRow>();
        }
    }
}
