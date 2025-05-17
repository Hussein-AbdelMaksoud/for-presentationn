using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace Server.Data.Entities
{
    public class Certificate
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,TextOnly, MaxLength(150)]
        public string Name { get; set; }

        [ForeignKey("educationalLevel")]
        public int? educationalLevelID { get; set; }
        public virtual EducationalLevel? educationalLevel { get; set; }
        public List<Qualification> Qualifications { get; set; } = new List<Qualification>();

    }
}
