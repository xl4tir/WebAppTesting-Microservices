using WebAppTesting_cyber.Dtos;

namespace WebAppTesting_cyber.AsyncDataServices;

public interface IMessageBusClient
{
    void PublishNewTesting(TestingPublishedDto testingPublishedDto);
}