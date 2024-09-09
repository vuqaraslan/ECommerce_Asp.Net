using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Concrete
{
    public class Cart
    {
        public List<CartLine>? Cartlines { get; set; }

        public Cart()
        {
            Cartlines = new List<CartLine>();
        }

        public decimal Total
        {
            get
            {
                return (decimal)Cartlines.Sum(c => c.Product.UnitPrice * c.Quantity);
            }
        }
    }
}
