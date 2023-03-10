namespace Client.Models
{
    public class ProductDto
    {
        public ProductDto()
        {
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
        public IFormFile? Avatar { get; set; }
        public IFormFile? Image1 { get; set; }
        public IFormFile? Image2 { get; set; }
        public IFormFile? Image3 { get; set; }
        public int? CategoryId { get; set; }
        public bool? Status { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
