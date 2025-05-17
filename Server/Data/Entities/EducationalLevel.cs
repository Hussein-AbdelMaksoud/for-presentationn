using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class EducationalLevel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,TextOnly, MaxLength(50)]
        public string Name { get; set; }
        [Range(1, int.MaxValue)]
        public int SortId { get; set; }
        public virtual List<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}
