using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class Salary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }

        [Required]
        public String EmpId { get; set; }

        [Range(1, int.MaxValue)]
        [Precision(18, 2)]
        public decimal? Agr_Wasefy { get; set; }

        [Range(1, int.MaxValue)]
        [Precision(18, 2)]
        public decimal? Agr_Mokamel { get; set; }

        [Range(1, int.MaxValue)]
        [Precision(18, 2)]
        public decimal? Hafez_Taawedy { get; set; }

        [Range(1, int.MaxValue)]
        [Precision(18, 2)]
        public decimal? M_asasy30 { get; set; }

        [Range(1, int.MaxValue)]
        [Precision(18, 2)]
        public decimal? Hafez_Thabet { get; set; }

        [Range(1, int.MaxValue)]
        [Precision(18, 2)]
        public decimal? MoKafat_Emt7anat { get; set; }

        [Range(1, int.MaxValue)]
        [Precision(18, 2)]
        public decimal? All_Badalt { get; set; }

        [Range(1, int.MaxValue)]
        [Precision(18, 2)]
        public decimal? Badalat_Okhra { get; set; }

        [Range(1, int.MaxValue)]
        [Precision(18, 2)]
        public decimal? All_Mok { get; set; }
        [MaxLength(250)]
        public string? Notes { get; set; }

        [ForeignKey("EmpId")]
        public virtual Employee NationalId { get; set; }

    }
}
