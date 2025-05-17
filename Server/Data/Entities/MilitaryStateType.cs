using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Data.Entities
{
    public class MilitaryStateType
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


        /// <summary>
        /// الموقف من التجنيد
        /// </summary>
        [Required, RegularExpression("^(?=.*[a-zA-Z\\u0600-\\u06FF])[\\p{L}a-zA-Z0-9\\s]+$"), MaxLength(150)]
        public string Name { get; set; }


        // Navigation Properties
        public virtual ICollection<MilitaryState> MilitaryState { get; set; } = new List<MilitaryState>();

    }
}
