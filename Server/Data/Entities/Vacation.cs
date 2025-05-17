using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Entities
{
    public class Vacation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }
        [Required]
        public bool Ended {  get; set; }

        [MaxLength(100),TextOnly]
        public string? Place {  get; set; }

        [ForeignKey("VacationType")]
        public int? VacationTypeId { get; set; }
      
        [Required]
        public String EmpId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public DateOnly? ReturnDate {  get; set; }
        [Range(1,int.MaxValue)]
        public int? DecisionNo {  get; set; }
        public DateOnly? DecisionDate {  get; set; }
        [MaxLength(300)]
        public string? Notes {  get; set; }
        
        public virtual VacationType? VacationType { get; set; }

        [ForeignKey("EmpId")]
        public virtual Employee NationalId { get; set; }

    }
}
