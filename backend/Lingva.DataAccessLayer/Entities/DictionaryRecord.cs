using System.ComponentModel.DataAnnotations;

namespace Lingva.DataAccessLayer.Entities
{
    public class DictionaryRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public string WordName { get; set; }

        public virtual Word Word { get; set; }

        [Required]
        [StringLength(200)]
        public string Translation { get; set; }

        [Required]
        [StringLength(3)]
        public string LanguageName { get; set; }

        public virtual Language Language { get; set; }

        [StringLength(200)]
        public string Context { get; set; }

        public string Picture { get; set; }
    }
}