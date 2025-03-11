using System;
using System.Collections.Generic;

namespace Store.Infrastracture;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductImageUri { get; set; } = null!;

    public decimal ProductPrice { get; set; }

    public string? ProductDesc { get; set; }

    public string? ProductSize { get; set; }

    public byte[]? TimeStamp { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
