using System;
using System.Collections.Generic;

#nullable disable

namespace Product.Api.Models.EF
{
    public partial class Product
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string ProductGroupNk { get; set; }
        public decimal Price { get; set; }
        public string Comments { get; set; }

        public virtual ProductGroup ProductGroupNkNavigation { get; set; }
    }
}
