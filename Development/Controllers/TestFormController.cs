using ANCL_Marriage_MVC.Models;
using ANCL_Marriage_MVC.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

namespace ANCL_Marriage_MVC.Controllers
{
    public class TestFormController : Controller
    {
        private readonly IRepository _repo;
        public TestFormController(IRepository repos)
        {
            _repo = repos;
        }

        public IActionResult Index()
        {
            TestModel model = null;
            if (TempData["EditMode"] != null)
            {
                model = JsonConvert.DeserializeObject<TestModel>(TempData["EditMode"].ToString());
                return View(model);
            }
            else
            {
                getUserList();
                return View();
            }
        }

        public void getUserList()
        {
            string query = "select num_test_id, var_unit_fname, var_test_lname, var_test_email, var_test_address1, var_test_address2, ";
            query += "  blb_test_profimage, var_test_profpath, var_test_state, var_test_postalcode, var_test_countryid, ";
            query += " num_test_mobileno,var_test_password,var_test_gender,num_test_dob from testmvc ";

            var response = _repo.QueryExecute<TestModel>(query);
            ViewBag.respo = response;
        }

        [HttpPost("HandleFileUpload")]
        public async Task<IActionResult> HandleFileUpload(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded!");

            string fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FileUploads/", file.FileName);

            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (var stream = new FileStream(path, FileMode.Create))
                await file.CopyToAsync(stream);

            string accessPath = $"{Request.Scheme}://{Request.Host}/FileUploads/{file.FileName}";

            // Save file bytes
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            HttpContext.Session.Set("FileBytes", fileBytes);
            HttpContext.Session.SetString("FilePath", accessPath);

            return Ok(new { imageUrl = accessPath });
        }

        [HttpPost]
        public IActionResult SubmitData(TestModel obj)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                int errorCode = 0;
                int testId = 0;
                int mode = 1;

                if (obj.num_test_id != 0)
                {
                    testId = obj.num_test_id;
                    mode = 2;
                }

                _repo.ExcuteDynamic("testmvc_ins", new
                {
                    in_userid = "MVC_Core",
                    in_id = obj.num_test_id != 0 ? (object)obj.num_test_id : DBNull.Value,
                    in_fname = !string.IsNullOrEmpty(obj.var_unit_fname) ? (object)obj.var_unit_fname : DBNull.Value,
                    in_lname = !string.IsNullOrEmpty(obj.var_test_lname) ? (object)obj.var_test_lname : DBNull.Value,
                    in_email = !string.IsNullOrEmpty(obj.var_test_email) ? (object)obj.var_test_email : DBNull.Value,
                    in_address1 = !string.IsNullOrEmpty(obj.var_test_address1) ? (object)obj.var_test_address1 : DBNull.Value,
                    in_address2 = !string.IsNullOrEmpty(obj.var_test_address2) ? (object)obj.var_test_address2 : DBNull.Value,
                    in_state = obj.num_tblstate_id != 0 ? (object)obj.num_tblstate_id : DBNull.Value,
                    in_postalcode = obj.var_test_postalcode != 0 ? (object)obj.var_test_postalcode : DBNull.Value,
                    in_country = obj.var_test_countryid != 0 ? (object)obj.var_test_countryid : DBNull.Value,
                    in_dob = obj.num_test_dob != DateTime.MinValue ? (object)obj.num_test_dob : DBNull.Value,
                    in_gender = !string.IsNullOrEmpty(obj.var_test_gender) ? (object)obj.var_test_gender : DBNull.Value,
                    in_mobileno = obj.num_test_mobileno == 0 ? (object)obj.num_test_mobileno : DBNull.Value,
                    in_password = !string.IsNullOrEmpty(obj.var_test_password) ? (object)obj.var_test_password : DBNull.Value,
                    in_mode = mode,
                },
                 out errorMessage,
                 out errorCode,
                 out testId);
                if (errorCode == 9999)
                {
                    int Id = testId;
                    if (HttpContext.Session.Get("FileBytes") != null)
                    {
                        byte[] fileUploaded = HttpContext.Session.Get("FileBytes");
                        string filepath = HttpContext.Session.GetString("FilePath");

                        string query = "";
                        int _a;

                        OracleConnection Con = _repo.GetConn();

                        if (Id != 0)
                        {
                            query = " update testmvc set blb_test_profimage =:BLOBBanImage,var_test_profpath='" + filepath + "' ";
                            query += " where num_test_id ='" + Id + "'";

                            OracleParameter _BLOBBanImage = new OracleParameter();
                            _BLOBBanImage.OracleDbType = OracleDbType.Blob;
                            _BLOBBanImage.ParameterName = "BLOBBanImage";
                            _BLOBBanImage.Value = fileUploaded;

                            OracleCommand Cmd1 = new OracleCommand(query, Con);
                            Cmd1.Parameters.Add(_BLOBBanImage);

                            _a = Cmd1.ExecuteNonQuery();
                            Cmd1.Dispose();

                            HttpContext.Session.Remove("FileBytes");
                            HttpContext.Session.Remove("FilePath");

                        }
                    }

                    ViewBag.ErrorMessage = errorMessage;
                }
                else
                {
                    ModelState.AddModelError("", errorMessage);
                    return View(obj);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SubmitData(int? id)
        {

            if (id == null || id == 0)
            {
                return View();
            }
            else
            {
                //Edit mode
                string query = "select num_test_id, var_unit_fname, var_test_lname, var_test_email, var_test_address1, var_test_address2, ";
                query += " var_test_profpath, var_test_state, var_test_postalcode, var_test_countryid, ";
                query += " num_test_mobileno,var_test_password,var_test_gender,num_test_dob from testmvc ";
                query += " where num_test_id = '" + id + "'";

                var response = _repo.QueryExecute<TestModel>(query);

                TestModel resp = response?.FirstOrDefault();

                TempData["EditMode"] = JsonConvert.SerializeObject(resp);
                TempData["SelectedCountry"] = resp.var_test_countryid;
                TempData["SelectedState"] = resp.var_test_state;
                TempData.Keep("SelectedState");

                return RedirectToAction("Index");

            }
        }

        [HttpGet]
        public IActionResult GetState(int? countryid)
        {
            string query = "select num_tblstate_id,var_tblstate_name from tblstate ";
            query += " where num_tblstate_country = '" + countryid + "'";

            var response = _repo.QueryExecute<TestModel>(query);
            return Json(new { data = response });
        }
    }
}
