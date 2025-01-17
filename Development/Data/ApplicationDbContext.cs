using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ANCL_Marriage_MVC.Data
{
    public class ApplicationDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ApplicationDbContext(IConfiguration configuration)
        {
            this._configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ANCLDbConnection");
        }

        public IDbConnection CreateConnection() => new OracleConnection(this._connectionString);
    }
}
