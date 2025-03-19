using System;
using System.Collections.Generic;

namespace Store.Models;

public class CartProduct : StoreEntity
{
    public int Quantity { get; set; }

    public string? ProductSize { get; set; }

    public new Guid? Id { get; set; }

    public Guid ProductId { get; set; }

    public virtual WebUser? WebUser { get; set; }

    public virtual Product Product { get; set; } = null!;
}
