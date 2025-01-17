namespace ANCL_Marriage_MVC.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
        public int? ParentId { get; set; }
        public string PagePath { get; set; }
        public string PageType { get; set; }
        public int OrderBy { get; set; }
        public List<Menu> SubMenus { get; set; } = new List<Menu>();
    }
}
