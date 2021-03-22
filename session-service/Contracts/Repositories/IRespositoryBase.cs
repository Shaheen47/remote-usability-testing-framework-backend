using System.Collections.Generic;
using System.Threading.Tasks;

namespace session_service.Contracts.Repositories
{
    public interface IRepositoryBase<T> where T : class
        {
            Task<IList<T>> FindAll();

            Task<T> FindById(string id);

            Task<T> Create(T entity);
        

            Task<bool> Save();
        
            Task<bool> Update(T entity);
        
            Task<bool> Delete(T entity);

        }
    }
