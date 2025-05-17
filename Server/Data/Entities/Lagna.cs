using Server.Data.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class Lagna
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
        /// الرقم الكودى
        /// </summary>
        [Required]
        public string EmpId { get; set; }

        /// <summary>
        /// اسم اللجنة
        /// </summary>
        [Required,RegularExpression("^(?=.*[a-zA-Z\\u0600-\\u06FF])[\\p{L}a-zA-Z0-9\\s]+$"),MaxLength(150)]
        public string Name { get; set; }


        /// <summary>
        /// الصفة داخل اللجنة ---> allow ""
        /// </summary>
        public string MemberType { get; set; } = string.Empty;

        /// <summary>
        /// رقم قرار اللجنة
        /// </summary>
        [Range(1, int.MaxValue)]
        public int? DecisionNo { get; set; }

        /// <summary>
        ///تاريخ القرار
        /// </summary>
        public DateOnly? DecisionDate { get; set; }



        // Virtual navigation property for lazy loading

        /// <summary>
        /// الربط مع كيان Employee
        /// </summary>
        [ForeignKey("EmpId")]
        public virtual Employee Employee { get; set; }
    }
}
