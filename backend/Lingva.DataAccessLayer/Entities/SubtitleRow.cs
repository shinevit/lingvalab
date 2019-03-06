using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    [Table("subtitles_row")]
    public class SubtitleRow
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Value { get; set; }
        public int SubtitlesId { get; set; }
        [Column("line_number")]
        public int LineNumber { get; set; }
        public TimeSpan StartTime { get; set; }
        [Column("end_time")]
        public TimeSpan EndTime { get; set; }
        public virtual Subtitle Subtitles { get; set; }
        public virtual ICollection<Word> Words { get; set; }
        public SubtitleRow()
        {
            Words = new List<Word>();
        }
    }
}
