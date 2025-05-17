using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class Department
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
        public byte Status { get; set; }
        [ForeignKey("SubAd")]
        public int? SubAdID { get; set; }
        public virtual SubAd? SubAd { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
