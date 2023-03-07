namespace WebAppTesting_cyber.Dtos;

public class TestingReadDto
{

    public int Id { get; set; }
    public string Name { get; set; }
    public long UserId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string Grade { get; set; }
    public string Subject { get; set; }
    public int NumberOfPasses { get; set; } = 0;
    

}