using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lingva.DataAccessLayer.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int SubtitleId { get; set; }
        public string Poster { get; set; }

        public ICollection<Group> Groups { get; set; }

        public Movie()
        {
            Groups = new List<Group>();
        }
    }
}
