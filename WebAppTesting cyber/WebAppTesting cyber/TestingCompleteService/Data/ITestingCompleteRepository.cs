using TestingCompleteService.Models;
using WebAppTesting_cyber.Data;

namespace TestingCompleteService.Data;

public interface ITestingCompleteRepository : IBaseRepository<TestingComplete>
{
    //Testing Complete
    //Implemented by BaseRep
    IEnumerable<TestingComplete> GetTestingCompleteForTesting(int testingId);


    //Testing
    void CreateTestings(Testing entity);
    IQueryable<Testing> GetAllTestings();
    bool TestingExits(int testingId);
    bool ExternalTestingExist(int externalPlatformId);

}