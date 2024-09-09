using ECommerce.Business.Abstract;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class CartService : ICartService
    {
        public void AddToCart(Cart cart, Product product)
        {
            CartLine cartLine = cart.Cartlines.FirstOrDefault(c => c.Product.ProductId == product.ProductId);
            if (cartLine != null)
            {
                cartLine.Quantity++;
            }
            else
            {
                cart.Cartlines.Add(new CartLine { Product = product, Quantity = 1 });
            }
        }

        public void IncreaseQuantity(Cart cart, int productId)
        {
            var increaseProductQuantity = cart.Cartlines.FirstOrDefault(cl => cl.Product.ProductId == productId);
            if (increaseProductQuantity != null)
            {
                increaseProductQuantity.Quantity += 1;
            }
        }

        public void DecreaseQuantity(Cart cart, int productId)
        {
            var decreaseProductQuantity=cart.Cartlines.FirstOrDefault(cl=>cl.Product.ProductId==productId);
            if (decreaseProductQuantity != null)
            {
                decreaseProductQuantity.Quantity -= 1;
            }
        }

        public List<CartLine>? List(Cart cart)
        {
            return cart.Cartlines;
        }

        public void RemoveFromCart(Cart cart, int productId)
        {
            cart.Cartlines.Remove(cart.Cartlines.FirstOrDefault(c => c.Product.ProductId == productId));
        }
    }
}
