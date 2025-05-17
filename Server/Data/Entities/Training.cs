using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class Training
    {
        [Key]
        public int Id { get; set; }
        [TextOnly,Required,MaxLength(150)]
        public string CourseName { get; set; }
        /// <summary>
        /// مكان انعقاد الدورة
        /// </summary>
        public string Place { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        /// <summary>
        /// التقدير
        /// </summary>
        public string Grade { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        /// <summary>
        /// داخلي او خارجي
        /// </summary>
        public int Type { get; set; }
        [ForeignKey("Employee")]
        public string NationalId { get; set; }
        public Employee Employee { get; set; }
    }
}
