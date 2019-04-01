using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.WebAPI.Dto
{
    public class SubtitleDTO : BaseStatusDto
    {
        public string Path { get; set; }

        public int? FilmId { get; set; }

        public string LanguageName { get; set; }
    }
}
