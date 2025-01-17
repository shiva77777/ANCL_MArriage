using Oracle.ManagedDataAccess.Client;

namespace ANCL_Marriage_MVC.Repositories.Interface
{
    public interface IRepository
    {
        string ConnectionString { get; set; }
        OracleConnection GetConn();
        void OpenConn();
        void CloseConn();
        void Execute(string name);
        void Execute(string name, object param);
        void ExcuteDynamic(string name, object param, out string errorMessage, out int errorCode, out int Id);
        void QueryExecute(string name);
        void QueryExecute(string name, object param);
        List<T> QueryExecute<T>(string query);
    }
}
