using ANCL_Marriage_MVC.Repositories.Interface;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ANCL_Marriage_MVC.Repositories
{
    public class Repository : IRepository
    {
        private IConfiguration _configuration { get; set; }
        public string ConnectionString { get; set; }

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DbConnectionString");
        }
        public OracleConnection GetConn()
        {
            var connection = new OracleConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public void OpenConn()
        {
            var connection = new OracleConnection(ConnectionString);
            connection.Open();
        }

        public void CloseConn()
        {
            var connection = new OracleConnection(ConnectionString);
            connection.Close();
        }

        public void Execute(string name)
        {
            Execute(name, null);
        }

        public void Execute(string name, object param)
        {
            using (var cnn = new OracleConnection(ConnectionString))
            {
                cnn.Execute(name, param, commandType: CommandType.StoredProcedure);
            }
        }
        public void ExcuteDynamic(string name, object param, out string errorMessage, out int errorCode, out int Id)
        {
            errorMessage = string.Empty;
            errorCode = 0;
            Id = 0;

            using (var cnn = new OracleConnection(ConnectionString))
            {
                var Params = new DynamicParameters(param);

                Params.Add("out_errcode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                Params.Add("out_errmsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                Params.Add("out_testid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                cnn.Execute(name, Params, commandType: CommandType.StoredProcedure);

                errorCode = Params.Get<int>("out_errcode");
                errorMessage = Params.Get<string>("out_errmsg");
                Id = Params.Get<int>("out_testid");
            }
        }


        public void QueryExecute(string name, object param)
        {
            using (var cnn = new OracleConnection(ConnectionString))
            {
                cnn.Execute(name, param, commandType: CommandType.Text);
            }
        }

        public void QueryExecute(string name)
        {
            using (var cnn = new OracleConnection(ConnectionString))
            {
                cnn.Execute(name, null, commandType: CommandType.Text);
            }
        }

        public List<T> QueryExecute<T>(string query)
        {
            using (var cnn = new OracleConnection(ConnectionString))
            {
                return cnn.Query<T>(query, null, commandType: CommandType.Text).ToList();
            }
        }
    }
}
