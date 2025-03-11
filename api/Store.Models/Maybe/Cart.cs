using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models.Maybe
{
    public class Cart : StoreEntity
    {
        public required User User { get; set; }
        public List<CartProducts>? CartProducts { get; set; }
    }
}
