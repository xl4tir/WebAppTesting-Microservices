using System.ComponentModel.DataAnnotations;

namespace TestingCompleteService.Models;

public class Testing
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public int ExternalID { get; set; }
    
    [Required]
    public string Name { get; set; }

    public ICollection<TestingComplete> TestingCompletes { get; set; } = new List<TestingComplete>();
}