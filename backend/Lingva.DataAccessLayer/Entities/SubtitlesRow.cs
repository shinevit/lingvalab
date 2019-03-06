using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    [Table("subtitles_row")]
    public class SubtitlesRow
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("subtitles_id")]
        public int SubtitlesId { get; set; }
        [Column("line_number")]
        public int LineNumber { get; set; }
        [Column("value")]
        public string Value { get; set; }
        [Column("start_time")]
        public TimeSpan StartTime { get; set; }
        [Column("end_time")]
        public TimeSpan EndTime { get; set; }
        public virtual Subtitles Subtitles { get; set; }
    }
}
