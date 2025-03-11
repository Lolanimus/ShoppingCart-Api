using Store.Models;
using System;
using System.Collections.Generic;

namespace Store.Models;

public partial class Cart
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string? ProductSize { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual WebUser User { get; set; } = null!;
}
