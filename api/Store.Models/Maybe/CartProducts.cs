using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models.Maybe
{
    public class CartProducts
    {
        public required Cart Cart { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
