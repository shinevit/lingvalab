using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Dto
{
    public class FilmViewDTO : BaseStatusDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubtitleId { get; set; }
        public string Poster { get; set; }
    }
}
