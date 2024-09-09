using ECommerce.Business.Abstract;
using ECommerce.Entities.Models;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: ProductController
        public async Task<ActionResult> Index(int page = 1, int category = 0, string? filterByName = null, string? filterByPrice = null)
        {
            int pageSize = 10;
            //var items = await _productService.GetAllAsync();
            var items = await _productService.GetAllByCategoryAsync(category);
            var productList = items.Skip((page - 1) * pageSize).Take(pageSize);

            List<Product> orderedProductList = new List<Product>();

            if (filterByName != null)
            {
                if (filterByName == "a-z")
                {
                    orderedProductList = productList.OrderBy(p => p.ProductName).ToList();
                }
                else if (filterByName == "z-a")
                {
                    orderedProductList = productList.OrderByDescending(p => p.ProductName).ToList();
                }
            }

            if (filterByPrice != null)
            {
                if (filterByPrice.ToLower() == "lower to higher")
                {
                    orderedProductList = productList.OrderBy(p => p.UnitPrice).ToList();
                }
                else if (filterByPrice.ToLower() == "higher to lower")
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

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
