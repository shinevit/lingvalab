using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Dto
{
    public class GroupCreatingDTO : BaseStatusDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilmId { get; set; }
        public string Picture { get; set; }
    }
}
