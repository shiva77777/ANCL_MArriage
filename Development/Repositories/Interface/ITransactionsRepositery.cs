using ANCL_Marriage_MVC.Models;
using Dapper;

namespace ANCL_Marriage_MVC.Repositories.Interface
{
    public interface ITransactionsRepositery
    {
        Task<FormEntry> FormEntry(DynamicParameters parameters);
    }
}
