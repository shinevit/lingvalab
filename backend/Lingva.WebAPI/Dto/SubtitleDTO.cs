using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.BusinessLayer.DTO
{
    public class SubtitleDTO
    {
        public string Path { get; set; }

        public int? FilmId { get; set; }

        public string LanguageName { get; set; }
        public IEnumerable<SubtitleRow> SubtitleRows { get; set; }
    }
}
