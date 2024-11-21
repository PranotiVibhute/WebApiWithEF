using System;
using System.Collections.Generic;

namespace WebApiWithEF.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? CategoryId { get; set; }

    public int? SupplierId { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
