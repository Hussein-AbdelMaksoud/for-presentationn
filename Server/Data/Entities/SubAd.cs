using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class SubAd
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

        [Required,TextOnly, MaxLength(150)]
        public string Name { get; set; }
        public bool Level { get; set; }
        public bool SpecialLevel { get; set; }
        [Range(1, byte.MaxValue)]
        public byte Status { get; set; }

        [ForeignKey("GeneralAd")]
        public int? GeneralAdId { get; set; }
        public virtual GeneralAd? GeneralAd { get; set; }
        public virtual List<Department> Departments { get; set; } = new List<Department>();
        public virtual List<Employee> Employees { get; set; } = new List<Employee>();

    }
}
