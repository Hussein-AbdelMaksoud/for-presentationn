using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class Sector
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
        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }

        [Required, MaxLength(150), TextOnly]
        public string Name { get; set; }
        [Range(1, byte.MaxValue)]
        public byte Status { get; set; }

        public virtual List<GeneralAd> GeneralAds { get; set; } = new List<GeneralAd>();
    }
}
