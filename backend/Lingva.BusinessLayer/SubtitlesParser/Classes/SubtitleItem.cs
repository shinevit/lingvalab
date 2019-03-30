using System;
using System.Collections.Generic;

namespace Lingva.BusinessLayer.SubtitlesParser.Classes
{
    public class SubtitleItem
    {
        //Constructors-----------------------------------------------------------------

        /// <summary>
        ///     The empty constructor
        /// </summary>
        public SubtitleItem()
        {
            Lines = new List<string>();
        }

        //Properties------------------------------------------------------------------

        //StartTime and EndTime times are in milliseconds
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public List<string> Lines { get; set; }


        // Methods --------------------------------------------------------------------------

        public override string ToString()
        {
            var startTs = new TimeSpan(0, 0, 0, 0, StartTime);
            var endTs = new TimeSpan(0, 0, 0, 0, EndTime);

            var res = string.Format("{0} --> {1}: {2}", startTs.ToString("G"), endTs.ToString("G"),
                string.Join(Environment.NewLine, Lines));
            return res;
        }
    }
}