using ANCL_Marriage_MVC.Models;
using System.Data;

namespace ANCL_Marriage_MVC.Repositories.Interface
{
    public interface ICommonRepositery
    {
        Task<IEnumerable<ULBS>> GetULBS();
        Task<ULBS> GetULBSById(string ulbId);

        Task<List<Menu>> GetMenusAsync(int deptId, string userId, Int64? ulbId);

        Task<IEnumerable<DropDown>> BindDropDown(string Query);

        Task<DataTable> ExecuteQueryAsync(string query);
    }
}
