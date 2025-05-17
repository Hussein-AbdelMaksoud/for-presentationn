using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Data.Entities
{
    public class JobType
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
        /// نوع الوظيفة
        /// </summary>

        [MaxLength(150),TextOnly, Required]
        public string Name { get; set; }

        public virtual ICollection<JobDegredationData> JobDegredationData { get; set; } = new List<JobDegredationData>();
    }
}
