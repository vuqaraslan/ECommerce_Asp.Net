using ECommerce.Business.Abstract;
using ECommerce.Entities.Models;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class AdminController : Controller
    {
        //[Authorize(Roles = "Admin")]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[Authorize(Roles = "Editor")]
        //public string Index2()
        //{
        //    return "Hello from Editor";
        //}

        private readonly IProductService _productService;

        public AdminController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ActionResult> Index(int page = 1, int category = 0, string? filterByName = null, string? filterByPrice = null)
        {
            int pageSize = 10;
            //var items = await _productService.GetAllAsync();
            var items = await _productService.GetAllByCategoryAsync(category);
            var productList = items.Skip((page - 1) * pageSize).Take(pageSize);

            List<Product> orderedProductList = new List<Product>();

            if (filterByName != null)
            {
                if (filterByName.ToLower() == "a-z")
                {
                    orderedProductList = productList.OrderBy(p => p.ProductName).ToList();
                }
                else if (filterByName.ToLower() == "z-a")
                {
                    orderedProductList = productList.OrderByDescending(p => p.ProductName).ToList();
                }
            }

            if (filterByPrice != null)
            {
                if (filterByPrice.ToLower() == "higher to lower")
                {
                    orderedProductList = productList.OrderBy(p => p.UnitPrice).ToList();
                }
                else if (filterByPrice.ToLower() == "lower to higher")
                {
                    orderedProductList = productList.OrderByDescending(p => p.UnitPrice).ToList();
                }
            }

            if (filterByPrice == null && filterByName == null)
            {
                orderedProductList = productList.ToList();
            }
            var model = new ProductListViewModel
            {
                //Products = items.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                Products = orderedProductList,
                CurrentCategory = category,
                PageCount = (int)Math.Ceiling(items.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };
            return View(model);
        }



    }
}
