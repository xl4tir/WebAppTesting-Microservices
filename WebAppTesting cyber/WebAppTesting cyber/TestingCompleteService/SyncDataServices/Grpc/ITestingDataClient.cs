using TestingCompleteService.Models;

namespace TestingCompleteService.SyncDataServices.Grpc;

public interface ITestingDataClient
{
    IEnumerable<Testing> returnAllTestings();
}