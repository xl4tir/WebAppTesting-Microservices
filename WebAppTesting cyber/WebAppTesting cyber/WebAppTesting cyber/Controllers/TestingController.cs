using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppTesting_cyber.AsyncDataServices;
using WebAppTesting_cyber.Data;
using WebAppTesting_cyber.Dtos;
using WebAppTesting_cyber.Models;
using WebAppTesting_cyber.SyncDataServices.Http;

namespace WebAppTesting_cyber.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestingController : ControllerBase
{
    private readonly ITesting _repository;
    private readonly IMapper _mapper;
    private readonly ITestingCompleteDataClient _testingCompleteDataClient ;
    private readonly IMessageBusClient _messageBusClient;


    public TestingController(
        ITesting repository, 
        IMapper mapper, 
        ITestingCompleteDataClient testingCompleteDataClient,
        IMessageBusClient messageBusClient 
        )
       
    {
        _repository = repository;
        _mapper = mapper;
        _testingCompleteDataClient = testingCompleteDataClient;
        _messageBusClient = messageBusClient;

    }

    [HttpGet]
    public ActionResult<IEnumerable<TestingReadDto>> GetTesting()
    {
        Console.WriteLine("------->>>>> Getting Testings.....");

        var testingItem = _repository.GetAll();

        return Ok(_mapper.Map<IEnumerable<TestingReadDto>>(testingItem));
    }

    
    [HttpGet("{id}" , Name = "GetTestingById")]
    public async Task<ActionResult<TestingReadDto>> GetTestingById(int id)
    {
        var testingItem = await _repository.Get(id);


        Console.WriteLine("--> Returning Platform - "+ id);
        return Ok(_mapper.Map<TestingReadDto>(testingItem));

    }
    
    [HttpPost]
    public async Task<ActionResult<TestingReadDto>> CreateTesting(TestingCreateDto testingCreateDto)
    {
        var testingModel = _mapper.Map<Testing>(testingCreateDto);
        await _repository.Create(testingModel);


        var testingReadDto = _mapper.Map<TestingReadDto>(testingModel);
        
        //Send Sync message
        try
        {
            await _testingCompleteDataClient.SendTestingToTestingComplete(testingReadDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not send synchronously: {ex.Message }");
        }
        
        
        //Send Async Messagee!!!
        try
        {
            var testingPublishedDto = _mapper.Map<TestingPublishedDto>(testingReadDto);
            testingPublishedDto.Event = "Testing_Published";
            _messageBusClient.PublishNewTesting(testingPublishedDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not send AAAAsynchronously: {ex.Message }");
        }
        
        return CreatedAtRoute(nameof(GetTestingById), new { Id = testingReadDto.Id}, testingReadDto);
    }

}