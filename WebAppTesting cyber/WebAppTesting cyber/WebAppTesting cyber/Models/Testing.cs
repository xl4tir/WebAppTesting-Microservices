using System.ComponentModel.DataAnnotations;

namespace WebAppTesting_cyber.Models;

public class Testing
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public long UserId { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;
    public string Grade { get; set; }
    public string Subject { get; set; }
    public int NumberOfPasses { get; set; } = 0;
    
}