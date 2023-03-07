namespace WebAppTesting_cyber.SyncDataServices.Http;
using Dtos;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class HttpTestingCompleteDataClient : ITestingCompleteDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    
    public HttpTestingCompleteDataClient(HttpClient httpClient, IConfiguration configuration) 
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task SendTestingToTestingComplete(TestingReadDto testing){
        var httpContent = new StringContent(
            JsonSerializer.Serialize(testing),
            Encoding.UTF8,
            "application/json"
        );
        
        var response = await _httpClient.PostAsync($"{_configuration["TestingCompleteService"]}", httpContent);
        
        if(response.IsSuccessStatusCode){
            Console.WriteLine(" ---> SYYnc Post to TestingCompleteService was OK!!! :-)");
        }else{
            Console.WriteLine(" --- >>>> Sorry my friend, but it does not work!((");
        }
    }
}  