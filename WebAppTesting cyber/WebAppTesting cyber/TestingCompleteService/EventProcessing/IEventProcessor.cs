namespace TestingCompleteService.EventProcessing;

public interface IEventProcessor
{
    public void ProcessEvent(string message);
}