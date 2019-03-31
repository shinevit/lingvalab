using Lingva.DataAccessLayer.Entities;
using SubtitlesParser.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.BusinessLayer.Extensions
{
    public static class SubtitleItemExtension
    {
        public static SubtitleRow ToSubtitleRow(this SubtitleItem item)
        {
            SubtitleRow result = new SubtitleRow();

            result.StartTime = new TimeSpan(0, 0, 0, 0, item.StartTime);
            result.EndTime = new TimeSpan(0, 0, 0, 0, item.EndTime);
            result.Value = String.Join(" ", item.Lines);

            return result;
        }
    }
}
