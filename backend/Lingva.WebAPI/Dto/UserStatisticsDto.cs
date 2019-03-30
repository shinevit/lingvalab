using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Dto
{
    public class UserStatisticsDto : BaseStatusDto
    {
        public int UserID { get; set; }

        public IEnumerable<int> groupsID;
    }
}
