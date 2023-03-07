using TestingCompleteService.Models;
using TestingCompleteService.SyncDataServices.Grpc;

namespace TestingCompleteService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder applicationBuilder)
    {
        
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var grpcClient = serviceScope.ServiceProvider.GetRequiredService<ITestingDataClient>();
        
            var platforms = grpcClient.returnAllTestings();
        
            SeedData(serviceScope.ServiceProvider.GetRequiredService<ITestingCompleteRepository>(), platforms);
        }
        
        
        
        // var grpcClient = serviceScope.ServiceProvider.GetRequiredService<ITestingDataClient>();
        //
        // var testings = grpcClient.returnAllTestings();
        //
        // SeedData(serviceScope.ServiceProvider.GetRequiredService<ITestingCompleteRepository>(), testings);
    
    }
         
    private static void SeedData(ITestingCompleteRepository repo, IEnumerable<Testing> testings)
    {
        Console.WriteLine("Seeding new testings...");

        foreach (var testing in testings)
        {
            if(!repo.ExternalTestingExist(testing.ExternalID))
            {
                repo.CreateTestings(testing);
            }

        }
    }
}