namespace ANCL_Marriage_MVC.Models
{
    public class LayoutViewModel
    {
        public LoginResponse LoginResponse { get; set; }
        public List<Menu> Menus { get; set; }
        public String logoUrl { get; set; } = "/Images/NagarKaryawaliLogo.png";
    }
}
