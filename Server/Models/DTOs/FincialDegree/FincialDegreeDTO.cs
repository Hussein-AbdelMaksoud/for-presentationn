using Server.Models.DTOs.BaseDTOs;
using System.ComponentModel.DataAnnotations;
namespace Server.Models.DTOs.FincialDegree
{
    public class FincialDegreeDTO: BaseDTO
    {
        public int? FincialDegreeTypeId { get; set; }
    }
}
