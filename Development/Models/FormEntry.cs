namespace ANCL_Marriage_MVC.Models
{
    public class FormEntry:BaseClass
    {
        public Int64? UlbId { get; set; } = 5;
        public String? UserId { get; set; }
        public Int64? FormId { get; set; }
        public String? FirstName { get; set; }
        public String? MiddleName { get; set; }
        public String? LastName { get; set; }
        public Int64? PhoneNo { get; set; }
        public Int64? CollCenterId { get; set; }
        public String? Adddress { get; set; }
        public Int64? SlotId { get; set; }
        public String? RegType { get; set; }
        public DateTime AllocDate { get; set; }
        public String? Source { get; set; } = "DPT";
        public Int64? OutFormId { get; set; }
        public String? WardNo { get; set; }

        public String? AppName { get; set; }
    }
}
