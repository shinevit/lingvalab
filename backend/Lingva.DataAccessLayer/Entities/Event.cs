using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        
        public DateTime Date { get; set; }

        public int? FilmId { get; set; }

        public virtual Film Film { get; set; }

        public int? SubtitleId { get; set; }

        public virtual Subtitle Subtitle { get; set; }
        
    }
}