using System;
using System.Collections.Generic;

namespace Store.Infrastracture;

public partial class WebUser
{
    public int Id { get; set; }

    public string UserEmail { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string UserFirstName { get; set; } = null!;

    public string UserLastName { get; set; } = null!;

    public string UserCity { get; set; } = null!;

    public string UserStreet { get; set; } = null!;

    public int UserNumber { get; set; }

    public string UserZip { get; set; } = null!;

    public string UserPhone { get; set; } = null!;

    public byte[]? TimeStamp { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
