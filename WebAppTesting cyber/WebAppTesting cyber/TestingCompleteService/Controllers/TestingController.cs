using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestingCompleteService.Data;
using TestingCompleteService.Dtos;

namespace TestingCompleteService.Controllers;

[Route("api/comp/[controller]")]
[ApiController]
public class TestingController : ControllerBase 
{
    private readonly ITestingCompleteRepository _repository;
    private readonly IMapper _mapper;

    public TestingController(ITestingCompleteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;

    }
    
    [HttpGet]
    public ActionResult<IEnumerable<TestingReadDto>> GetTesting()
    {
        Console.WriteLine("--> Getting Testings from TestingCompleteService");

        var testingItems = _repository.GetAllTestings();

        return Ok(_mapper.Map<IEnumerable<TestingReadDto>>(testingItems));
    }

    
    
    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        Console.WriteLine("--> Inbout Post # Command Service.....");

        return Ok("nbound test of from Testing COMPLETE controller");
    }
}