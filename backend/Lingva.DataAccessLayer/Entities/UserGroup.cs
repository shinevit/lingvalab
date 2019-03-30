using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    public class UserGroup
    {
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
