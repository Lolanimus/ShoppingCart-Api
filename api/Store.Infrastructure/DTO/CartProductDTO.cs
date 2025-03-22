using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastracture.DTO
{
    public class CartProductDTO
    {
        public Guid? Id { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public string? ProductSize { get; set; }

        public string? TimeStamp { get; set; }

        public virtual Product? Product { get; set; } = null!;

        public virtual WebUser? User { get; set; } = null!;
    }
}
