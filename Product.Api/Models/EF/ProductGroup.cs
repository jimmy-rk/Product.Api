using System;
using System.Collections.Generic;

#nullable disable

namespace Product.Api.Models.EF
{
    public partial class ProductGroup
    {
        public ProductGroup()
        {
            Products = new HashSet<Product>();
        }

        public string Nk { get; set; }
        public string Text { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
