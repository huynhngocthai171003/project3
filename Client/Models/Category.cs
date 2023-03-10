using System;
using System.Collections.Generic;

namespace Client.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
