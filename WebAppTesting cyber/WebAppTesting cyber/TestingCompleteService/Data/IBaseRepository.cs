using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTesting_cyber.Data
{
    public interface IBaseRepository<T>
    {
        void Create(int testingId,T entity);


        Task<T> Get(int testingId, int testingCompleteId);

        Task<List<T>> Select();

        Task DeleteAsync(T entity);

        IQueryable<T> GetAll();

        Task<T> Update(T entity);



    }
}
