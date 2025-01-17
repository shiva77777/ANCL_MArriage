using ANCL_Marriage_MVC.Models;

namespace ANCL_Marriage_MVC.Services.Interface
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(Login login);
    }
}
