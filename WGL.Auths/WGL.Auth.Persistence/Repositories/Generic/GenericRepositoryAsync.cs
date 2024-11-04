using Dapper;
using System.Data;
using WGL.Auth.Application.Interfaces.Generic;
using WGL.Auth.Persistence.Contexts;

namespace WGL.Auth.Persistence.Repositories.Generic
{
    public class GenericRepositoryAsync<T>(DapperContext dapperContext) : IGenericRepositoryAsync<T> where T : class
    {
        private readonly IDbConnection _dbconnection = dapperContext.CreateConnection();

        
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbconnection.QueryAsync<T>("SELECT * FROM " + typeof(T).Name);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbconnection.QueryFirstOrDefaultAsync<T>("SELECT * FROM " + typeof(T).Name + " WHERE Id = @Id", new { Id = id });
        }
        public Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
        public async Task<int> InsertAsync(T entity)
        {
            return await _dbconnection.ExecuteAsync($"INSERT INTO {typeof(T).Name} VALUES (@Property1, @Property2, ...)", entity);
        }
        public async Task<int> UpdateAsync(T entity)
        {
            return await _dbconnection.ExecuteAsync($"UPDATE {typeof(T).Name} SET Property1 = @Property1, Property2 = @Property2, ... WHERE Id = @Id", entity);            
        }
        public async Task<int> DeleteAsync(int id)
        {
            return await _dbconnection.ExecuteAsync($"DELETE FROM {typeof(T).Name} WHERE Id = @Id", new { Id = id });
        }

        public Task<T> sp_GetByIdAsync(int id)
        {
            return _dbconnection.QuerySingleOrDefaultAsync<T>("", "", commandType: CommandType.StoredProcedure);
        }

        public Task<IEnumerable<T>> sp_GetAllAsync()
        {
            return _dbconnection.QueryAsync<T>("","",commandType:CommandType.StoredProcedure);
        }

        public Task<IEnumerable<T>> sp_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return _dbconnection.QueryAsync<T>("", "", commandType: CommandType.StoredProcedure);
        }

        public Task<int> sp_InsertAsync(T entity)
        {
            return _dbconnection.ExecuteAsync("", "", commandType: CommandType.StoredProcedure);
        }

        public Task<int> sp_UpdateAsync(T entity)
        {
            return _dbconnection.ExecuteAsync("", "", commandType: CommandType.StoredProcedure);
        }

        public Task<int> sp_DeleteAsync(int id)
        {
            return _dbconnection.ExecuteAsync("", "", commandType: CommandType.StoredProcedure);
        }
    }
}
