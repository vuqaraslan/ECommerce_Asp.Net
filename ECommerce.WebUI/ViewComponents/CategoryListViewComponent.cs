using ECommerce.Business.Abstract;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ECommerce.WebUI.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryService? _categoryService;

        public CategoryListViewComponent(ICategoryService? categoryService)
        {
            _categoryService = categoryService;
        }

        public ViewViewComponentResult Invoke(string controllerName="product")
        {
            var categories = _categoryService.GetAllAsync().Result;
            var param = HttpContext.Request.Query["category"];
            var category = int.TryParse(param, out var categoryId);
            var model = new CategoryListViewModel
            {
                ControllerName= controllerName,
                AllCategories = "All Categories",
                Categories=categories,
                CurrentCategory=category ? categoryId : 0
            };
            return View(model);
        }
    }
}
