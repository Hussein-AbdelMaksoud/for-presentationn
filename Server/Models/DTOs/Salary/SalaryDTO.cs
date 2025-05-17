namespace Server.Models.DTOs.Salary
{
    public class SalaryDTO
    {
        public int Id { get; set; }

        public int Code { get; set; }
        public String EmpId { get; set; } 

        public decimal? Agr_Wasefy { get; set; }

        public decimal? Agr_Mokamel { get; set; }

        public decimal? Hafez_Taawedy { get; set; }

        public decimal? M_asasy30 { get; set; }

        public decimal? Hafez_Thabet { get; set; }

        public decimal? MoKafat_Emt7anat { get; set; }

        public decimal? All_Badalt { get; set; }

        public decimal? Badalat_Okhra { get; set; }

        public decimal? All_Mok { get; set; }

        public string? Notes { get; set; }
    }
}
