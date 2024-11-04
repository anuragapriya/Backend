using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace WGL.Utility.DBContext
{
    public class DBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("WGLDevConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
    
}
