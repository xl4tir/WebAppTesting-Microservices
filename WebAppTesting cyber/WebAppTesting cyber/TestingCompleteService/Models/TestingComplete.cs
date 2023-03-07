using System.ComponentModel.DataAnnotations;

namespace TestingCompleteService.Models;

public class TestingComplete
{
    
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Tester { get; set; }
    
    public string Mark { get; set; }
    
    public DateTime DateOfTesting { get; set; } = DateTime.Now;
    
    [Required]
    public int TestingId{ get; set; }
    
    public Testing Testing { get; set; }
}