using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGL.Auth.Application.Interfaces.Generic
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);       
        Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<int> InsertAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);

        Task<T> sp_GetByIdAsync(int id);
        Task<IEnumerable<T>> sp_GetAllAsync();
        Task<IEnumerable<T>> sp_GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<int> sp_InsertAsync(T entity);
        Task<int> sp_UpdateAsync(T entity);
        Task<int> sp_DeleteAsync(int id);




    }
}
