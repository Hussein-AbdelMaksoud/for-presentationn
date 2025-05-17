using Server.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.Salary
{
    public class CreateSalaryDTO
    {

        [Required]
        public String EmpId { get; set; }
        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }
        [Range(1, int.MaxValue)]

        public decimal? Agr_Wasefy { get; set; }
        [Range(1, int.MaxValue)]

        public decimal? Agr_Mokamel { get; set; }
        [Range(1, int.MaxValue)]

        public decimal? Hafez_Taawedy { get; set; }
        [Range(1, int.MaxValue)]

        public decimal? M_asasy30 { get; set; }
        [Range(1, int.MaxValue)]

        public decimal? Hafez_Thabet { get; set; }
        [Range(1, int.MaxValue)]

        public decimal? MoKafat_Emt7anat { get; set; }
        [Range(1, int.MaxValue)]

        public decimal? All_Badalt { get; set; }
        [Range(1, int.MaxValue)]

        public decimal? Badalat_Okhra { get; set; }
        [Range(1, int.MaxValue)]

        public decimal? All_Mok { get; set; }

        public string? Notes { get; set; }

    }
}
