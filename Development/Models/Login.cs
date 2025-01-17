using System.ComponentModel.DataAnnotations;

namespace ANCL_Marriage_MVC.Models
{
    public class Login
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string macaddr { get; set; }
        public string ipaddr { get; set; }
        public string hostname { get; set; }
        public string source { get; set; } = "WEB";
        public Int64 deptid { get; set; } = 25;
    }
}
