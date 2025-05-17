using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

namespace Server.Data.Entities
{
    public class ThanksLetter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Code must contain only numbers.")]
        public int Code { get; set; }
        [Required]
        public String EmpId { get; set; } 
    
        [MaxLength(300)]
        public string? LetterName {  get; set; }
        [MaxLength(100)]
        public string? Geha { get; set; }
        public DateOnly? DecisionDate { get; set; }
        [Range(1,int.MaxValue)] 
        public int? DecisionNo {  get; set; }

        [ForeignKey("EmpId")]
        public virtual Employee NationalId { get; set; }
    }
}
