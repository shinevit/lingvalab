using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lingva.DataAccessLayer.Entities
{
    public class Genre
    {
        public Genre()
        {
            Films = new List<Film>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}