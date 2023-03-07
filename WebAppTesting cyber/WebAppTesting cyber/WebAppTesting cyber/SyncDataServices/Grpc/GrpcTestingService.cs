using AutoMapper;
using Grpc.Core;
using WebAppTesting_cyber.Data;

namespace WebAppTesting_cyber.SyncDataServices.Grpc;

public class GrpcTestingService : GrpcTesting.GrpcTestingBase
{
    private readonly ITesting _repository;
    private readonly IMapper _mapper;

    public GrpcTestingService(ITesting repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public override Task<TestingResponse> GetAllTesting(GetAllRequest request, ServerCallContext context)
    {
        var response = new TestingResponse();
        var testings = _repository.GetAll();

        foreach(var testing in testings)
        {
            response.Testing.Add(_mapper.Map<GrpcTestingModel>(testing));
        }

        return Task.FromResult(response);
    }
}