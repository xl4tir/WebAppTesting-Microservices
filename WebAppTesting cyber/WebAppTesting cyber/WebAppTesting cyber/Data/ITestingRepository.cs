
using WebAppTesting_cyber.Models;

namespace WebAppTesting_cyber.Data
{
    public interface ITesting : IBaseRepository<Testing>
    {

        Task<Testing> GetByName(string name);
        Task<List<Testing>> SelectAllByAuthor(long Author);
        
    }
}
