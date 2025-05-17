using Server.Models.DTOs.BaseDTOs;
using System.ComponentModel.DataAnnotations;
namespace Server.Models.DTOs.FincialDegree
{
    public class CreateFincialDegreeDTO:CreateBaseDTO
    {
        public int? FincialDegreeTypeId { get; set; }
    }
}
