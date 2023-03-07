using Microsoft.EntityFrameworkCore;
using TestingCompleteService.Models;

namespace TestingCompleteService.Data;

public class TestingCompleteRepository: ITestingCompleteRepository
{
    private readonly AppDbContext _db;

    public TestingCompleteRepository(AppDbContext db)
    {
        _db = db;
    }
    
    public void Create(int testingId, TestingComplete entity)
    {

        entity.TestingId = testingId;
         _db.TestingComplete.Add(entity);
         _db.SaveChanges();
    }

    public async Task<TestingComplete> Get(int testingId, int testingCompleteId)
    {
        return await _db.TestingComplete.FirstOrDefaultAsync(x => x.Id == testingCompleteId && x.TestingId == testingId);
        
        
    }

    public Task<List<TestingComplete>> Select()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(TestingComplete entity)
    {
        _db.TestingComplete.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public IQueryable<TestingComplete> GetAll()
    {
        return _db.TestingComplete;
    }

    public async Task<TestingComplete> Update(TestingComplete entity)
    {
        _db.TestingComplete.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public IEnumerable<TestingComplete> GetTestingCompleteForTesting(int testingId)
    {
        return _db.TestingComplete.Where(c => c.TestingId == testingId).OrderBy(c => c.Testing.Name);
    }

    public void CreateTestings(Testing entity)
    {
        if (entity == null)
        {
            throw new NotImplementedException();
        }

        _db.Testing.Add(entity);
        _db.SaveChanges();
    }

    public IQueryable<Testing> GetAllTestings()
    {
        return _db.Testing;
    }

    public bool TestingExits(int testingId)
    {
        return _db.Testing.Any(p => p.Id == testingId);
    }

    public bool ExternalTestingExist(int externalPlatformId)
    {
        return _db.Testing.Any(p => p.ExternalID == externalPlatformId);
    }
}