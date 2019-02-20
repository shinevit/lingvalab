using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    [Table("film")]
    public class Film
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        public virtual ICollection<Subtitles> Subtitles { get; set; }

        public Film()
        {
            Subtitles = new List<Subtitles>();
        }
    }
}
