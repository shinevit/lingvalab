using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        [Required]

        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(250)]

        public string Link { get; set; }
        [StringLength(250)]

        public string Poster { get; set; }        

        public string Description { get; set; }

        public string LanguageName { get; set; }

        public int SubtitleId { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public Film()
        {            
            Groups = new List<Group>();
        }
    }
}
