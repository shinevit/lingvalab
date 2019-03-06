using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    [Table("Film")]
    public class Film
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Link { get; set; }
        public string LanguageName { get; set; }
        public virtual Genre Genre { get; set; }
        [StringLength(3)]
        [ForeignKey("LanguageName")]
        public virtual Language Language { get; set; }
        public virtual ICollection<Subtitle> Subtitles { get; set; }
        public Film()
        {
            Subtitles = new List<Subtitle>();
        }
    }
}
