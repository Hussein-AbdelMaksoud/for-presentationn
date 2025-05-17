using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class GeneralAd
    {
        /// <summary>
        /// Primary Key - Auto Generated
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// University Code - User Visible
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers."),Required]
        public int Code { get; set; }

        [Required,TextOnly, MaxLength(150)]
        public string Name { get; set; }
        public bool Level { get; set; }
        public bool SpecialLevel { get; set; }
        [Range(1, byte.MaxValue)]
        public byte Status { get; set; }
        [ForeignKey("Sector")]
        public int? SectorID { get; set; }
        public virtual Sector? Sector { get; set; }
        public virtual List<SubAd> SubAds { get; set; } = new List<SubAd>();
    }
}
