using ANCL_Marriage_MVC.Models;
using ANCL_Marriage_MVC.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ANCL_Marriage_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICommonRepositery commonRepositery;

        public HomeController(ILogger<HomeController> logger, ICommonRepositery commonRepositery)
        {
            _logger = logger;
            this.commonRepositery = commonRepositery;
        }

        public async Task<IActionResult> Index()
        {
            var modelData = HttpContext.Session.GetString("ModelData");
            if (!string.IsNullOrEmpty(modelData))
            {
                //var model = System.Text.Json.JsonSerializer.Deserialize<ANCL_Fire_MVC.Models.LoginResponse>(
                //    TempData["ModelData"].ToString()
                //);

                var model = JsonConvert.DeserializeObject<ANCL_Marriage_MVC.Models.LoginResponse>(modelData);
                var menus = await commonRepositery.GetMenusAsync(25, model.userId, model.OrgId);
                var menuHierarchy = BuildMenuHierarchy(menus);
                var viewModel = new LayoutViewModel
                {
                    LoginResponse = model,
                    Menus = menus
                };
                HttpContext.Session.SetString("viewModel", System.Text.Json.JsonSerializer.Serialize(viewModel));
                return View("Home"); // Pass the model to the "Home" view
            }

            return View("Home");
        }
        private List<Menu> BuildMenuHierarchy(List<Menu> menus)
        {
            var lookup = menus.ToDictionary(x => x.MenuId);
            foreach (var menu in menus.Where(x => x.ParentId.HasValue))
            {
                if (lookup.ContainsKey(menu.ParentId.Value))
                {
                    lookup[menu.ParentId.Value].SubMenus.Add(menu);
                }
            }

            return menus.Where(x => !x.ParentId.HasValue).ToList();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Logout()
        {
            //var modelData = HttpContext.Session.GetString("ModelData");
            //var model = JsonConvert.DeserializeObject<ANCL_Fire_MVC.Models.LoginResponse>(modelData);
          
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //  return View("Index","Login");
            TempData["SuccessMessage"] = "Log Out successfully!";
            return RedirectToAction("Index", "Login");
        }
    }
}
