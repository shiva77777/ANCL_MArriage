using ANCL_Marriage_MVC.Models;
using Dapper;

namespace ANCL_Marriage_MVC.Repositories.Interface
{
    public interface ILoginRepository
    {
        Task<LoginResponse> Login(DynamicParameters parameters);
    }
}
