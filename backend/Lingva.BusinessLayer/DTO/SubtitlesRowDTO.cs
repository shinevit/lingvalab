using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.BusinessLayer.DTO
{
    public class SubtitlesRowDTO
    {
        public int? SubtitleId { get; set; }

        public int LineNumber { get; set; }

        public string Value { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
