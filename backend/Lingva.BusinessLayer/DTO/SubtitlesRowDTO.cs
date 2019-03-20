using Lingva.DataAccessLayer.Entities;
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

        public static explicit operator SubtitleRow (SubtitlesRowDTO sub)
        {
            return new SubtitleRow
            {
                SubtitleId = sub.SubtitleId,
                LineNumber = sub.LineNumber,
                Value = sub.Value,
                StartTime = sub.StartTime,
                EndTime = sub.EndTime
            };
        }
    }
}
