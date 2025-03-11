using System;
using System.Collections.Generic;

namespace Store.Models;

public partial class Product : StoreEntity
{

    public string ProductName { get; set; } = null!;

    public string? ProductGender { get; set; }

    public string ProductImageUri { get; set; } = null!;

    public decimal ProductPrice { get; set; }

    public string? ProductDesc { get; set; }

}
