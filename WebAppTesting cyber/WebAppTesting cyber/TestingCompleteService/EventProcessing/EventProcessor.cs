using AutoMapper;
using System.Text.Json;
using TestingCompleteService.Dtos;
using TestingCompleteService.Data;
using TestingCompleteService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace TestingCompleteService.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }

    public void ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);

        switch (eventType)
        {
            case EventType.TestingPublished:
                addTesting(message);
                break;
            default:
                break;
        }
    }

    private EventType DetermineEvent(string notifcationMessage)
    {
        Console.WriteLine("--> Determining Event");

        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

        switch(eventType.Event)
        {
            case "Testing_Published":
                Console.WriteLine("--> Testing Published Event Detected");
                return EventType.TestingPublished;
            default:
                Console.WriteLine("--> Could not determine the event type");
                return EventType.Undetermined;
        }
    }

    private void addTesting(string testingPublishedMessage)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<ITestingCompleteRepository>();
            
            var testingPublishedDto = JsonSerializer.Deserialize<TestingPublishedDto>(testingPublishedMessage);

            try
            {
                var testing = _mapper.Map<Testing>(testingPublishedDto);
                if(!repo.ExternalTestingExist(testing.ExternalID))
                {
                    repo.CreateTestings(testing);
                    Console.WriteLine("--> Testing added!");
                }
                else
                {
                    Console.WriteLine("--> Testing already exisits...");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not add Testing to DB {ex.Message}");
            }
        }
    }
}

enum EventType
{
    TestingPublished,
    Undetermined
}