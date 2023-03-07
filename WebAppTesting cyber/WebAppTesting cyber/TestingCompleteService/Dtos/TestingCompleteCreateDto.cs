using System.ComponentModel.DataAnnotations;

namespace TestingCompleteService.Dtos;

public class TestingCompleteCreateDto
{
    [Required]
    public string Tester { get; set; }
    [Required]
    public string Mark { get; set; }
}