using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestingCompleteService.Data;
using TestingCompleteService.Dtos;
using TestingCompleteService.Models;

namespace TestingCompleteService.Controllers;

[Route("api/comp/testing/{testingId}/[controller]")]
[ApiController]
public class TestingCompleteController : ControllerBase
{
    private readonly ITestingCompleteRepository _repository;
    private readonly IMapper _mapper;

    public TestingCompleteController(ITestingCompleteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    
    [HttpGet]
    public ActionResult<IEnumerable<TestingCompleteReadDto>> GetTestingsCompletesFotTestings(int testingId)
    {
        Console.WriteLine($"------> Hit ALL!!! GetTestingCompleteFotTestings: {testingId}");

        if (!_repository.TestingExits(testingId))
        {
            return NotFound();
        }

        var testingComplete = _repository.GetTestingCompleteForTesting(testingId);

        return Ok(_mapper.Map<IEnumerable<TestingCompleteReadDto>>(testingComplete));
    }
    
    
    [HttpGet("{testingCompleteId}", Name = "GetTestingCompleteForTesting")]
    public ActionResult<TestingCompleteCreateDto> GetTestingCompleteForTesting(int testingId, int testingCompleteId)
    {
        Console.WriteLine($"-----> Hit ONE GetTestingCompleteForTesting: {testingId} / {testingCompleteId}");

        if (!_repository.TestingExits(testingId))
        {
            return NotFound();
        }

        var testingComplete = _repository.Get(testingId, testingCompleteId);

        if(testingComplete == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<TestingCompleteCreateDto>(testingComplete));
    }
    
     
    [HttpPost]
    public ActionResult<TestingCompleteReadDto> CreateTestingCompleteForTesting(int testingId, TestingCompleteCreateDto testingCompleteDto)
    {
        Console.WriteLine($"--++---> Hit CreateTestingCompleteForTesting: {testingId}");

        if (!_repository.TestingExits(testingId))
        {
            return NotFound();
        }

        var testingComplete = _mapper.Map<TestingComplete>(testingCompleteDto);

        _repository.Create(testingId, testingComplete);

        var testingCompleteReadDto = _mapper.Map<TestingCompleteReadDto>(testingComplete);

        return CreatedAtRoute(nameof(GetTestingCompleteForTesting),
            new {testingId = testingId, testingCompleteId = testingCompleteReadDto.Id}, testingCompleteReadDto);
    }
    
}