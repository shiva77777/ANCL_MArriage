using ANCL_Marriage_MVC.Models;
using ANCL_Marriage_MVC.Repositories.Interface;
using ANCL_Marriage_MVC.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ANCL_Marriage_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILoginRepository loginRepository;
        private readonly ILoginService loginService;

        public LoginController(IConfiguration configuration, ILoginRepository loginRepository, ILoginService loginService)
        {
            this.configuration = configuration;
            this.loginRepository = loginRepository;
            this.loginService = loginService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Login/Login")]
        public async Task<IActionResult> Login(Login objLogin)
        {
            //ViewBag.LogoUrl = "/Images/NagarKaryawaliLogo.png";
            objLogin.Password = AscPasswordLib.ClsPassword.Encrypt(objLogin.Password);


            LoginResponse response = await loginService.Login(objLogin);

            if (response == null)
            {
                return View(objLogin);
            }
            // Serialize the response model to TempData
            //TempData["ModelData"] = System.Text.Json.JsonSerializer.Serialize(response);
            HttpContext.Session.SetString("USERID", response.userId);
            HttpContext.Session.SetString("ULBID", response.OrgId.ToString());
            HttpContext.Session.SetString("CollectionCenter", response.collectioncenter.ToString());
            HttpContext.Session.SetString("ModelData", System.Text.Json.JsonSerializer.Serialize(response));
            TempData["SuccessMessage"] = "Login successfully!";
          //  TempData["ErrorMessage"] = "An error occurred while saving data.";
            return RedirectToAction("Index", "Home");


        }
    }
}
