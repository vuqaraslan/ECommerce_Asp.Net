using ECommerce.Entities.Models;

namespace ECommerce.WebUI
{
    public class CategoryListViewModel
    {
        public List<Category> Categories { get; set; }
        public int CurrentCategory { get; set; }
        public string AllCategories { get; internal set; }
        public string ControllerName { get; set; }
    }
}