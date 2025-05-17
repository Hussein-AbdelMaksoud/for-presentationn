using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.BaseDTOs;

public class BaseDTO
{

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Code { get; set; }
}