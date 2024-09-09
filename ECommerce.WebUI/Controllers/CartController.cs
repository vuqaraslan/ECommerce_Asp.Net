using ECommerce.Business.Abstract;
using ECommerce.Business.Concrete;
using ECommerce.Entities.Concrete;
using ECommerce.WebUI.Models;
using ECommerce.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartSessionService? _cartSessionService;
        private readonly IProductService? _productService;
        private readonly ICartService? _cartService;
        public CartController(ICartSessionService? cartSessionService, IProductService? productService, ICartService? cartService)
        {
            _cartSessionService = cartSessionService;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> AddToCart(int productId, int page, int category)
        {
            var productToBeAdded = await _productService.GetByIdAsync(productId);
            var cart = _cartSessionService.GetCart();

            _cartService.AddToCart(cart, productToBeAdded);
            _cartSessionService.SetCart(cart);
            TempData.Add("message", string.Format("Your Product, {0} was added successfully to cart !", productToBeAdded.ProductName));
            return RedirectToAction("Index", "Product", new { page = page, category = category });
        }

        public IActionResult List()
        {
            var cart = _cartSessionService.GetCart();
            var model = new CartListViewModel
            {
                Cart = cart
            };
            return View(model);
        }

        public IActionResult Remove(int productId)
        {
            var cart = _cartSessionService.GetCart();
            _cartService.RemoveFromCart(cart, productId);
            _cartSessionService.SetCart(cart);
            TempData.Add("message", "Your product removed successfully from cart!");
            return RedirectToAction("List");
        }

        public IActionResult Increase(int productId)
        {
            var cart = _cartSessionService.GetCart();
            _cartService.IncreaseQuantity(cart, productId);
            //var increasePrd=cart.Cartlines.FirstOrDefault(cl=>cl.Product.ProductId == productId);
            //if(increasePrd!=null)
            //{
            //    increasePrd.Quantity += 1;
            //    _cartSessionService.SetCart(cart);
            //}
            _cartSessionService.SetCart(cart);
            return RedirectToAction("List");

        }

        public IActionResult Decrease(int productId)
        {
            var cart = _cartSessionService.GetCart();
            _cartService.DecreaseQuantity(cart, productId);
            _cartSessionService.SetCart(cart);
            return RedirectToAction("List");
        }

        public IActionResult Complete()
        {
            var shippingDetailViewModel = new ShippingDetailViewModel
            {
                ShippingDetails = new ShippingDetails()
            };
            return View(shippingDetailViewModel);
        }

        [HttpPost]
        public IActionResult Complete(ShippingDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TempData.Add("message", String.Format("Thank you {0}, you order is in progress", model.ShippingDetails.Firstname));
            //return View(new ShippingDetailViewModel { ShippingDetails=new ShippingDetails()});
            //return RedirectToAction("Index","Product");
            return RedirectToAction(nameof(List));
        }
    }
}
