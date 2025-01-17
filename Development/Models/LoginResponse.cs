namespace ANCL_Marriage_MVC.Models
{
    public class LoginResponse
    {
        public string? userId { get; set; }
        public string? password { get; set; }
        public string? macaddr { get; set; }
        public string? ipaddr { get; set; }
        public string? hostname { get; set; }
        public string? userName { get; set; }
        public string? lastLogin { get; set; }
        public string? lastLogOut { get; set; }
        public string? corporation { get; set; }
        public string? corporationAddress { get; set; }
        public string? receiptOfficeName { get; set; }
        public string? chalanOfficeName { get; set; }
        public string? prabhagName { get; set; }
        public string? prabhagID { get; set; }
        public string? desigID { get; set; }
        public Int64? deptID { get; set; }
        public string? userType { get; set; }
        public Int64? collectioncenter { get; set; }
        public string? mobileno { get; set; }
        public string? otpValidate { get; set; }
        public Int64? errorCode { get; set; }
        public string? errorMsg { get; set; }

        public string? Out_Corpengname { get; set; }

        public Int64? OrgId { get; set; }
        public string? forceFullPassChage { get; set; }
    }
}
