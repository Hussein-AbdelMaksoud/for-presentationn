using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class MilitaryState
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


        [Required]
        [ForeignKey("MilitaryStateType")]
        public int MilitaryStateTypeId { get; set; }

        public DateOnly? Date { get; set; }
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// الموقف الحالى
        /// </summary>
        public bool CurrentMilitaryState { get; set; }





        // Virtual navigation property for lazy loading

        /// <summary>
        /// الربط مع جدول Employee
        /// </summary>
        [ForeignKey("EmpId")]
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// الربط مع جدول MilitaryStateType
        /// </summary>
        public virtual MilitaryStateType MilitaryStateType { get; set; }

    }
}
