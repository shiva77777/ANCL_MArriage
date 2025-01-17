using ANCL_Marriage_MVC.Models;
using ANCL_Marriage_MVC.Repositories.Interface;
using ANCL_Marriage_MVC.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace ANCL_Marriage_MVC.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ICommonRepositery commonRepositery;
        private readonly ITransactionsServices transactionsServices;


        public TransactionsController(ICommonRepositery commonRepositery, ITransactionsServices transactionsServices)
        {
            this.commonRepositery = commonRepositery;
            this.transactionsServices = transactionsServices;

        }

        [HttpGet]
        [Route("/Masters/FrmFormEntry")]
        public async Task<IActionResult> Index()
        {
            String Query = " select wardname Name ,wardid Id from prop.vw_ward_mas where ulbid = '" + HttpContext.Session.GetString("ULBID") + "' ";
            var ward = await commonRepositery.BindDropDown(Query);

            var dropDownList = ward.Select(obj => new SelectListItem
            {
                Value = obj.Id.ToString(), // Assuming ULBS has a property Id
                Text = obj.Name // Assuming ULBS has a property Name
            }).ToList();
            ViewBag.DropDownItems = dropDownList;
            ViewBag.Ward = ward;
            return View("FormEntry");
        }

        [HttpPost]
        [Route("/Masters/FrmFormEntry")]
        public async Task<IActionResult> Entry(FormEntry formEntry)
        {
            formEntry.UserId = HttpContext.Session.GetString("USERID");
            FormEntry response = await transactionsServices.FormEntry(formEntry);

            if (response == null)
            {
                return View("FormEntry");
            }
            // Serialize the response model to TempData
            //TempData["ModelData"] = System.Text.Json.JsonSerializer.Serialize(response);
            HttpContext.Session.SetString("FormId", response.OutFormId.ToString());

            return View("FormEntry");
        }

        [HttpGet]
        [Route("/Masters/FrmSlottAllocation")]
        public async Task<IActionResult> SlottAllocation()
        {
            String Query = " select wardname Name ,wardid Id from prop.vw_ward_mas where ulbid = '" + HttpContext.Session.GetString("ULBID") + "' ";
            var ward = await commonRepositery.BindDropDown(Query);

            var dropDownList = ward.Select(obj => new SelectListItem
            {
                Value = obj.Id.ToString(), // Assuming ULBS has a property Id
                Text = obj.Name // Assuming ULBS has a property Name
            }).ToList();
            ViewBag.DropDownItems = dropDownList;
            ViewBag.Ward = ward;

           // DataTable 
            return View();
        }


        [HttpPost]
        [Route("Transactions/SearchbyFormno")]
        public IActionResult SearchbyFormno()
        {
            return View();
        }

        [HttpPost]
        [Route("Masters/FrmSlottAllocation")]
        public IActionResult SlottAllocation1()
        {
            return View();
        }

    }
}
