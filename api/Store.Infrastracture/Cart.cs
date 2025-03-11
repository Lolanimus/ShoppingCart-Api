using System;
using System.Collections.Generic;

namespace Store.Infrastracture;

public partial class Cart
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int ProductQuantity { get; set; }

    public byte[]? TimeStamp { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual WebUser User { get; set; } = null!;
}
