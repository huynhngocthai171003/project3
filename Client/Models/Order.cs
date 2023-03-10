using System;
using System.Collections.Generic;

namespace Client.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? CustomerId { get; set; }
        public int Quantity { get; set; }
        public string? Status { get; set; }
        public string? Address { get; set; }
        public int TotalPrice { get; set; }
        public int PaymentTerm { get; set; }
        public string Phone { get; set; } = null!;
        public DateTime? OrderDate { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
