using System.ComponentModel.DataAnnotations;

namespace WebAppTesting_cyber.Dtos;

public class TestingCreateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public long UserId { get; set; }
    public string Grade { get; set; }
    public string Subject { get; set; }

}