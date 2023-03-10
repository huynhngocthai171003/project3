using System;
using System.Collections.Generic;

namespace Client.Models
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            OrderDetails = new HashSet<OrderDetail>();
            Orders = new HashSet<Order>();
            Stocks = new HashSet<Stock>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public int? Rate { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
        public string Avatar { get; set; } = null!;
        public string Image1 { get; set; } = null!;
        public string Image2 { get; set; } = null!;
        public string Image3 { get; set; } = null!;
        public int? CategoryId { get; set; }
        public bool? Status { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
