using AutoMapper;
using Grpc.Net.Client;
using TestingCompleteService.Models;
using WebAppTesting_cyber;

namespace TestingCompleteService.SyncDataServices.Grpc;

public class TestingDataClient : ITestingDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public TestingDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public IEnumerable<Testing> returnAllTestings()
    {
        Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcTesting"]}");
        var channel = GrpcChannel.ForAddress("https://localhost:7100");
        var client = new GrpcTesting.GrpcTestingClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = client.GetAllTesting(request);
            return _mapper.Map<IEnumerable<Testing>>(reply.Testing);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"----0---> Couldnot call GRPC Server {ex.Message}");
            return null;
        }
    }
}