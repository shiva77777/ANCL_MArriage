using System.ComponentModel.DataAnnotations;

namespace ANCL_Marriage_MVC.Models
{
    public class TestModel
    {
        [Key]
        public int num_test_id { get; set; }
        [Required]
        public string? var_unit_fname { get; set; }
        [Required]
        public string? var_test_lname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? var_test_email { get; set; }
        [Required]
        public string? var_test_address1 { get; set; }
        public string? var_test_address2 { get; set; }
        [Required]
        public string? var_test_state { get; set; }
        [Required]
        public Int64 var_test_postalcode { get; set; }
        [Required]
        public int var_test_countryid { get; set; }

        [MaxLength(100)]
        public Int64 num_test_mobileno { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime num_test_dob { get; set; }
        [Required]
        public string? var_test_gender { get; set; }

        [DataType(DataType.Password)]
        public string? var_test_password { get; set; }
        public byte[] blb_test_profimage { get; set; }
        public string? var_test_profpath { get; set; }

        public Int32 errorcode { get; set; }
        public string? errormsg { get; set; }
        public Int32 out_testid { get; set; }
        public string? usserid { get; set; }

        public int num_tblstate_id { get; set; }
        public string? var_tblstate_name { get; set; }

    }
}
