
using Microsoft.EntityFrameworkCore;
using WebAppTesting_cyber.Models;

namespace WebAppTesting_cyber.Data
{
    public class TestingRepository : ITesting
    {

        private readonly AppDbContext _db;

        public TestingRepository(AppDbContext db)
        {
            this._db = db;
        }
        

        public async Task Create(Testing entity)
        {
            await _db.Testing.AddAsync(entity);
            await _db.SaveChangesAsync();

            
        }

        public async Task DeleteAsync(Testing entity)
        {
             _db.Testing.Remove(entity);
            await _db.SaveChangesAsync();
            
        }

        public async Task<Testing> Get(int id)
        {
            return await _db.Testing.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Testing> GetAll()
        {
            return _db.Testing;
        }

        public async Task<Testing> GetByName(string name)
        {
            return await _db.Testing.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Testing>> Select()
        {
            return await _db.Testing.ToListAsync();
        }

        

        public async Task<List<Testing>> SelectWithInclude()
        {
            return await _db.Testing.Include(x => x.UserId).Include(x => x.Subject).Include(x => x.Grade).ToListAsync();
        }

        public async Task<Testing> Update(Testing entity)
        {
            _db.Testing.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }


        


        public async Task<List<Testing>> SelectAllByAuthor(long AuthorId)
        {
            return await _db.Testing.Where(p => p.UserId == AuthorId).ToListAsync();
        }


}
}
