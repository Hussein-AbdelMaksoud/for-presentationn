using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Data.Entities
{
    public class MandateType
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

        [MaxLength(150),Required,TextOnly]
        public string Name { get; set; }


        // Navigation Property
        public virtual ICollection<MandateData> MandateData { get; set; } = new List<MandateData>();

    }
}
