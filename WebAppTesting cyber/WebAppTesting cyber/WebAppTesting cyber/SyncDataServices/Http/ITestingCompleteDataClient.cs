namespace WebAppTesting_cyber.SyncDataServices.Http;
using WebAppTesting_cyber.Dtos;

public interface ITestingCompleteDataClient
{
    Task SendTestingToTestingComplete(TestingReadDto testing);
    
}